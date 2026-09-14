namespace Insightboard.Api.Ai;

public interface IAiProvider
{
    Task<string> SendMessageAsync(string prompt);
}
