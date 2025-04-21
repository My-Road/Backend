using MyRoad.API.Payments.EmployeePayments.RequestsDto;
using MyRoad.Domain.Payments.EmployeePayments;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Payments.EmployeePayments;

[Mapper]
public static partial class EmployeePaymentMapper
{
    public static partial EmployeePayment ToDomainEmployeePayment(this CreateEmployeePaymentDto dto);
    public static partial EmployeePayment ToDomainEmployeePayment(this UpdateEmployeePaymentDto dto);
}