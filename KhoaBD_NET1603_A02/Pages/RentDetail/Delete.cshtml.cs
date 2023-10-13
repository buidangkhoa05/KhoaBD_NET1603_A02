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
    public class DeleteModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public DeleteModel(Domain.Models.FucarRentingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RentingDetail RentingDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RentingDetails == null)
            {
                return NotFound();
            }

            var rentingdetail = await _context.RentingDetails.FirstOrDefaultAsync(m => m.RentingTransactionId == id);

            if (rentingdetail == null)
            {
                return NotFound();
            }
            else 
            {
                RentingDetail = rentingdetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RentingDetails == null)
            {
                return NotFound();
            }
            var rentingdetail = await _context.RentingDetails.FindAsync(id);

            if (rentingdetail != null)
            {
                RentingDetail = rentingdetail;
                _context.RentingDetails.Remove(RentingDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
