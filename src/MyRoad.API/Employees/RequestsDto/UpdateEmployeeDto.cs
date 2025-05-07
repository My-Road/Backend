using System.Text.Json.Serialization;

namespace MyRoad.API.Employees.RequestsDto
{
    public class UpdateEmployeeDto
    {
        public long Id { get; set; }

        [JsonPropertyName("employeeName")]
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public DateOnly StartDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
