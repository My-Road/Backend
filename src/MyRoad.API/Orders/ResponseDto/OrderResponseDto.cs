using MyRoad.API.Common.Entities;
using MyRoad.API.Customers.ResponseDto;

namespace MyRoad.API.Orders.ResponseDto;

public class OrderResponseDto : BaseEntity<long>
{
    public DateOnly OrderDate { get; set; }
    public string? RecipientName { get; set; }
    public string? RecipientPhoneNumber { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalDueAmount { get; set; }
    public string? Notes { get; set; }
    public bool IsCompleted { get; set; }
    public long CreatedByUserId { get; set; }
}