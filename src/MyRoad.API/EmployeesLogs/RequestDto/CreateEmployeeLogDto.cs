namespace MyRoad.API.EmployeesLogs.RequestDto
{
    public class CreateEmployeeLogDto
    {
        public long EmployeeId { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public decimal HourlyWage { get; set; }
        public string? Notes { get; set; }
        public long CreatedByUserId { get; set; }

    }
}
