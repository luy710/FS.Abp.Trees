using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Linq;
using Volo.Abp.DependencyInjection;

namespace FS.Abp.Trees
{
    public interface ITreeDomainService<TEntity> : IDomainService, ITransientDependency
    where TEntity : class, ITree<TEntity>, IEntity<Guid>

    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task MoveAsync(Guid id, Guid? parentId);

    }
    public class TreeDomainService<TEntity> : DomainService, ITreeDomainService<TEntity>
        where TEntity : class, ITree<TEntity>, IEntity<Guid>
    {
        protected IRepository<TEntity, Guid> EntityRepository { get; set; }
        protected TreeCodeDomainService TreeCodeDomainService { get; set; }

        public TreeDomainService(
            IRepository<TEntity, Guid> entityRepository,
            TreeCodeDomainService treeCodeDomainService
            )
        {
            EntityRepository = entityRepository;
            TreeCodeDomainService = treeCodeDomainService;
        }


        public async Task CreateAsync(TEntity entity)
        {
            entity.Code = await GetNextChildCodeAsync(entity.ParentId);
            await ValidateEntityAsync(entity);
            await EntityRepository.InsertAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var children = await FindChildrenAsync(id, true);

            foreach (var child in children)
            {
                await EntityRepository.DeleteAsync(child);
            }

            await EntityRepository.DeleteAsync(id);
        }

        public async Task MoveAsync(Guid id, Guid? parentId)
        {
            var entity = await EntityRepository.GetAsync(id);
            if (entity.ParentId == parentId)
            {
                return;
            }

            //Should find children before Code change
            var children = await FindChildrenAsync(id, true);

            //Store old code of OU
            var oldCode = entity.Code;

            //Move OU
            entity.Code = await GetNextChildCodeAsync(parentId);
            entity.ParentId = parentId;

            await ValidateEntityAsync(entity);

            //Update Children Codes
            foreach (var child in children)
            {
                child.Code = TreeCodeDomainService.AppendCode(entity.Code, TreeCodeDomainService.GetRelativeCode(child.Code, oldCode));
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await ValidateEntityAsync(entity);
            await EntityRepository.UpdateAsync(entity);
        }

        public async Task<TEntity> GetLastChildOrNullAsync(Guid? parentId)
        {

            var children = await EntityRepository.Where(tree => tree.ParentId == parentId).ToListAsync();
            return children.OrderBy(c => c.Code).LastOrDefault();
        }
        public async Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId);
            if (lastChild == null)
            {
                var parentCode = parentId != null ? await GetCodeAsync(parentId.Value) : null;
                return TreeCodeDomainService.AppendCode(parentCode, TreeCodeDomainService.CreateCode(1));
            }

            return TreeCodeDomainService.CalculateNextCode(lastChild.Code);
        }
        public async Task<string> GetCodeAsync(Guid id)
        {
            return (await EntityRepository.GetAsync(id)).Code;
        }
        protected virtual async Task ValidateEntityAsync(TEntity entity)
        {

            var siblings = (await FindChildrenAsync(entity.ParentId))
                .Where(ou => ou.Id != entity.Id)
                .ToList();

            if (siblings.Any(ou => ou.DisplayName == entity.DisplayName))
            {
                throw new Exception("DuplicateDisplayNameWarning");
            }
        }
        public async Task<List<TEntity>> FindChildrenAsync(Guid? parentId, bool recursive = false)
        {
            if (!recursive)
            {
                return await EntityRepository.Where(x => x.ParentId == parentId).ToListAsync();
            }

            if (!parentId.HasValue)
            {
                return await EntityRepository.ToListAsync();
            }

            var code = await GetCodeAsync(parentId.Value);

            return await EntityRepository.Where(
                ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value).ToListAsync();
        }
    }
    //move to core
    public static class TreeDomainServiceEx
    {
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query)
        {
            return Volo.Abp.Linq.DefaultAsyncQueryableExecuter.Instance.ToListAsync(query);
        }
    }
}
