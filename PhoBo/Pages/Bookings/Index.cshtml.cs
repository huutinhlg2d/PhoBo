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

namespace PhoBo.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public IndexModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public User currentUser { get; set; }

        public IList<Booking> Booking { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Debug.WriteLine($"[{this}]GET: ");

            currentUser = Auth.Auth.GetUser(HttpContext);

            if (currentUser == null)
            {
                return NotFound();
            }

            Booking = await _context.Booking
                .Include(b => b.Concept)
                .Include(b => b.Customer)
                .Include(b => b.Photographer).ToListAsync();

            return Page();
        }

        public void OnPost()
        {
            Debug.WriteLine($"[{this}]POST:");
        }
        
        public JsonResult OnPostCancel(int id)
        {
            Debug.WriteLine($"[{this}]POST: id={id}");

            Booking booking = _context.Booking.ToList().Find(b => b.Id == id);

            if(!booking?.State.Equals(BookingState.Waiting) ?? false) return new JsonResult(new {result = "ERROR", value="Id not found"});

            booking.State = BookingState.Canceled;
            _context.SaveChanges();

            return new JsonResult(new { result = "OK", value = "Canceled"});
        }

        public JsonResult OnPostAccept(int id)
        {
            Debug.WriteLine($"[{this}]POST: id={id}");

            Booking booking = _context.Booking.ToList().Find(b => b.Id == id);

            if (!booking?.State.Equals(BookingState.Waiting) ?? false) return new JsonResult(new { result = "ERROR", value = "Id not found" });

            booking.State = BookingState.Accepted;
            _context.SaveChanges();

            return new JsonResult(new { result = "OK", value = "Accepted" });
        }

        public JsonResult OnPostDecline(int id)
        {
            Debug.WriteLine($"[{this}]POST: id={id}");

            Booking booking = _context.Booking.ToList().Find(b => b.Id == id);

            if (!booking?.State.Equals(BookingState.Waiting) ?? false) return new JsonResult(new { result = "ERROR", value = "Id not found" });

            booking.State = BookingState.Declined;
            _context.SaveChanges();

            return new JsonResult(new { result = "OK", value = "Declined" });
        }

        //public JsonResult OnPostRate(int id, float rate)
        //{
        //    Debug.WriteLine($"[{this}]POST: id={id}");

        //    Booking booking = _context.Booking.ToList().Find(b => b.Id == id);

        //    Console.WriteLine($"{(booking?.State.Equals(BookingState.Accepted) ?? false)} {!(booking?.BookingRate.Equals(0) ?? false)}");

        //    if (!(booking?.State.Equals(BookingState.Accepted) ?? false) && !(booking?.BookingRate.Equals(0) ?? false))  return new JsonResult(new { result = "ERROR", value = "can not rate" });

        //    booking.BookingRate = rate;
        //    //_context.SaveChanges();

        //    return new JsonResult(new { result = "OK", value = rate });
        //}

        public IActionResult OnPostRate(int id, float rate)
        {
            Debug.WriteLine($"[{this}]POST: id={id}");

            Booking booking = _context.Booking.ToList().Find(b => b.Id == id);

            if ((booking?.State.Equals(BookingState.Accepted) ?? false) && (booking?.BookingRate.Equals(0) ?? false))
            {
                booking.BookingRate = rate;
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
