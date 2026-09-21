namespace Insightboard.Api.Models.AiCalls;

public class UsageSummary
{
    public required int TotalCalls { get; set; }
    public required int InvalidCalls { get; set; }
    public required int TotalInputTokens { get; set; }
    public required int TotalOutputTokens { get; set; }
    public required int WastedTokens { get; set; }
}
