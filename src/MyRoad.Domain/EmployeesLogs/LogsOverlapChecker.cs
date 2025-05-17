namespace MyRoad.Domain.EmployeesLogs;

public static class LogsOverlapChecker
{
    public static bool HasOverlap(EmployeeLog newLog, IEnumerable<EmployeeLog> existingLogs)
    {
        var newCheckIn = newLog.Date.ToDateTime(newLog.CheckIn);
        var newCheckOut = newLog.CheckOut > newLog.CheckIn
            ? newLog.Date.ToDateTime(newLog.CheckOut)
            : newLog.Date.AddDays(1).ToDateTime(newLog.CheckOut);

        return existingLogs.Where(existingLog =>
        {
            var existingCheckIn = existingLog.Date.ToDateTime(existingLog.CheckIn);
            var existingCheckOut = existingLog.CheckOut > existingLog.CheckIn
                ? existingLog.Date.ToDateTime(existingLog.CheckOut)
                : existingLog.Date.AddDays(1).ToDateTime(existingLog.CheckOut);

            return newCheckIn < existingCheckOut && newCheckOut > existingCheckIn;
        }).Any();
    }
}


/*
   2:00 to 8:00
   22:00 to 3:00
*/