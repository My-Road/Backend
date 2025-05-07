using MyRoad.Domain.EmployeesLogs;
using Sieve.Services;

namespace MyRoad.Infrastructure.EmployeesLogs
{
    public class EmployeeLogSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<EmployeeLog>(x => x.Date)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.EmployeeId)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.Id)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.CreatedByUserId)
                .CanFilter()
                .CanSort();

        }
    }
}
