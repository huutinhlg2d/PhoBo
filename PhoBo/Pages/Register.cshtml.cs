using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhoBo.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PhoBo.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public RegisterModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public Register Register { get; set; }

        public List<SelectListItem> registerRoles = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = UserRole.Customer.ToString(),
                Text = UserRole.Customer.ToString(),
            },
            new SelectListItem()
            {
                Value = UserRole.Photographer.ToString(),
                Text = UserRole.Photographer.ToString()
            }
        };

        public void OnGet()
        {
            Debug.WriteLine("GET:");
            Debug.WriteLine("GET: Register = " + JsonConvert.SerializeObject(Register));
            Debug.WriteLine("GET: Register = " + JsonConvert.SerializeObject(registerRoles));
            if (Register != null)
            {
                registerRoles.Find(sli => sli.Value == Register.Role.ToString()).Selected = true;
            }
        }

        public IActionResult OnPost()
        {
            Debug.WriteLine("POST: Register = " + JsonConvert.SerializeObject(Register));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            User user = Register.GetUser();

            if (user.Role == UserRole.Photographer) user.Role = UserRole.PendingPhotographer;

            Debug.WriteLine("POST: user = " + JsonConvert.SerializeObject(user));

            _context.User.Add(user);
            _context.SaveChanges();

            return RedirectToPage("./Login");
        }
    }
}
