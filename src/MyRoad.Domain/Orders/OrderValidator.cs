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

        RuleFor(x => x.CreatedByUserId)
            .NotEmpty()
            .WithMessage("Created by user ID cannot be empty");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price cannot be negative");
        
        RuleFor(x => x.Notes)
            .NotEmpty().When(x => x.TotalDueAmount > 10_000)
            .WithMessage("Notes is required for large orders");
    }
}