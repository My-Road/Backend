namespace MyRoad.API.Orders.RequestDto;

public class CreateOrderDto
{
    public DateTime OrderDate { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientPhoneNumber { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public string? Notes { get; set; }

    public long CustomerId { get; set; }

    public long CreatedByUserId { get; set; }
}