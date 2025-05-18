namespace MyRoad.Domain.EmployeesLogs;

public class TimeOverlapValidator : ITimeOverlapValidator
{
    public Task<bool> HasOverlapAsync(EmployeeLog newLog, IEnumerable<EmployeeLog> existingLogs)
    {
        var newCheckIn = newLog.Date.ToDateTime(newLog.CheckIn);
        var newCheckOut = newLog.CheckOut > newLog.CheckIn
            ? newLog.Date.ToDateTime(newLog.CheckOut)
            : newLog.Date.AddDays(1).ToDateTime(newLog.CheckOut);

        var hasOverlap = existingLogs.Any(existingLog =>
        {
            var existingCheckIn = existingLog.Date.ToDateTime(existingLog.CheckIn);
            var existingCheckOut = existingLog.CheckOut > existingLog.CheckIn
                ? existingLog.Date.ToDateTime(existingLog.CheckOut)
                : existingLog.Date.AddDays(1).ToDateTime(existingLog.CheckOut);

            return newCheckIn < existingCheckOut && newCheckOut > existingCheckIn;
        });

        return Task.FromResult(hasOverlap);
    }
}

