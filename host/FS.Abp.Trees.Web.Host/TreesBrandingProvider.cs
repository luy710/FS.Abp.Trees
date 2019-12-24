using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace FS.Abp.Trees
{
    [Dependency(ReplaceServices = true)]
    public class TreesBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Trees";
    }
}
