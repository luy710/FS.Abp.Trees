using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace FS.Abp.Trees
{
    [DependsOn(
        typeof(TreesHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class TreesConsoleApiClientModule : AbpModule
    {
        
    }
}
