using MyRoad.Domain.Purchases;
using Sieve.Services;

namespace MyRoad.Infrastructure.Purchases
{
    public class PurchaseSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Purchase>(x => x.PurchasesDate)
                .CanFilter()
                .CanSort();

            mapper.Property<Purchase>(x => x.SupplierId)
                .CanFilter()
                .CanSort();

            mapper.Property<Purchase>(x => x.Id)
                .CanFilter()
                .CanSort();

            mapper.Property<Purchase>(x => x.CreatedByUserId)
                .CanFilter()
                .CanSort();
        }
    }
}
