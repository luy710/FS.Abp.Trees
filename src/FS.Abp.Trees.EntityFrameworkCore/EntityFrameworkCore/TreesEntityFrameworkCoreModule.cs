using FS.Abp.Trees.EntityFrameworkCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Modularity;

namespace FS.Abp.Trees.EntityFrameworkCore
{
    [DependsOn(
        typeof(TreesDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class TreesEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //new EfCoreTreeRepositoryRegistrar(options).AddRepositories();
            context.Services.AddAbpDbContext<TreesDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

            });
        }
    }
}