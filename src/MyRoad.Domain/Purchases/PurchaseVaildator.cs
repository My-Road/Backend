using FluentValidation;

namespace MyRoad.Domain.Purchases
{
    public class PurchaseVaildator : AbstractValidator<Purchase>
    {
        public PurchaseVaildator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty()
                .WithMessage("SupplierId");

            RuleFor(x => x.PurchasesDate)
                .NotEmpty()
                .WithMessage("Purchase date cannot be empty");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity cannot be empty")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty()
                .WithMessage("Created by user ID cannot be empty");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price cannot be negative");
        }
    }
}
