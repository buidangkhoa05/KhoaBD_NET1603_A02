using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;
using Team5.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KhoaBDRazorPage.Pages.Car
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string SearchText { get; set; } = null;

        public PagedList<CarInformationViewDto> CarInformationPaged { get; set; } = default!;

        public async Task OnGetAsync(int? pageNumber, string searchText = "")
        {
            var query = new QueryHelper<CarInformation, CarInformationViewDto>()
            {
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
                },
                PaginationParams = new PagingParameters(pageNumber, 5),
                Filter = t => t.CarName.Contains(searchText)
            };

            try
            {
                var carInformations = await _unitOfWork.CarInformation.GetWithPagination(query);

                CarInformationPaged = carInformations;

                if (!string.IsNullOrEmpty(searchText))
                {
                    SearchText = searchText;
                }
            }
            catch (Exception)
            {
            }
        }

        public async Task OnGetSearchAsync(int? pageNumber, string searchText)
        {
            var query = new QueryHelper<CarInformation, CarInformationViewDto>()
            {
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
                },
                PaginationParams = new PagingParameters(pageNumber, 5),
                Filter = t => t.CarName.Contains(searchText)
            };

            try
            {
                var carInformations = await _unitOfWork.CarInformation.GetWithPagination(query);

                CarInformationPaged = carInformations;
            }
            catch (Exception)
            {
            }
        }
    }
}
