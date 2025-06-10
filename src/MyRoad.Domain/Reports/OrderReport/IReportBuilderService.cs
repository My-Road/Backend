using MyRoad.Domain.Orders;

namespace MyRoad.Domain.Reports.PDF
{
    public interface IReportBuilderOrdersService
    {
        string BuildOrdersReportHtml(List<Order> orders);
    }
}
