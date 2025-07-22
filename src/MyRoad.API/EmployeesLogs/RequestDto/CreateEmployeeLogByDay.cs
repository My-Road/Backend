namespace MyRoad.API.EmployeesLogs.RequestDto;

public class CreateEmployeeLogByDay
{
    public long EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public decimal DailyPrice { get; set; }
    public string? Notes { get; set; }
    public long CreatedByUserId { get; set; }
}