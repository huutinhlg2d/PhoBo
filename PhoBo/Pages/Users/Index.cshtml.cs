using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhoBo.Data;
using PhoBo.Models;

namespace PhoBo.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public IndexModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            User currentUser = Auth.Auth.GetUser(HttpContext);
            if (currentUser == null || currentUser.Role != UserRole.Admin)
            {
                return NotFound();
            }

            User = await _context.User.ToListAsync();

            return Page();
        }

        public JsonResult OnPostAccept(int id)
        {
            Debug.WriteLine($"[{this}]POST: id={id}");

            User user = _context.User.ToList().Find(b => b.Id == id);

            if (!user?.Role.Equals(UserRole.PendingPhotographer) ?? false) return new JsonResult(new { result = "ERROR", value = "Action fail" });

            user.Role = UserRole.Photographer;

            Photographer photographer = new Photographer(user);
            _context.Photographer.Add(photographer);
            _context.User.Remove(user);
            _context.SaveChanges();

            return new JsonResult(new { result = "OK", value = "Photographer"});
        }
    }
}
