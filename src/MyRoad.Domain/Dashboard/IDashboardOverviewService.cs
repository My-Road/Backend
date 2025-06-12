using ErrorOr;

namespace MyRoad.Domain.Dashboard;

public interface IDashboardOverviewService
{
    Task<ErrorOr<DashboardOverview>> ExecuteAsync();
}