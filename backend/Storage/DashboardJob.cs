using backend.Models;

namespace backend.Storage;

public class DashboardJob
{
    public DashboardStatus Status { get; set; } = DashboardStatus.Processing;
    public DashboardSpec? Spec { get; set; } = null;
}
