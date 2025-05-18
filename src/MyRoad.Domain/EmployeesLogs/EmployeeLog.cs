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
    public decimal TotalHours => (decimal)(CheckOut - CheckIn).TotalHours;

    /*public decimal TotalHours
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var checkInDateTime = today.ToDateTime(CheckIn);
            var checkOutDateTime = today.ToDateTime(CheckOut);

            if (checkOutDateTime < checkInDateTime)
            {
                checkOutDateTime = checkOutDateTime.AddDays(1);
            }

            var timeSpan = checkOutDateTime - checkInDateTime;
            return timeSpan.Hours + Math.Round((decimal)timeSpan.Minutes / 60, 2);
        }
    }
    */

    public decimal DailyWage => TotalHours* HourlyWage;
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
