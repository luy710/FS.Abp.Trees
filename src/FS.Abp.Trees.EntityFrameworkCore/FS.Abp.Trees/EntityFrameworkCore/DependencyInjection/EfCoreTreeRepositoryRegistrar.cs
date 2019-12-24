using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Reflection;

namespace FS.Abp.Trees.EntityFrameworkCore.DependencyInjection
{
    internal static class DbContextHelper
    {
        public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return
                from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) &&
                    typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
                select property.PropertyType.GenericTypeArguments[0];
        }
    }
    public class EfCoreTreeRepositoryRegistrar : RepositoryRegistrarBase<AbpDbContextRegistrationOptions>
    {
        public EfCoreTreeRepositoryRegistrar(AbpDbContextRegistrationOptions options)
            :base(options)
        {
            
        }
        public override void AddRepositories()
        {
            RegisterDefaultRepositories();
        }
        protected override void RegisterDefaultRepository(Type entityType)
        {
            var repositoryImplementationType = GetDefaultRepositoryImplementationType(entityType);

            var treeRepositoryInterface = typeof(ITreeRepository<>).MakeGenericType(entityType);
            if (treeRepositoryInterface.IsAssignableFrom(repositoryImplementationType))
            {
                Options.Services.TryAddTransient(treeRepositoryInterface, repositoryImplementationType);
            }
        }

        protected override Type GetDefaultRepositoryImplementationType(Type entityType)
        {
            return GetRepositoryType(Options.DefaultRepositoryDbContextType, entityType);
        }
        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return DbContextHelper.GetEntityTypes(dbContextType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(EfCoreTreeRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
        {
            throw new NotImplementedException();
        }
        protected override bool ShouldRegisterDefaultRepositoryFor(Type entityType)
        {
            var isTreeEntity = entityType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(FS.Abp.Trees.ITree<>));

            return isTreeEntity;
        }
    }
}
