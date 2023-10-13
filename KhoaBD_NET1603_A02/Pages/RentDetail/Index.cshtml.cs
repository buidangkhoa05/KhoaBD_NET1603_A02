using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.RentDetail
{
    public class IndexModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public IndexModel(Domain.Models.FucarRentingManagementContext context)
        {
            _context = context;
        }

        public IList<RentingDetail> RentingDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RentingDetails != null)
            {
                RentingDetail = await _context.RentingDetails
                .Include(r => r.Car)
                .Include(r => r.RentingTransaction).ToListAsync();
            }
        }
    }
}
