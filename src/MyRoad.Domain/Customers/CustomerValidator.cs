using FluentValidation;

namespace MyRoad.Domain.Customers;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name cannot be empty");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number cannot be empty")
            .MaximumLength(15);
    }
}