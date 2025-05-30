using MyRoad.API.Common.Entities;

namespace MyRoad.API.Payments.ResponseDto;

public class PaymentResponseDto : BaseEntity<long>
{
    public decimal Amount { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Notes { get; set; }
    public bool IsDeleted { get; set; }
    public DateOnly? DeletedAt { get; set; }
}