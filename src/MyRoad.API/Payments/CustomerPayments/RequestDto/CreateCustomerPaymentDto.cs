namespace MyRoad.API.Payments.CustomerPayments.RequestDto;

public class CreateCustomerPaymentDto
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
    public long CustomerId { get; set; }
}