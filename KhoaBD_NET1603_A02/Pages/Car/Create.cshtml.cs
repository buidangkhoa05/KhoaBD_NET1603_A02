using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.Car
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
        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            return Page();
        }

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CarInformations == null || CarInformation == null)
            {
                return Page();
            }

            _context.CarInformations.Add(CarInformation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
