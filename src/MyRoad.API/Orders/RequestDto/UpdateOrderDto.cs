namespace MyRoad.API.Orders.RequestDto;

public class UpdateOrderDto
{
    public long Id { get; set; }
    public DateOnly OrderDate { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientPhoneNumber { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public long CreatedByUserId { get; set; }

    public long CustomerId { get; set; }

    public string? Notes { get; set; }
}