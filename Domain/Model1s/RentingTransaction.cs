using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Domain.Models;

public partial class RentingTransaction
{
    public int RentingTransationId { get; set; }

    public DateTime? RentingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? RentingStatus { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<RentingDetail> RentingDetails { get; set; } = new List<RentingDetail>();
}


public class RentingTransactionValidator : AbstractValidator<RentingTransaction>
{
    public RentingTransactionValidator()
    {
        RuleFor(x => x.RentingTransationId)
            .GreaterThanOrEqualTo(-1).WithMessage("Rent transaction ID must be greater than or equal to -1.");

        RuleFor(x => x.RentingDate)
            .NotEmpty().WithMessage("Renting date cannot be empty.");

        RuleFor(x => x.TotalPrice)
            .NotEmpty().WithMessage("Total rent price cannot be empty.");

        RuleFor(x => x.CustomerId)
            .GreaterThanOrEqualTo(-1).WithMessage("Customer ID must be greater than or equal to -1.");

        RuleFor(x => x.RentingStatus)
            .NotEmpty().WithMessage("Renting status cannot be empty.");
    }
}
