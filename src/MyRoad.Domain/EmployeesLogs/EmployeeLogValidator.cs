using FluentValidation;

namespace MyRoad.Domain.EmployeesLogs
{
    public class EmployeeLogValidator : AbstractValidator<EmployeeLog>
    {
        public EmployeeLogValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee ID Cannot be empty!");

            RuleFor(x => x.Date)
                .NotNull()
                .WithMessage("Date cannot be Null");

            RuleFor(x => x.CheckIn)
                .NotNull()
                .WithMessage("CheckIn cannot be Null");

            RuleFor(x => x.CheckOut)
                .NotEmpty()
                .WithMessage("CheckOut cannot be empty");

            RuleFor(x => x.HourlyWage)
                .GreaterThan(0)
                .WithMessage("HourlyWage must be greater than 0");

            RuleFor(x => x.DailyWage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DailyWage cannot be negative");
        }
    }
}