using MyRoad.Domain.Orders;
using Sieve.Services;

namespace MyRoad.Infrastructure.Orders;

public class OrderSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Order>(x => x.OrderDate)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.CustomerId)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.Id)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.CreatedByUserId)
            .CanFilter()
            .CanSort();
    }
}