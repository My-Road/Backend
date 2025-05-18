using MyRoad.Domain.Suppliers;

namespace MyRoad.Domain.Payments.SupplierPayments
{
    public class SupplierPayment : Payment
    {
        public long SupplierId {  get; set; }
        public Supplier Supplier {  get; set; }
    }
}
