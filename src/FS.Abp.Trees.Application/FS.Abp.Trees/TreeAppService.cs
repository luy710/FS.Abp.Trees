using FS.Abp.Trees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Application.Services;

namespace FS.Abp.Trees
{
    public class TreeAppService<TEntity, TTreeDomainService, TGetOutputDto, TGetListOutputDto, TGetListInput, TCreateInput, TUpdateInput, TMoveInput>
        : CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, Guid, TGetListInput, TCreateInput, TUpdateInput>,
        ITreeAppService<TGetOutputDto, TCreateInput, TUpdateInput, TMoveInput> where TEntity : class, ITree<TEntity>, IEntity<Guid>, new()
        where TTreeDomainService : ITreeDomainService<TEntity>
        where TGetOutputDto : ITreeDto
        where TGetListOutputDto : ITreeDto
        where TGetListInput: IGetListInput
        where TCreateInput : ICreateInput
        where TUpdateInput : IUpdateInput
        where TMoveInput : IMoveInput
    {
        //TODO: down to Repository??
        protected TTreeDomainService TreeDomainService { get; }

        protected IRepository<TEntity, Guid> TreeRepository { get; }

        protected virtual string MovePolicyName { get; }

        public TreeAppService(
            TTreeDomainService treeDomainService,
            IRepository<TEntity, Guid> treeRepository
            )
            : base(treeRepository)
        {
            this.TreeDomainService = treeDomainService;
            this.TreeRepository = treeRepository;
        }

        protected override Task<TEntity> GetEntityByIdAsync(Guid id)
        {
            var query = this.Repository.WithDetails(x => x.Children).Where(x => x.Id == id);
            return AsyncQueryableExecuter.FirstOrDefaultAsync(query);
        }
        public async override Task<TGetOutputDto> CreateAsync(TCreateInput input)
        {
            await CheckCreatePolicyAsync();

            var entity = MapToEntity(input);

            TryToSetTenantId(entity);

            await TreeDomainService.CreateAsync(entity);

            return MapToGetOutputDto(entity);
        }

        public async override Task<TGetOutputDto> UpdateAsync(Guid id, TUpdateInput input)
        {
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);
            //TODO: Check if input has id different than given id and normalize if it's default value, throw ex otherwise
            MapToEntity(input, entity);

            await TreeDomainService.UpdateAsync(entity);

            return MapToGetOutputDto(entity);
        }

        public async override Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();

            await TreeDomainService.DeleteAsync(id);
        }

        public virtual async Task<TGetOutputDto> MoveTree(TMoveInput input)
        {
            await CheckUpdatePolicyAsync();

            await TreeDomainService.MoveAsync(input.Id, input.NewParentId);

            return MapToGetOutputDto(await TreeRepository.GetAsync(input.Id));
        }
        protected virtual async Task CheckMovePolicyAsync()
        {
            await CheckPolicyAsync(MovePolicyName);
        }
    }
}
