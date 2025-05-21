using FluentValidation;

namespace MyRoad.Domain.Purchases
{
    public class PurchaseValidator : AbstractValidator<Purchase>
    {
        public PurchaseValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty()
                .WithMessage("Supplier Id Cannot be empty!");

            RuleFor(x => x.PurchasesDate)
                .NotEmpty()
                .WithMessage("Purchase date cannot be empty");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity cannot be empty")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price cannot be negative");

            RuleFor(x => x.PurchasesDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Purchase date must be in the past or today");
        }
    }
}