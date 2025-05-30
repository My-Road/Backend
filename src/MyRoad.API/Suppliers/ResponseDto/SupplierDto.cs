using System.Text.Json.Serialization;
using MyRoad.API.Common.Entities;

namespace MyRoad.API.Suppliers.ResponseDto;

public class SupplierDto : BaseEntity<long>
{
    [JsonPropertyName("supplierName")] public string? FullName { get; set; }
    public string Address { get; set; } = string.Empty;
}