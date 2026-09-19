namespace Insightboard.Api.Models.AiCalls;

public class AiCallLog
{
    public required Guid Id { get; set; }
    public required Guid DashboardId { get; set; }
    public required int Attempt { get; set; }
    public required string Model { get; set; }
    public required int InputTokens { get; set; }
    public required int OutputTokens { get; set; }
    public required int DurationMs { get; set; }
    public required bool IsValid { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}
