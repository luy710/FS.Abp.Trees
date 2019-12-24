using FS.Abp.Trees.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FS.Abp.Trees.Pages
{
    public abstract class TreesPageModel : AbpPageModel
    {
        protected TreesPageModel()
        {
            LocalizationResourceType = typeof(TreesResource);
        }
    }
}