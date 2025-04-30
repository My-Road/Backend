using MyRoad.Domain.Customers;

namespace MyRoad.Domain.Payments.CustomerPayments;

public class CustomerPayment : Payment
{
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    
}