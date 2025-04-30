namespace MyRoad.API.Employees.RequestsDto
{
    public class CreateEmployeeDto
    {
        public string? employeeName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }
        public string? Notes { get; set; }
    }
}
