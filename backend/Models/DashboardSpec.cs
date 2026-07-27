namespace backend.Models;

public class DashboardSpec
{
    public List<ChartSpec> Charts { get; set; } = new();
    public List<Dictionary<string, string>> Data { get; set; } = new();
}
