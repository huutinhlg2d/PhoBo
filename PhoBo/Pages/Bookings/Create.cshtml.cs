using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoBo.Data;
using PhoBo.Models;

namespace PhoBo.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public CreateModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ConceptId"] = new SelectList(_context.Concept, "Id", "Name");
        ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email");
        ViewData["PhotographerId"] = new SelectList(_context.Photographer, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
