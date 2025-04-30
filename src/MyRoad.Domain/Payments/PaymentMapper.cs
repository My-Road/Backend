using MyRoad.Domain.Customers;

namespace MyRoad.Domain.Payments;

public static class PaymentMapper
{
    public static void MapUpdatedPayment(this Payment existingPayment, Payment paymentToUpdate)
    {
        existingPayment.Amount = paymentToUpdate.Amount;
        existingPayment.PaymentDate = paymentToUpdate.PaymentDate;
        existingPayment.Notes = paymentToUpdate.Notes;
    }
}