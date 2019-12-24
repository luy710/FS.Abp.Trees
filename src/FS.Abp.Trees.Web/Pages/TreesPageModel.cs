using FS.Abp.Trees.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FS.Abp.Trees.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class TreesPageModel : AbpPageModel
    {
        protected TreesPageModel()
        {
            LocalizationResourceType = typeof(TreesResource);
        }
    }
}