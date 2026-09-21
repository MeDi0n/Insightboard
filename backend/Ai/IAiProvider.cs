namespace Insightboard.Api.Ai;

public interface IAiProvider
{
    Task<AiResponse> SendMessageAsync(string prompt);
}
