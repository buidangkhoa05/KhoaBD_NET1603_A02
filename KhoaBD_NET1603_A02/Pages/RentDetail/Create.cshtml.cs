using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.RentDetail
{
    public class CreateModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public CreateModel(Domain.Models.FucarRentingManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CarId"] = new SelectList(_context.CarInformations, "CarId", "CarName");
        ViewData["RentingTransactionId"] = new SelectList(_context.RentingTransactions, "RentingTransationId", "RentingTransationId");
            return Page();
        }

        [BindProperty]
        public RentingDetail RentingDetail { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RentingDetails == null || RentingDetail == null)
            {
                return Page();
            }

            _context.RentingDetails.Add(RentingDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
