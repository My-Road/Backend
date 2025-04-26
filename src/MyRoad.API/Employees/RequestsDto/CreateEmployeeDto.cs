namespace MyRoad.API.Employees.RequestsDto
{
    public class CreateEmployeeDto
    {
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }
        public string? Notes { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
    }
}
