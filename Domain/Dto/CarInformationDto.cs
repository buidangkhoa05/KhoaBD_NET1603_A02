using PRN221.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CarInformationDto
    {
        public int CarId { get; set; }

        public string CarName { get; set; } = null!;

        public string? CarDescription { get; set; }

        public int? NumberOfDoors { get; set; }

        public int? SeatingCapacity { get; set; }

        public string? FuelType { get; set; }

        public int? Year { get; set; }

        public int ManufacturerId { get; set; }

        public int SupplierId { get; set; }

        public byte? CarStatus { get; set; }

        public decimal? CarRentingPricePerDay { get; set; }
    }

    public class CarInformationValidator : AbstractValidator<CarInformationDto>
    {
        public CarInformationValidator()
        {
            //RuleFor(x => x.CarId)
            //    .GreaterThanOrEqualTo(-1).WithMessage("Car ID must be greater than or equal to -1.");

            RuleFor(x => x.CarName)
                .Length(0, 25).WithMessage("Car name must not exceed 25 characters.")
                .NotEmpty().WithMessage("Car name is required.");

            RuleFor(x => x.CarDescription)
                .Length(0, 110).WithMessage("Car description must not exceed 110 characters.");

            RuleFor(x => x.NumberOfDoors)
                .NotEmpty().WithMessage("Number of doors cannot be empty.");

            RuleFor(x => x.SeatingCapacity)
                .NotEmpty().WithMessage("Seating capacity cannot be empty.");

            RuleFor(x => x.FuelType)
                .Length(0, 10).WithMessage("Fuel type must not exceed 10 characters.")
                .NotNull().NotEmpty().WithMessage("Fuel type is required.");

            RuleFor(x => x.Year)
                .NotNull().NotEmpty().WithMessage("Production year cannot be empty.");

            RuleFor(x => x.ManufacturerId)
                .GreaterThanOrEqualTo(-1).WithMessage("Manufacturer ID must be greater than or equal to -1.");

            RuleFor(x => x.SupplierId)
                .GreaterThanOrEqualTo(-1).WithMessage("Supplier ID must be greater than or equal to -1.");

            RuleFor(x => x.CarStatus)
                .NotEmpty().WithMessage("Car status cannot be empty.");

            RuleFor(x => x.CarRentingPricePerDay)
                .NotEmpty().WithMessage("Daily rental price cannot be empty.");
        }
    }

}
