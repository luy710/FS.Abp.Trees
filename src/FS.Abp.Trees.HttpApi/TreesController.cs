using FS.Abp.Trees.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FS.Abp.Trees
{
    public abstract class TreesController : AbpController
    {
        protected TreesController()
        {
            LocalizationResource = typeof(TreesResource);
        }
    }
}
