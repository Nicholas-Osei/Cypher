using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cypher.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class DeactivatedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}