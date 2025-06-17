using ErrorOr;

namespace MyRoad.Domain.Dashboard;

public interface IDashboardOverviewService
{
    Task<ErrorOr<DashboardAdminOverview>> ExecuteAdminAsync();
    Task<ErrorOr<DashboardManagerOverview>>  ExecuteManagerAsync();
}