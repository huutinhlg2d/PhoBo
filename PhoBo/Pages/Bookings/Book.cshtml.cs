using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoBo.Data;
using PhoBo.Models;

namespace PhoBo.Pages.Bookings
{
    public class BookModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public BookModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }
        public Photographer Photographer { get; set; }
        public List<SelectListItem> PhotographerConcepts { get; set; }

        public IActionResult OnGet(int id)
        {
            Debug.WriteLine("GET");
            if (!_context.Photographer.Where(x => x.Id == id).Any())
            {
                return NotFound();
            }

            User currentUser = Auth.Auth.GetUser(HttpContext);
            Customer = _context.Customer.ToList().Find(c => c.Id == currentUser.Id);
            Photographer = _context.Photographer.Where(x => x.Id == id).FirstOrDefault();

            List<BookingConceptConfig> bookingConceptConfigs = _context.BookingConceptConfig.Include(bcc => bcc.Concept)
                .Where(bck => bck.PhotographerId == id)
                .ToList();

            if(bookingConceptConfigs.Count == 0) return NotFound();

            PhotographerConcepts = bookingConceptConfigs.Select(bcc =>
                new SelectListItem
                {
                    Value = bcc.ConceptId.ToString(),
                    Text = bcc.Concept.Name
                }
            ).ToList();
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public async Task<IActionResult> OnPostAsync(int photographerId, int customerId)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Debug.WriteLine("POST");

            Booking.ConceptId = int.Parse(Request.Form["conceptId"]);
            Booking.CustomerId = customerId;
            Booking.PhotographerId = photographerId;
            Booking.Duration = float.Parse(Request.Form["duration"]);
            Booking.BookingDate = DateTime.Now;
            Booking.State = BookingState.Waiting;

            Debug.WriteLine($"DEBUG: {Booking.ConceptId} " +
                $"{Booking.CustomerId} " +
                $"{Booking.PhotographerId} " +
                $"{Booking.Duration} " +
                $"{Booking.Location} " +
                $"{Booking.Note} ");
            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return Redirect("?id=" + photographerId);
        }

        public PartialViewResult OnGetDuration(int conceptId, int photographerId)
        {
            User currentUser = Auth.Auth.GetUser(HttpContext);
            string durationConfig = _context.BookingConceptConfig.ToList()
                .Find(bcc => bcc.ConceptId == conceptId && bcc.PhotographerId == photographerId)
                .DurationConfig;
            List<string> durationConfigList = durationConfig.Split(':').ToList();

            return Partial("_BookingDurationPartial", durationConfigList);
        }
    }
}
