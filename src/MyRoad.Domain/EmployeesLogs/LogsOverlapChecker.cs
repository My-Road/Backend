namespace MyRoad.Domain.EmployeesLogs;

public static class LogsOverlapChecker
{
    public static bool HasOverlap(EmployeeLog newLog, IEnumerable<EmployeeLog> existingLogs)
    {
        var newCheckIn = newLog.Date.ToDateTime(newLog.CheckIn);
        var newCheckOut = newLog.CheckOut > newLog.CheckIn
            ? newLog.Date.ToDateTime(newLog.CheckOut)
            : newLog.Date.AddDays(1).ToDateTime(newLog.CheckOut);

        return (from existingLog in existingLogs
            where existingLog.Id != newLog.Id 
            let existingCheckIn = existingLog.Date.ToDateTime(existingLog.CheckIn)
            let existingCheckOut = existingLog.CheckOut > existingLog.CheckIn
                ? existingLog.Date.ToDateTime(existingLog.CheckOut)
                : existingLog.Date.AddDays(1).ToDateTime(existingLog.CheckOut)
            where newCheckIn < existingCheckOut && newCheckOut > existingCheckIn
            select existingCheckIn).Any();
    }
}


/*
   2:00 to 8:00
   22:00 to 3:00
*/