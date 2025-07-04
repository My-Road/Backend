using MyRoad.API.Dashboard.ResponseDto;
using MyRoad.Domain.Dashboard;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Dashboard;

[Mapper]
public static partial class DashboardMapper
{
    public static partial DashboardAdminOverviewDto ToDashboardAdminOverviewDto(DashboardAdminOverview dto);
    public static partial DashboardManagerOverviewDto ToDashboardManagerOverviewDto(DashboardManagerOverview dto);
}