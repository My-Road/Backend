using System.ComponentModel.DataAnnotations.Schema;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;

namespace MyRoad.Domain.EmployeesLogs;
public class EmployeeLog :BaseEntity<long>
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } 
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public bool IsWorkingDay { get; set; }
    public decimal HourlyWage { get; set; }
    public string? Notes { get; set; }
    public long CreatedByUserId { get; set; }
    
    [NotMapped] public double TotalHours
    {
        get
        {
            if (CheckIn.HasValue && CheckOut.HasValue)
                return (CheckOut.Value - CheckIn.Value).TotalHours;
            return 0;
        }
    }
    public decimal DailyWage => (decimal)TotalHours * HourlyWage;
}