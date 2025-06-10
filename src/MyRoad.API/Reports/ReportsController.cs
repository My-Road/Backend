using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Purchases;
using MyRoad.Domain.Reports;
using MyRoad.Domain.Reports.PDF;
using MyRoad.Domain.Reports.SuppliersReports;

namespace MyRoad.API.Reports
{
    [Route("api/v{version:apiVersion}/report")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReportsController(IOrderService orderService,
    IReportBuilderOrdersService reportBuilderOrdersService,
    IPdfGeneratorService pdfGeneratorService,
    IEmployeeLogService employeeLogService,
    IReportBuilderEmployeesLogService reportBuilderEmployeesLogService,
    IPurchaseService purchaseService,
    IReportBuilderPurchaseService reportBuilderPurchaseService
    ) : ControllerBase
    {

        [HttpPost("orders-report")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GenerateOrderReport([FromBody] ReportFilter filter)
        {
            var ordersResult = await orderService.GetOrdersForReportAsync(filter);
            var htmlContent = reportBuilderOrdersService.BuildOrdersReportHtml(ordersResult.Value);
            var pdfBytes = pdfGeneratorService.GeneratePdfFromHtml(htmlContent);

            return File(await pdfBytes, "application/pdf",
                $"OrderReport.pdf");
        }

        [HttpPost("employeesLogs-report")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GenerateEmployeeLogReport([FromBody] ReportFilter filter)
        {
            var empLogResult = await employeeLogService.GetEmployeesLogForReportAsync(filter);
            var htmlContent = reportBuilderEmployeesLogService.BuildEmployeesLogReportHtml(empLogResult.Value);
            var pdfBytes = pdfGeneratorService.GeneratePdfFromHtml(htmlContent);

            return File(await pdfBytes, "application/pdf",
                $"EmployeeLogReport.pdf");
        }

        [HttpPost("purchase-report")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GeneratePurchaseReport([FromBody] ReportFilter filter)
        {
            var purchaseResult = await purchaseService.GetPurchasesForReportAsync(filter);
            var htmlContent = reportBuilderPurchaseService.BuildPurchaseReportHtml(purchaseResult.Value);
            var pdfBytes = pdfGeneratorService.GeneratePdfFromHtml(htmlContent);

            return File(await pdfBytes, "application/pdf",
                $"PurchaseReport.pdf");
        }
    }
}
