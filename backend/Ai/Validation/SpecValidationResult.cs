using Insightboard.Api.Models.Dashboards;

namespace Insightboard.Api.Ai.Validation;

public class SpecValidationResult
{
    public bool IsValid { get; set; }
    public DashboardSpec? Spec { get; set; }
    public List<string>? Errors { get; set; }
}
