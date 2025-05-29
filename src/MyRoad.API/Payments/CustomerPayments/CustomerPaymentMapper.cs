using MyRoad.API.Payments.CustomerPayments.RequestDto;
using MyRoad.API.Payments.CustomerPayments.ResponseDto;
using Riok.Mapperly.Abstractions;
using MyRoad.Domain.Payments.CustomerPayments;

namespace MyRoad.API.Payments.CustomerPayments;

[Mapper]
public static partial class CustomerPaymentMapper
{
    public static partial CustomerPayment ToCustomerPayment(this CreateCustomerPaymentDto dto);
    public static partial CustomerPayment ToCustomerPayment(this UpdateCustomerPaymentDto dto);
    public static partial CustomerPaymentResponseDto ToCustomerPaymentResponseDto(this CustomerPayment customerPayment);
}