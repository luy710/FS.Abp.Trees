using FS.Abp.Trees.Localization;
using Volo.Abp.Application.Services;

namespace FS.Abp.Trees
{
    public abstract class TreesAppService : ApplicationService
    {
        protected TreesAppService()
        {
            LocalizationResource = typeof(TreesResource);
        }
    }
}
