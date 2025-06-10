using MyRoad.Domain.Purchases;

namespace MyRoad.Domain.Reports.SuppliersReports
{
    public interface IReportBuilderPurchaseService
    {
        string BuildPurchaseReportHtml(List<Purchase> purchases);
    }
}
