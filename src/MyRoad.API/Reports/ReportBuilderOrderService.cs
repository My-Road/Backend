using System.Text;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Reports;

namespace MyRoad.API.Reports
{
    public static class ReportBuilderOrderService 
    {
        public static async Task<string> BuildOrdersReportHtml(List<Order> orders)
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
                <h2>تقارير طلبات الزبائن</h2>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>التاريخ</th>
                        <th>اسم الزبون</th>
                        <th>العنوان</th>
                        <th>الكمية</th>
                        <th>السعر</th>
                        <th>المبلغ الاجمالي</th>
                    </tr>
                </thead>
                <tbody>");

            int index = 1;
            foreach (var order in orders)
            {
                sb.Append($@"
                    <tr>
                        <td>{index}</td>
                        <td>{order.OrderDate:yyyy-MM-dd}</td>
                        <td>{order.Customer.FullName}</td>
                        <td>{order.Customer.Address}</td>
                        <td>{order.Quantity}</td>
                        <td>{order.Price} شيكل</td>
                        <td>{order.Price * order.Quantity} شيكل</td>
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
