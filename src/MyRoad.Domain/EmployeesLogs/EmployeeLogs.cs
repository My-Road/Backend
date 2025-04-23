using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRoad.Domain.EmployeeLog;
public class EmployeeLogs :BaseEntity<long>
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } 
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public bool IsWorkingDay { get; set; }
    public decimal HourlyWage { get; set; }
    public string? Notes { get; set; }
    public long CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; }

    [NotMapped] public double TotalHours
    {
        get
        {
            if (CheckIn.HasValue && CheckOut.HasValue)
                return (CheckOut.Value - CheckIn.Value).TotalHours;
            return 0;
        }
    }
    [NotMapped] public decimal DailyWage => (decimal)TotalHours * HourlyWage;
}