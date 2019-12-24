using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using FS.Abp.Trees.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization.Resources.AbpValidation;
using Volo.Abp.VirtualFileSystem;

namespace FS.Abp.Trees
{
    [DependsOn(
        typeof(AbpLocalizationModule)
    )]
    public class TreesDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TreesDomainSharedModule>("FS.Abp.Trees");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<TreesResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Trees");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Trees", typeof(TreesResource));
            });
        }
    }
}
