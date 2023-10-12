using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.Car
{
    public class DetailsModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public DetailsModel(Domain.Models.FucarRentingManagementContext context)
        {
            _context = context;
        }

      public CarInformation CarInformation { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CarInformations == null)
            {
                return NotFound();
            }

            var carinformation = await _context.CarInformations.FirstOrDefaultAsync(m => m.CarId == id);
            if (carinformation == null)
            {
                return NotFound();
            }
            else 
            {
                CarInformation = carinformation;
            }
            return Page();
        }
    }
}
