using FluentValidation;

namespace MyRoad.Domain.Payments;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(x => x.PaymentDate)
            .NotEmpty()
            .WithMessage("Payment date cannot be empty");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount cannot be empty")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Amount must be greater than 0")
            .LessThanOrEqualTo(1_000_000).WithMessage("Amount cannot exceed 1,000,000")
            .Must(a => decimal.Round(a, 2) == a).WithMessage("Amount can only have up to 2 decimal places");

        RuleFor(x => x.Notes)
            .NotEmpty().When(x => x.Amount > 10_000)
            .WithMessage("Note is required for large payments");
    }
}