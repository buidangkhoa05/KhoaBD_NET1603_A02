using PRN221.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ManufacturerDto
    {
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; } = null!;

        public string? Description { get; set; }

        public string? ManufacturerCountry { get; set; }
    }


    public class ManufacturerValidator : AbstractValidator<ManufacturerDto>
    {
        public ManufacturerValidator()
        {
            RuleFor(x => x.ManufacturerId)
        .GreaterThanOrEqualTo(-1).WithMessage("ManufacturerId >= -1.");

            RuleFor(x => x.ManufacturerName)
                .Length(0, 25).WithMessage("ManufacturerName <= 25 character");

            RuleFor(x => x.Description)
                .Length(0, 125).WithMessage("Description <= 125 character");

            RuleFor(x => x.ManufacturerCountry)
                .NotEmpty().WithMessage("ManufacturerCountry is not empty");   
        }
    }
}
