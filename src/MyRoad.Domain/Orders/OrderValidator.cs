using FluentValidation;

namespace MyRoad.Domain.Orders;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID cannot be empty");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order date cannot be empty");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity cannot be empty")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price cannot be negative");

        RuleFor(x => x.Notes)
            .NotEmpty().When(x => x.TotalDueAmount > 10_000)
            .WithMessage("Notes is required for large orders");

        RuleFor(x => x.OrderDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Order date must be in the past or today");
    }
}