using Insightboard.Api.Models.Charts;

namespace Insightboard.Api.Models.Dashboards;

public class DashboardSpec
{
    public List<ChartSpec> Charts { get; set; } = new();
    public List<Dictionary<string, string>> Data { get; set; } = new();
}
