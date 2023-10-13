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
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public CarInformationViewDto CarInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carinformation = await _unitOfWork.CarInformation.GetFirstOrDefault(new QueryHelper<CarInformation, CarInformationViewDto>()
            {
                Filter = t => t.CarId == id,
                Selector = t => new CarInformationViewDto
                {
                    CarDescription = t.CarDescription,
                    CarId = t.CarId,
                    CarName = t.CarName,
                    CarRentingPricePerDay = t.CarRentingPricePerDay,
                    CarStatus = t.CarStatus,
                    FuelType = t.FuelType,
                    NumberOfDoors = t.NumberOfDoors,
                    SeatingCapacity = t.SeatingCapacity,
                    Year = t.Year,
                    Manufacturer = t.Manufacturer.ManufacturerName,
                    Supplier = t.Supplier.SupplierName,
                },
                Includes = new System.Linq.Expressions.Expression<Func<CarInformation, object>>[]
                {
                    t => t.Manufacturer,
                    t => t.Supplier
                }
            });

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                if (await ValidateCar(id))
                {
                    var deletdResult = await _unitOfWork.CarInformation.DeleteAsync(t => t.CarId == id);
                }
                else
                {
                    await _unitOfWork.CarInformation.UpdateAsync(t => t.CarId == id, setter => setter.SetProperty(t => t.CarStatus, byte.Parse("0")));
                }
            }

            catch (Exception)
            {
            }

            return RedirectToPage("./Index");
        }

        public async Task<bool> ValidateCar(int? id)
        {
            try
            {
                var car = await _unitOfWork.CarInformation.GetFirstOrDefault(new QueryHelper<CarInformation>()
                {
                    Filter = t => t.CarId == id,
                    Includes = new System.Linq.Expressions.Expression<Func<CarInformation, object>>[]
                    {
                        t => t.RentingDetails
                    }
                });

                if (car.RentingDetails.Any())
                {
                    return false;
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
