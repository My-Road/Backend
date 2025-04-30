using System.Text.Json.Serialization;

namespace MyRoad.API.Customers.RequestDto;

public class UpdateCustomerDto
{
    public long Id { get; set; }
    
    [JsonPropertyName("customerName")]
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}