using Insightboard.Api.Models.Dashboards;

namespace Insightboard.Api.Services;

public class DashboardListItem
{
    public Guid Id { get; set; }
    public required string FileName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public required DashboardStatus Status { get; set; }
}
