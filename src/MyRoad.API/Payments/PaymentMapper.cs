using MyRoad.API.Payments.CustomerPayments.ResponseDto;
using MyRoad.API.Payments.EmployeePayments.ResponseDto;
using MyRoad.API.Payments.ResponseDto;
using MyRoad.API.Payments.SupplierPayments.ResponseDto;
using MyRoad.Domain.Payments;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Domain.Payments.SupplierPayments;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Payments;

[Mapper]
public static partial class PaymentMapper
{
 
    public static partial PaymentResponseDto ToPaymentResponseDto(this Payment dto);
    public static partial CustomerPaymentResponseDto ToCustomerPaymentResponseDto(this CustomerPayment dto);
    public static partial EmployeePaymentResponseDto ToEmployeePaymentResponseDto(this EmployeePayment dto);
    public static partial SupplierPaymentResponseDto ToSupplierPaymentResponseDto(this SupplierPayment dto);
}