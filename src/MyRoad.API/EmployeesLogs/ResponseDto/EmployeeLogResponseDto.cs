using MyRoad.API.Common.Entities;
using MyRoad.API.Employees.ResponseDto;

namespace MyRoad.API.EmployeesLogs.ResponseDto;

public class EmployeeLogResponseDto : BaseEntity<long>
{
    public DateOnly Date { get; set; }
    public TimeOnly CheckIn { get; set; }
    public TimeOnly CheckOut { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCompleted { get; set; }
    public decimal HourlyWage { get; set; }
    public string? Notes { get; set; }
    public long CreatedByUserId { get; set; }
    public decimal TotalHours { get; set; }
    public decimal DailyWage { get; set; }
    
    public EmployeeDto Employee { get; set; }
}