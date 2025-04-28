namespace MyRoad.Domain.Payments.CustomerPayments;

public static class CustomerPaymentMapper
{
    public static void MapUpdatedCustomerPayment(this CustomerPayment customerPayment,
        CustomerPayment updatedCustomerPayment)
    {
        customerPayment.Amount = updatedCustomerPayment.Amount;
        customerPayment.PaymentDate = updatedCustomerPayment.PaymentDate;
        customerPayment.Notes = updatedCustomerPayment.Notes;
    }
}