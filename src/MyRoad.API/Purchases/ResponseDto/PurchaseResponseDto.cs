using MyRoad.API.Common.Entities;
using MyRoad.API.Suppliers.ResponseDto;

namespace MyRoad.API.Purchases.ResponseDto;

public class PurchaseResponseDto : BaseEntity<long>
{
    public DateOnly PurchasesDate { get; set; }
    public string GoodsDeliverer { get; set; }
    public string GoodsDelivererPhoneNumber { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalDueAmount { get; set; }
    public string Notes { get; set; }
    public bool IsCompleted { get; set; }
    public long CreatedByUserId { get; set; }
}