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
    public class TreeAppService<TEntity, TGetOutputDto, TGetListOutputDto, TGetListInput, TCreateInput, TUpdateInput, TMoveInput>
        : CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, Guid, TGetListInput, TCreateInput, TUpdateInput>,
        ITreeAppService<TGetOutputDto, TCreateInput, TUpdateInput, TMoveInput> where TEntity : class, ITree<TEntity>, IEntity<Guid>, new()
        where TGetOutputDto : ITreeDto
        where TGetListOutputDto : ITreeDto
        where TGetListInput : IGetListInput
        where TCreateInput : ICreateInput
        where TUpdateInput : IUpdateInput
        where TMoveInput : IMoveInput
    {
        protected ITreeRepository<TEntity> TreeRepository { get; }

        protected virtual string MovePolicyName { get; }

        public TreeAppService(
            ITreeRepository<TEntity> treeRepository
            )
            : base(treeRepository)
        {
            this.TreeRepository = treeRepository;
        }
        public virtual async Task<TGetOutputDto> MoveAsync(TMoveInput input)
        {
            var entity = await this.TreeRepository.GetAsync(input.Id);

            await CheckUpdatePolicyAsync();

            await TreeRepository.MoveAsync(entity, input.NewParentId);

            return MapToGetOutputDto(await this.TreeRepository.GetAsync(input.Id));
        }
        protected virtual async Task CheckMovePolicyAsync()
        {
            await CheckPolicyAsync(MovePolicyName);
        }
    }
}
