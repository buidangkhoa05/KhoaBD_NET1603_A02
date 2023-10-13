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
using MapsterMapper;

namespace KhoaBDRazorPage.Pages.Car
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [BindProperty]
        public CarInformationDto CarInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _unitOfWork.CarInformation.GetFirstOrDefault(new QueryHelper<CarInformation, CarInformationDto>()
            {
                Filter = t => t.CarId == id,
            });

            if (car == null)
            {
                return NotFound();
            }

            CarInformation = car;

            var manufacturers = await _unitOfWork.Manufacturer.Get(new QueryHelper<Manufacturer, ManufacturerDto>());
            var suppliers = await _unitOfWork.Supplier.Get(new QueryHelper<Supplier, SupplierDto>());

            ViewData["ManufacturerId"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "SupplierName");

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

            try
            {
                var createReult = await _unitOfWork.CarInformation.UpdateAsync(_mapper.Map<CarInformation>(CarInformation), true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CarInformationExists(CarInformation.CarId))
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

        private async Task<bool> CarInformationExists(int id)
        {
            return (await _unitOfWork.CarInformation.GetFirstOrDefault(new QueryHelper<CarInformation>() { Filter = t => t.CarId == id })) != null;
        }
    }
}
