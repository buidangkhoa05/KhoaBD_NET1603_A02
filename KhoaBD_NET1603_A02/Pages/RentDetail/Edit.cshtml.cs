using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.RentDetail
{
    public class EditModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public EditModel(Domain.Models.FucarRentingManagementContext context)
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

            var rentingdetail =  await _context.RentingDetails.FirstOrDefaultAsync(m => m.RentingTransactionId == id);
            if (rentingdetail == null)
            {
                return NotFound();
            }
            RentingDetail = rentingdetail;
           ViewData["CarId"] = new SelectList(_context.CarInformations, "CarId", "CarName");
           ViewData["RentingTransactionId"] = new SelectList(_context.RentingTransactions, "RentingTransationId", "RentingTransationId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RentingDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentingDetailExists(RentingDetail.RentingTransactionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RentingDetailExists(int id)
        {
          return (_context.RentingDetails?.Any(e => e.RentingTransactionId == id)).GetValueOrDefault();
        }
    }
}
