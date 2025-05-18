using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Payments.SupplierPayments;
using MyRoad.Domain.Purchases;

namespace MyRoad.Domain.Suppliers
{
    public class Supplier : BaseEntity<long>
    {
        public string? SupplierName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalDueAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public decimal RemainingAmount => TotalDueAmount - TotalPaidAmount;
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();

        public ErrorOr<Success> Delete()
        {
            if (IsDeleted)
            {
                return SupplierErrors.AlreadyDeleted;
            }

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;

            return new Success();
        }

        public ErrorOr<Success> Restore()
        {
            if (!IsDeleted)
            {
                return SupplierErrors.NotDeleted;
            }

            IsDeleted = false;
            DeletedAt = null;

            return new Success();
        }
    }      
}
