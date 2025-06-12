using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Purchases;
using MyRoad.Domain.Reports;

namespace MyRoad.API.Reports
{
    [Route("api/v{version:apiVersion}/report")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReportsController(IOrderService orderService,
    IPdfGeneratorService pdfGeneratorService,
    IEmployeeLogService employeeLogService,
    IPurchaseService purchaseService
    ) : ControllerBase
    {
        [HttpPost("orders-report")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GenerateOrderReport([FromBody] RetrievalRequest request)
        {
            var orderResult = await orderService.GetOrdersForReportAsync(request.ToSieveModel());
            var htmlContent = await ReportBuilderOrderService.BuildOrdersReportHtml(orderResult.Value);
            var pdfBytes = await pdfGeneratorService.GeneratePdfFromHtml(htmlContent);

            return File(pdfBytes, "application/pdf",
                $"OrderReport.pdf");
        }

        [HttpPost("employeesLogs-report")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GenerateEmployeeLogReport([FromBody] RetrievalRequest request)
        {
            var empLogResult = await employeeLogService.GetEmployeesLogForReportAsync(request.ToSieveModel());
            var htmlContent = await ReportBuilderEmployeeLogService.BuildEmployeesLogReportHtml(empLogResult.Value);
            var pdfBytes = await pdfGeneratorService.GeneratePdfFromHtml(htmlContent);

            return File(pdfBytes, "application/pdf",
                $"EmployeeLogReport.pdf");
        }

        [HttpPost("purchase-report")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GeneratePurchaseReport([FromBody] RetrievalRequest request)
        {
            var purchasesResult = await purchaseService.GetPurchasesForReportAsync(request.ToSieveModel());
            var htmlContent = await ReportBuilderPurchaseService.BuildPurchaseReportHtml(purchasesResult.Value);
            var pdfBytes = await pdfGeneratorService.GeneratePdfFromHtml(htmlContent);

            return File(pdfBytes, "application/pdf",
                $"PurchaseReport.pdf");
        }
    }
}
