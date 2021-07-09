using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tankerz.Web.Pages.Errors
{
    [Authorize]
    public class _404Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
