using MyRoad.API.Employees.ResponseDto;
using MyRoad.API.Payments.ResponseDto;

namespace MyRoad.API.Payments.EmployeePayments.ResponseDto;

public class EmployeePaymentResponseDto : PaymentResponseDto
{
    public EmployeeDto Employee { get; set; }
}