using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PhoBo.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhoBo.Pages
{
    public class LoginModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public LoginModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
            HttpContext?.Session.SetString("user", "");
            HttpContext?.Session.Remove("user");
        }

        public string Message { get; set; }

        [BindProperty]
        public Login Login { get; set; }

        public IActionResult OnPost()
        {
            Debug.WriteLine($"E:{Login.Email} P:{Login.Password}");
            if(_context.User.ToList().Exists(u => u.Email == Login.Email && u.Password == Login.Password))
            {
                User user = _context.User.ToList().Find(u => u.Email == Login.Email);
                var userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("user", userJson);
                return RedirectToPage("Index");
            } else
            {
                Message = "Invalid username or password.";
                return Page();
            }
        }
    }
}
