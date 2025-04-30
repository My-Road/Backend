using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;

namespace MyRoad.Domain.Customers;

public class Customer : BaseEntity<long>
{
    [JsonPropertyName("customerName")] public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public decimal TotalPaidAmount { get; set; }
    public decimal TotalDueAmount { get; set; }
    
    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public decimal RemainingAmount => TotalDueAmount - TotalPaidAmount;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<CustomerPayment> CustomerPayments { get; set; } = new List<CustomerPayment>();

    public ErrorOr<Success> Restore()
    {
        if (!IsDeleted)
        {
            return CustomerErrors.NotDeleted;
        }

        IsDeleted = false;
        DeletedAt = null;

        return new Success();
    }

    public ErrorOr<Success> Delete()
    {
        if (IsDeleted)
        {
            return CustomerErrors.AlreadyDeleted;
        }

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;

        return new Success();
    }
}