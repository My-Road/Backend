using System.Text.Json.Serialization;
using MyRoad.API.Common.Entities;

namespace MyRoad.API.Employees.ResponseDto;

public class EmployeeResponseDto : BaseEntity<long>
{
    [JsonPropertyName("employeeName")] public string? FullName { get; set; }
    public string? JobTitle { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal TotalDueAmount { get; set; }
    public decimal TotalPaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
}