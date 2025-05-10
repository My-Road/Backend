namespace MyRoad.Domain.EmployeesLogs
{
    public static class EmployeeLogMapper
    {
        public static void MapUpdateEmployeeLog(this EmployeeLog existingEmployeeLog , EmployeeLog updatedEmployeeLog)
        {
            existingEmployeeLog.EmployeeId = updatedEmployeeLog.EmployeeId;
            existingEmployeeLog.Date = updatedEmployeeLog.Date;
            existingEmployeeLog.CheckIn = updatedEmployeeLog.CheckIn;
            existingEmployeeLog.CheckOut = updatedEmployeeLog.CheckOut;
            existingEmployeeLog.HourlyWage = updatedEmployeeLog.HourlyWage;
            existingEmployeeLog.Notes = updatedEmployeeLog.Notes;
        }
    }
}
