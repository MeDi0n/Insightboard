namespace Insightboard.Api.Services;

public class CreateDashboardResult
{
    public bool IsSuccess { get; set; }
    public Guid? Id { get; set; }
    public string? Error { get; set; }
}
