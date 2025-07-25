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

        mapper.Property<Order>(x => x.Id)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.CreatedByUserId)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.RecipientName)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.Customer.FullName)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.Customer.Address)
            .CanFilter()
            .CanSort();

        mapper.Property<Order>(x => x.IsCompleted)
            .CanFilter()
            .CanSort();
    }
}