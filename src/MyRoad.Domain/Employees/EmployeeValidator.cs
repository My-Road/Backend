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
                .NotEmpty()
                .WithMessage("Phone number cannot be empty")
                .MaximumLength(15);

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Start date must be in the past or today");
        }
    }
}
