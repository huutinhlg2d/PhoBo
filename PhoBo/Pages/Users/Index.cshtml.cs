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
    public class IndexModel : PageModel
    {
        private readonly PhoBo.Data.PhoBoContext _context;

        public IndexModel(PhoBo.Data.PhoBoContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
