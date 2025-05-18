namespace MyRoad.API.Purchases.RequestDto
{
    public class UpdatePurchasesDto
    {
        public long Id { get; set; }
        public DateOnly PurchasesDate { get; set; }
        public string? GoodsDeliverer { get; set; }
        public string? GoodsDelivererPhoneNumber { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }
        public long SupplierId { get; set; }
    }
}
