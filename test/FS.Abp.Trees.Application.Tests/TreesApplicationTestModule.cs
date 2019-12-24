using Volo.Abp.Modularity;

namespace FS.Abp.Trees
{
    [DependsOn(
        typeof(TreesApplicationModule),
        typeof(TreesDomainTestModule)
        )]
    public class TreesApplicationTestModule : AbpModule
    {

    }
}
