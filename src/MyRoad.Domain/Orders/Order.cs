using System.ComponentModel.DataAnnotations.Schema;
using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Orders
{
    public class Order : BaseEntity<long>
    {
        public DateTime OrderDate { get; set; }
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

        public ErrorOr<Success> Delete(string note)
        {
            if (IsDeleted)
            {
                return OrderErrors.IsDeleted;
            }

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            Notes = note;

            return new Success();
        }

        public ErrorOr<Success> Restore(string note)
        {
            if (!IsDeleted)
            {
                return OrderErrors.NotDeleted;
            }

            IsDeleted = false;
            DeletedAt = null;
            Notes = note;

            return new Success();
        }
    }
}