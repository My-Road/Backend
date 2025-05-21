using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Suppliers;

namespace MyRoad.Domain.Purchases
{
    public class Purchase : BaseEntity<long>
    {
        public DateOnly PurchasesDate { get; set; }
        public string GoodsDeliverer { get; set; }
        public string GoodsDelivererPhoneNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalDueAmount => Math.Round(Quantity * Price, 2);
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }
        public long CreatedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ErrorOr<Success> Delete()
        {
            if (IsDeleted)
            {
                return PurchaseErrors.IsDeleted;
            }

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;

            return new Success();
        }
    }
}