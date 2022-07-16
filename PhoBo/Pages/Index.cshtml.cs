using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoBo.Data;
using PhoBo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoBo.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly PhoBo.Data.PhoBoContext _context;
        public IndexModel(PhoBoContext logger)
        {
            _context = logger;
        }
        public IList<User> User { get; set; }
        public IList<Photographer> Photographers { get; set; }
        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
            Photographers = await _context.Photographer.ToListAsync();
        }
    }
}
