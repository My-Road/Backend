namespace MyRoad.Domain.EmployeesLogs;

public class TimeOverlapValidator : ITimeOverlapValidator
{
    public bool HasOverlapAsync(EmployeeLog newLog, IEnumerable<EmployeeLog> existingLogs)
    {
        var newCheckIn = newLog.Date.ToDateTime(newLog.CheckIn);
        var newCheckOut = newLog.CheckOut > newLog.CheckIn
            ? newLog.Date.ToDateTime(newLog.CheckOut)
            : newLog.Date.AddDays(1).ToDateTime(newLog.CheckOut);
        

        return existingLogs.Any(existingLog =>
        {
            if (newLog.Id == existingLog.Id)
                return false;

            var existingCheckIn = existingLog.Date.ToDateTime(existingLog.CheckIn);
            var existingCheckOut = existingLog.CheckOut > existingLog.CheckIn
                ? existingLog.Date.ToDateTime(existingLog.CheckOut)
                : existingLog.Date.AddDays(1).ToDateTime(existingLog.CheckOut);

            return newCheckIn < existingCheckOut && newCheckOut > existingCheckIn;
        });
    }
}

