using MyRoad.Domain.Payments.SupplierPayments;
using Sieve.Services;

namespace MyRoad.Infrastructure.Payments.SupplierPayments
{
    public class SupplierPaymentSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<SupplierPayment>(x => x.SupplierId)
                .CanFilter()
                .CanSort();

            mapper.Property<SupplierPayment>(x => x.PaymentDate)
                .CanFilter()
                .CanSort();

            mapper.Property<SupplierPayment>(x => x.Id)
                .CanFilter()
                .CanSort();
        }
    }
}
