using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;

namespace MyRoad.Domain.EmployeesLogs;
public class EmployeeLog :BaseEntity<long>
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } 
    public DateOnly Date {  get; set; }
    public TimeOnly CheckIn { get; set; }
    public TimeOnly CheckOut { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCompleted { get; set; }
    public decimal HourlyWage { get; set; }
    public string? Notes { get; set; }
    public long CreatedByUserId { get; set; }
    public decimal TotalHours => Math.Round((decimal)(CheckOut - CheckIn).TotalHours, 2);
    public decimal DailyWage => Math.Round(TotalHours * HourlyWage, 2);
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
