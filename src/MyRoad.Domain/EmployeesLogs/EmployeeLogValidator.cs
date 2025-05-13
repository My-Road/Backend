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

            RuleFor(x=>x.Date)
                .NotEmpty()
                .WithMessage("Date cannot be empty");

            RuleFor(x => x.HourlyWage)
                .GreaterThan(0)
                .WithMessage("HourlyWage must be greater than 0");

            RuleFor(x => x.DailyWage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DailyWage cannot be negative");
        }
    }
}
