namespace MyRoad.API.Payments.EmployeePayments.RequestsDto;

public class CreateEmployeePaymentDto
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
    public long EmployeeId { get; set; }
}