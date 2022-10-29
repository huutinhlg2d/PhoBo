using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhoBo.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PhoBo.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        private IHostingEnvironment _environment;
        public RegisterModel(PhoBo.Data.PhoBoContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

        public async Task<IActionResult> OnPost()
        {
            Debug.WriteLine("POST: Register = " + JsonConvert.SerializeObject(Register));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            User user = Register.GetUser();

            if (user.Role == UserRole.Photographer)
            {
                user.Role = UserRole.PendingPhotographer;
                _context.User.Add(user);
            }
            else
            {
                _context.Customer.Add(new Customer(user));
            }

            Debug.WriteLine("POST: user = " + JsonConvert.SerializeObject(user));

            if(user.AvatarUrl != null)
            {
                System.Console.WriteLine($"{_environment.WebRootPath} {user.AvatarUrl}");
                var file = Path.GetFullPath(user.AvatarUrl,_environment.WebRootPath);
                System.Console.WriteLine(file);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Register.AvatarFile.CopyToAsync(fileStream);
                }
            }

            _context.SaveChanges();


            return RedirectToPage("./Login");
        }
    }
}
