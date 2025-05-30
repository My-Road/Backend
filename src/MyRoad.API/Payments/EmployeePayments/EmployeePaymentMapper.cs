using MyRoad.API.Payments.EmployeePayments.RequestsDto;
using MyRoad.API.Payments.EmployeePayments.ResponseDto;
using MyRoad.Domain.Payments.EmployeePayments;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Payments.EmployeePayments;

[Mapper]
public static partial class EmployeePaymentMapper
{
    public static partial EmployeePayment ToDomainEmployeePayment(this CreateEmployeePaymentDto dto);
    public static partial EmployeePayment ToDomainEmployeePayment(this UpdateEmployeePaymentDto dto);
    public static partial EmployeePaymentResponseDto ToEmployeePaymentResponseDto(this EmployeePayment dto);
}