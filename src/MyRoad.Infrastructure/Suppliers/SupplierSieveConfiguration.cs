using MyRoad.Domain.Suppliers;
using Sieve.Services;

namespace MyRoad.Infrastructure.Suppliers
{
    public class SupplierSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Supplier>(x => x.Id)
                .CanFilter()
                .CanSort();

            mapper.Property<Supplier>(x => x.SupplierName)
                .CanFilter();

            mapper.Property<Supplier>(x => x.IsDeleted)
                .CanFilter()
                .CanSort();
        }
    }
}
