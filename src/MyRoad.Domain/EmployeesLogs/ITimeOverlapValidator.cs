namespace MyRoad.Domain.EmployeesLogs;

public interface ITimeOverlapValidator
{
    bool HasOverlapAsync(EmployeeLog newLog, IEnumerable<EmployeeLog> existingLogs);
}