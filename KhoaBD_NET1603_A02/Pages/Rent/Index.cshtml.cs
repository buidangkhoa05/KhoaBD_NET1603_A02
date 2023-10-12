using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.Rent
{
    public class IndexModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public IndexModel(Domain.Models.FucarRentingManagementContext context)
        {
            _context = context;
        }

        public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RentingTransactions != null)
            {
                RentingTransaction = await _context.RentingTransactions
                .Include(r => r.Customer).ToListAsync();
            }
        }
    }
}
