using System.Text.Json.Serialization;
using MyRoad.API.Common.Entities;

namespace MyRoad.API.Suppliers.ResponseDto;

public class SupplierResponseDto : BaseEntity<long>
{
    [JsonPropertyName("supplierName")] public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public decimal TotalPaidAmount { get; set; }
    public decimal TotalDueAmount { get; set; }
    public bool IsDeleted { get; set; }
    public decimal RemainingAmount { get; set; }
}