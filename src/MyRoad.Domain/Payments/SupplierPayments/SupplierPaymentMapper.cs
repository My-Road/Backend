namespace MyRoad.Domain.Payments.SupplierPayments
{
    public static partial class SupplierPaymentMapper
    {
        public static void MapUpdatedSupplierPayment(this SupplierPayment supplierPayment, SupplierPayment updatedsupplierPayment)
        {
            supplierPayment.Amount = updatedsupplierPayment.Amount;
            supplierPayment.PaymentDate = updatedsupplierPayment.PaymentDate;
            supplierPayment.Notes = updatedsupplierPayment.Notes;
        }
    }
}
