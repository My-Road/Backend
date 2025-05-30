using MyRoad.API.Employees.ResponseDto;

namespace MyRoad.API.EmployeesLogs.ResponseDto;

public class SearchResponseDto : EmployeeLogResponseDto
{
    public EmployeeDto Employee { get; set; }
}