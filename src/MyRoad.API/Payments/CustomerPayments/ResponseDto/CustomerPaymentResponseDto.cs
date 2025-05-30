using MyRoad.API.Customers.ResponseDto;
using MyRoad.API.Payments.ResponseDto;

namespace MyRoad.API.Payments.CustomerPayments.ResponseDto;

public class CustomerPaymentResponseDto : PaymentResponseDto
{ 
    public CustomerDto Customer { get; set; }
}