using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhoBo.Data;
using PhoBo.Models;

namespace PhoBo.Pages.Bookings
{
    public class DetailsModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public DetailsModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await _context.Booking
                .Include(b => b.Concept)
                .Include(b => b.Customer)
                .Include(b => b.Photographer).FirstOrDefaultAsync(m => m.Id == id);

            if (Booking == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
