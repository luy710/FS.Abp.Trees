using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using FS.Abp.Trees.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FS.Abp.Trees.Pages
{
    public abstract class TreesPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<TreesResource> L { get; set; }
    }
}
