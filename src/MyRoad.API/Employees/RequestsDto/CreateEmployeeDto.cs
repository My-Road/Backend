using System.Text.Json.Serialization;

namespace MyRoad.API.Employees.RequestsDto
{
    public class CreateEmployeeDto
    {
        [JsonPropertyName("EmployeeName")]
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
