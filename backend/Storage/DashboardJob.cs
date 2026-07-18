using backend.Models;

namespace backend.Storage;

public class DashboardJob
{
    public string Status { get; set; } = "processing";
    public DashboardSpec? Spec { get; set; } = null;
}
