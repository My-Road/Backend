using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Customers;

namespace MyRoad.Domain.Orders
{
    public class Order : BaseEntity<long>
    {
        public DateOnly OrderDate { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientPhoneNumber { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        
        public decimal TotalDueAmount => Price * Quantity;

        public string? Notes { get; set; }
        
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsCompleted { get; set; }

        public long CreatedByUserId { get; set; }

        public ErrorOr<Success> Delete()
        {
            if (IsDeleted)
            {
                return OrderErrors.IsDeleted;
            }

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            
            return new Success();
        }

        public ErrorOr<Success> Restore()
        {
            if (!IsDeleted)
            {
                return OrderErrors.NotDeleted;
            }

            IsDeleted = false;
            DeletedAt = null;

            return new Success();
        }
    }
}