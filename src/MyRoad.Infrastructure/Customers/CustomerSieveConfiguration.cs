using MyRoad.Domain.Customers;
using Sieve.Services;

namespace MyRoad.Infrastructure.Customers;

public class CustomerSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Customer>(x => x.Id)
            .CanFilter()
            .CanSort();

        mapper.Property<Customer>(x => x.FullName)
            .CanFilter();
    }
}