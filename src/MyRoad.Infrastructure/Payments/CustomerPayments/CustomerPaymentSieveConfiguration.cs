using MyRoad.Domain.Payments.CustomerPayments;
using Sieve.Services;

namespace MyRoad.Infrastructure.Payments.CustomerPayments;

public class CustomerPaymentSieveConfiguration : ISieveConfiguration

{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<CustomerPayment>(x => x.PaymentDate)
            .CanFilter()
            .CanSort();

        mapper.Property<CustomerPayment>(x => x.Id)
            .CanFilter()
            .CanSort();

        mapper.Property<CustomerPayment>(x => x.Customer.FullName)
            .CanFilter()
            .CanSort();

        mapper.Property<CustomerPayment>(x => x.Customer.Address)
            .CanFilter()
            .CanSort();
    }
}