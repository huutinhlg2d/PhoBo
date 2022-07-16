using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhoBo.Data;
using PhoBo.Models;

namespace PhoBo.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public DetailsModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public User User { get; set; }
        public Photographer Photographer { get; set; }
        public int something { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.Id == id);

            Photographer = await _context.Photographer.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
