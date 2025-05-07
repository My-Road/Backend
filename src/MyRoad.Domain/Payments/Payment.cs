using ErrorOr;
using MyRoad.Domain.Common.Entities;

namespace MyRoad.Domain.Payments;

public class Payment : BaseEntity<long>
{
    public decimal Amount { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Notes { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateOnly? DeletedAt { get; set; }

    public ErrorOr<Success> Delete()
    {
        if (IsDeleted)
        {
            return PaymentErrors.IsDeleted;
        }

        IsDeleted = true;
        DeletedAt = DateOnly.FromDateTime(DateTime.UtcNow);

        return new Success();
    }
}