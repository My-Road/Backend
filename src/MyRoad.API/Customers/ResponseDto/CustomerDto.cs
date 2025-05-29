using System.Text.Json.Serialization;
using MyRoad.API.Common.Entities;

namespace MyRoad.API.Customers.ResponseDto;

public class CustomerDto : BaseEntity<long>
{
    [JsonPropertyName("customerName")] public string? FullName { get; set; }
}