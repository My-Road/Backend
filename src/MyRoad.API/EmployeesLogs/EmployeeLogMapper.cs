using MyRoad.API.EmployeesLogs.RequestDto;
using MyRoad.API.EmployeesLogs.ResponseDto;
using MyRoad.Domain.EmployeesLogs;
using Riok.Mapperly.Abstractions;
namespace MyRoad.API.EmployeesLogs
{
    [Mapper]
    public static partial class EmployeeLogMapper
    {
        public static partial EmployeeLog ToDomainEmployeeLog(this CreateEmployeeLogDto dto);
        public static partial EmployeeLog ToDomainEmployeeLog(this UpdateEmployeeLogDto dto);

        public static partial EmployeeLogResponseDto ToDomainEmployeeLogResponseDto(this EmployeeLog dto);
        public static partial SearchResponseDto ToSearchResponseDto(this EmployeeLog dto);

       
    }
}
