using PRN221.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CarInformationViewDto
    {
        public int CarId { get; set; }

        public string CarName { get; set; } = null!;

        public string? CarDescription { get; set; }

        public int? NumberOfDoors { get; set; }

        public int? SeatingCapacity { get; set; }

        public string? FuelType { get; set; }

        public int? Year { get; set; }

        public byte? CarStatus { get; set; }

        public decimal? CarRentingPricePerDay { get; set; }

        public string Manufacturer { get; set; } = null!;

        public string Supplier { get; set; } = null!;
    }
}
