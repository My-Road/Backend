using FluentValidation;

namespace MyRoad.Domain.Employees
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required");

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .WithMessage("Job title cannot be empty");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{10}$").When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Phone number is not valid,must be exactly 10 digits");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Start date must be in the past or today");

            RuleFor(x => x.TotalDueAmount)
                .NotEmpty()
                .WithMessage("Amount cannot be empty")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Amount must be greater than 0")
                .LessThanOrEqualTo(1_000_000).WithMessage("Amount cannot exceed 1,000,000")
                .Must(a => decimal.Round(a, 2) == a).WithMessage("Amount can only have up to 2 decimal places");

            RuleFor(x => x.TotalPaidAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total paid amount must be positive")
                .LessThanOrEqualTo(x => x.TotalDueAmount)
                .WithMessage("Paid amount cannot exceed due amount");
        }
    }
}
