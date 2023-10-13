using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class PickCarDto
    {
        //public CarInformationDto Car { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public bool IsPicked { get; set; } = false;
        public int CardId { get; set; }
        public string CarName { get; set; } = null!;
        public int? NumberOfDoors { get; set; }
        public decimal? CarRentingPricePerDay { get; set; }
        public int? SeatingCapacity { get; set; }
        public string? FuelType { get; set; }

    }
}
