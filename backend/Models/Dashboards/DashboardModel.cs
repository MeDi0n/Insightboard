namespace Insightboard.Api.Models.Dashboards;

public class DashboardModel
{
    public DashboardStatus Status { get; set; } = DashboardStatus.Processing;
    public DashboardSpec? Spec { get; set; } = null;
}
