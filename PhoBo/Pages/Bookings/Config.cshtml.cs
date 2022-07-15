using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoBo.Data;
using PhoBo.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhoBo.Pages.Bookings
{
    public class ConfigModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public ConfigModel(PhoBoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Photographer Photographer { get; set; }
        public List<BookingConceptConfig> BookingConceptConfigs { get; set; } 
        public IActionResult OnGet(int id)
        {
            Debug.WriteLine("GET");
            if (!_context.Photographer.Where(x => x.Id == id).Any())
            {
                return NotFound();
            }

            Photographer = _context.Photographer.Where(x => x.Id == id).FirstOrDefault();

            BookingConceptConfigs = _context.BookingConceptConfig.Include(bcc => bcc.Concept)
                .Where(bcc => bcc.PhotographerId == id)
                .ToList();

            return Page();
        }

        public JsonResult OnGetUpdateDurationConfig(int id, string duration)
        {
            try
            {
                List<BookingConceptConfig> bookingConceptConfigs = _context.BookingConceptConfig.Where(bcc => bcc.Id == id).ToList();
                if (bookingConceptConfigs.Any())
                {
                    bookingConceptConfigs[0].DurationConfig = duration;
                    _context.SaveChanges();
                }
                else
                {
                    return new JsonResult("id not found");
                }
            }
            catch (System.Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult("change successful");
        }

        public JsonResult OnGetDeleteDurationConfig(int id)
        {
            try
            {
                List<BookingConceptConfig> bookingConceptConfigs = _context.BookingConceptConfig.Where(bcc => bcc.Id == id).ToList();
                if (bookingConceptConfigs.Any())
                {
                    _context.Remove(bookingConceptConfigs[0]);
                    _context.SaveChanges();
                }
                else
                {
                    return new JsonResult("id not found");
                }
            }
            catch (System.Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult("delete successful");
        }

        public PartialViewResult OnGetAddConceptConfig(int photographerId)
        {
            System.Console.WriteLine("CONFIG");

            List<SelectListItem> conceptItems = _context.Concept.Where(c => !_context.BookingConceptConfig
                    .Where(bcc => bcc.PhotographerId == photographerId && bcc.ConceptId == c.Id)
                    .Any()
                 )
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return Partial("_AddConceptConfigPartial", conceptItems);
        }

        public JsonResult OnPost()
        {
            System.Console.WriteLine("photographerId " + Photographer.Id);
            BookingConceptConfig bookingConceptConfig = new BookingConceptConfig
            {
                ConceptId = int.Parse(Request.Form["conceptId"]),
                PhotographerId = Photographer.Id,
                DurationConfig = Request.Form["duration"]
            };

            _context.BookingConceptConfig.Add(bookingConceptConfig);
            _context.SaveChanges();

            return new JsonResult("add successful");
        }
    }
}
