namespace MyRoad.Domain.EmployeesLogs;

public interface ITimeOverlapValidator
{
    Task<bool> HasOverlapAsync(EmployeeLog newLog, IEnumerable<EmployeeLog> existingLogs);
}