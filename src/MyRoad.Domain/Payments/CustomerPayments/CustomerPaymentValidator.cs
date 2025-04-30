using FluentValidation;

namespace MyRoad.Domain.Payments.CustomerPayments;

public class CustomerPaymentValidator : AbstractValidator<CustomerPayment>
{
    public CustomerPaymentValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID cannot be empty");
    }
}