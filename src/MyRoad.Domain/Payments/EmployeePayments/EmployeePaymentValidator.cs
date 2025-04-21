using FluentValidation;

namespace MyRoad.Domain.Payments.EmployeePayments;

public class EmployeePaymentValidator : AbstractValidator<EmployeePayment>
{
    public EmployeePaymentValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .WithMessage("Employee Id cannot be empty");
    }
}