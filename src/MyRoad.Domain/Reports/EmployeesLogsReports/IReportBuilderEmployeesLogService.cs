using MyRoad.Domain.EmployeesLogs;

namespace MyRoad.Domain.Reports
{
    public interface IReportBuilderEmployeesLogService
    {
        string BuildEmployeesLogReportHtml(List<EmployeeLog> employeeLogs);
    }
}
