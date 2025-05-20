namespace MyRoad.Domain.Orders;

public static class OrderMapper
{
    public static void MapUpdatedOrder(this Order existingOrder, Order updatedOrder)
    {
        existingOrder.OrderDate = updatedOrder.OrderDate;
        existingOrder.Price = updatedOrder.Price;
        existingOrder.Quantity = updatedOrder.Quantity;
        existingOrder.RecipientName = updatedOrder.RecipientName;
        existingOrder.RecipientPhoneNumber = updatedOrder.RecipientPhoneNumber;
        existingOrder.Notes = updatedOrder.Notes;
        existingOrder.CustomerId = updatedOrder.CustomerId;
    }
}