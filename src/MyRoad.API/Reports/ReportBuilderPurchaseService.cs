﻿using System.Text;
using MyRoad.Domain.Purchases;

namespace MyRoad.API.Reports
{
    public static class ReportBuilderPurchaseService
    {
        public static async Task<string> BuildPurchaseReportHtml(List<Purchase> purchases)
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
                <h2>تقارير عمليات الشراء للمُوَرِّدين</h2>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>التاريخ</th>
                        <th>اسم ٱلْمُوَرِّد</th>
                        <th>العنوان</th>
                        <th>الكمية</th>
                        <th>السعر</th>
                        <th>المبلغ الاجمالي</th>
                    </tr>
                </thead>
                <tbody>");

            int index = 1;
            foreach (var purchase in purchases)
            {
                sb.Append($@"
                    <tr>
                        <td>{index}</td>
                        <td>{purchase.PurchasesDate:yyyy-MM-dd}</td>
                        <td>{purchase.Supplier.FullName}</td>
                        <td>{purchase.Supplier.Address}</td>
                        <td>{purchase.Quantity}</td>
                        <td>{purchase.Price} شيكل</td>
                        <td>{purchase.Price * purchase.Quantity} شيكل</td>
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
