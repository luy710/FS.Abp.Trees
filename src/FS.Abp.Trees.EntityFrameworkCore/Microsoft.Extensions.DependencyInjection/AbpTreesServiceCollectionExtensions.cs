using System;
using FS.Abp.Trees.EntityFrameworkCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AbpTreesServiceCollectionExtensions
    {
        public static IServiceCollection AddTreeRepository<TDbContext>(
            this IServiceCollection services, 
            Action<IAbpDbContextRegistrationOptionsBuilder> optionsBuilder = null)
            where TDbContext : AbpDbContext<TDbContext>
        {
            var options = new AbpDbContextRegistrationOptions(typeof(TDbContext), services);

            optionsBuilder?.Invoke(options);

            new EfCoreTreeRepositoryRegistrar(options).AddRepositories();

            return services;
        }
    }
}
