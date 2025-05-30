using MyRoad.API.Payments.ResponseDto;
using MyRoad.Domain.Payments;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Payments;

[Mapper]
public static partial class PaymentMapper
{
 
    public static partial PaymentResponseDto ToPaymentResponseDto(this Payment dto);
}