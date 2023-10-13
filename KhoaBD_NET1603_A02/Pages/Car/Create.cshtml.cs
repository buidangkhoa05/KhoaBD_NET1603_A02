using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;
using PRN221.Domain.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KhoaBDRazorPage.Pages.Car
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGet()
        {

            try
            {
                var manufacturers = await _unitOfWork.Manufacturer.Get(new QueryHelper<Manufacturer, ManufacturerDto>());
                var suppliers = await _unitOfWork.Supplier.Get(new QueryHelper<Supplier, SupplierDto>());

                ViewData["ManufacturerId"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
                ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "SupplierName");
                return Page();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [BindProperty]
        public CarInformationDto CarInformation { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || CarInformation == null)
            {
                return Page();
            }

            try
            {
                var resultCreate = await _unitOfWork.CarInformation.CreateAsync(_mapper.Map<CarInformation>(CarInformation), true);
            }
            catch (Exception)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}
