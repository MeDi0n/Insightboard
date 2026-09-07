using Insightboard.Api.Models.Dashboards;

namespace Insightboard.Api.Validation;

public class ValidationResult
{
    public bool IsValid { get; set; }
    public DashboardSpec? Spec { get; set; }
    public List<string>? Errors { get; set; }
}
