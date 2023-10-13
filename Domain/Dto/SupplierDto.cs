using PRN221.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = null!;

        public string? SupplierDescription { get; set; }

        public string? SupplierAddress { get; set; }

    }

    public class SupplierValidator : AbstractValidator<SupplierDto>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThanOrEqualTo(-1).WithMessage("Supplier ID must be greater than or equal to -1.");

            RuleFor(x => x.SupplierName)
                .Length(0, 25).WithMessage("Supplier name must not exceed 25 characters.");

            RuleFor(x => x.SupplierDescription)
                .Length(0, 125).WithMessage("Description must not exceed 125 characters.");

            RuleFor(x => x.SupplierAddress)
                .Length(0, 40).WithMessage("Supplier address must not exceed 40 characters.");
        }
    }

}
