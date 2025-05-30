using System.Text.Json.Serialization;
using MyRoad.API.Common.Entities;

namespace MyRoad.API.Employees.ResponseDto;

public class EmployeeDto : BaseEntity<long>
{
    [JsonPropertyName("employeeName")] public string? FullName { get; set; }
    public string Address { get; set; } = string.Empty;
}