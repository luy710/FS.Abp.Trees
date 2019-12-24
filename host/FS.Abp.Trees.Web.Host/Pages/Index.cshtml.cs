using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace FS.Abp.Trees.Pages
{
    public class IndexModel : TreesPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}