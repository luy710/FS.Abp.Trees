using Volo.Abp.Modularity;

namespace FS.Abp.Trees
{
    [DependsOn(
        typeof(TreesDomainSharedModule)
        )]
    public class TreesDomainModule : AbpModule
    {

    }
}
