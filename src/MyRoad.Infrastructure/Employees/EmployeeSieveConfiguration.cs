using MyRoad.Domain.Employees;
using Sieve.Services;

namespace MyRoad.Infrastructure.Employees;

public class EmployeeSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Employee>(x => x.Id)
            .CanFilter()
            .CanSort();

        mapper.Property<Employee>(x => x.FullName)
            .CanFilter();

        mapper.Property<Employee>(x => x.IsActive)
            .CanFilter()
            .CanSort();
        
        mapper.Property<Employee>(x => x.TotalDueAmount)
            .CanFilter()
            .CanSort();
        

    }
}