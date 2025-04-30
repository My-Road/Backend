using MyRoad.API.Employees.RequestsDto;
using MyRoad.Domain.Employees;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Employees
{
    [Mapper]
    public static partial class EmployeeMapper
    {
        public static partial Employee ToDomainEmployee(this CreateEmployeeDto dto);
        public static partial Employee ToDomainEmployee(this UpdateEmployeeDto dto);
    }
}
