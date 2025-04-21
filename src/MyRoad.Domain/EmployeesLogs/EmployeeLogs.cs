using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRoad.Domain.EmployeesLogs;
public class EmployeeLogs :BaseEntity<long>
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } 
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public bool IsWork { get; set; }
    public string? Notes { get; set; }
    public int CreatedByUserId { get; set; }
    
    [NotMapped]
    public double TotalHours
    {
        get
        {
            if (CheckIn.HasValue && CheckOut.HasValue)
                return (CheckOut.Value - CheckIn.Value).TotalHours;
            return 0;
        }
    }
}