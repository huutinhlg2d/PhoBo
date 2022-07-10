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
    public class IndexModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public IndexModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.Concept)
                .Include(b => b.Customer)
                .Include(b => b.Photographer).ToListAsync();
        }
    }
}
