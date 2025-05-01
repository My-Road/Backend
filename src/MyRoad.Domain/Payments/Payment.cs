using ErrorOr;
using MyRoad.Domain.Common.Entities;

namespace MyRoad.Domain.Payments;

public class Payment : BaseEntity<long>
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public ErrorOr<Success> Delete()
    {
        if (IsDeleted)
        {
            return PaymentErrors.IsDeleted;
        }

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;

        return new Success();
    }
}