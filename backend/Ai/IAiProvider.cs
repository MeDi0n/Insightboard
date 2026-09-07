namespace Insightboard.Api.Ai;

public interface IAiProvider
{
    Task<string> GetResponseAsync(string prompt);
}
