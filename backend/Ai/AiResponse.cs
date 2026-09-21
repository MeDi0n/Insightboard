namespace Insightboard.Api.Ai;

public class AiResponse
{
    public required string Text { get; set; }
    public required string Model { get; set; }
    public required int InputTokens { get; set; }
    public required int OutputTokens { get; set; }
}
