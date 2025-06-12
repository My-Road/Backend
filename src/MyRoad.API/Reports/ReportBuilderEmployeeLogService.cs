using System.Text;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Reports;

namespace MyRoad.API.Reports
{
    public static class ReportBuilderEmployeeLogService 
    {
        public async static Task<string> BuildEmployeesLogReportHtml(List<EmployeeLog> employeeLogs)
        {
            var sb = new StringBuilder();

            sb.Append(@"
        <html dir='rtl'>
        <head>
            <style>
                body { font-family: 'Arial Arabic', 'Times New Roman'; margin: 20px; }
                table { width: 100%; border-collapse: collapse; margin-top: 20px; }
                th, td { border: 1px solid #ddd; padding: 8px; text-align: center; }
                th { background-color: #f2f2f2; }
                tr + tr { border-top: 2px solid #bbb; }
                .header { text-align: center; margin-bottom: 20px; }
                .footer { margin-top: 20px; text-align: center; }
            </style>
        </head>
        <body>
            <div class='header'>
                <h2>تقارير سجلات الموظفين </h2>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>التاريخ</th>
                        <th>اسم الموظف</th>
                        <th>العنوان</th>
                        <th>سعر الساعة </th>
                        <th>وقت الدخول </th>
                        <th>وقت الخروج  </th>
                        <th>ساعات العمل </th>
                        <th>الاجر اليومي </th>
                    </tr>
                </thead>
                <tbody>");

            int index = 1;
            foreach (var empLog in employeeLogs)
            {
                sb.Append($@"
                    <tr>
                        <td>{index}</td>
                        <td>{empLog.Date:yyyy-MM-dd}</td>
                        <td>{empLog.Employee.FullName}</td>
                        <td>{empLog.Employee.Address}</td>
                        <td>{empLog.HourlyWage} شيكل</td>
                        <td>{empLog.CheckIn}</td>
                        <td>{empLog.CheckOut}</td>
                        <td>{empLog.TotalHours}</td>
                        <td>{empLog.DailyWage}</td>
                    </tr>");
                index++;
            }

            sb.Append(@"
                </tbody>
            </table>
        </body>
        </html>");

            return await Task.FromResult(sb.ToString());
        }
    }
}
