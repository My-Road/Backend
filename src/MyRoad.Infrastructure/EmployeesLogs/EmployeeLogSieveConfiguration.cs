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

            mapper.Property<EmployeeLog>(x => x.Id)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.CreatedByUserId)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.Employee.FullName)
                .CanFilter()
                .CanSort();
            
            mapper.Property<EmployeeLog>(x => x.Employee.Address)
                .CanFilter()
                .CanSort();
            
            mapper.Property<EmployeeLog>(x => x.IsCompleted)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.Employee.StartDate)
                .CanFilter()
                .CanSort();

            mapper.Property<EmployeeLog>(x => x.Employee.EndDate)
                .CanFilter()
                .CanSort();

        }
    }
}
