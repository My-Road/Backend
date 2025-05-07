using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;

namespace MyRoad.Domain.EmployeesLogs;
public class EmployeeLog :BaseEntity<long>
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } 
    public DateOnly? Date {  get; set; }
    public TimeOnly? CheckIn { get; set; }
    public TimeOnly? CheckOut { get; set; }
    public bool IsDeleted { get; set; }
    public decimal HourlyWage { get; set; }
    public string? Notes { get; set; }
    public long CreatedByUserId { get; set; }
    public decimal TotalHours { get; set; }
    public decimal DailyWage { get; set; }
    public ErrorOr<Success> Delete()
    {
        if (IsDeleted)
        {
            return EmployeeLogErrors.AlreadyDeleted;
        }

        IsDeleted = true;
        return new Success();
    }
}
