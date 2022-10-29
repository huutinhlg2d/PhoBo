using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoBo.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            Auth.Auth.Logout(HttpContext);

            return RedirectToPage("/Index");
        }
    }
}
