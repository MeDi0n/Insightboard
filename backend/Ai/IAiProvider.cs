namespace backend.Ai;

public interface IAiProvider
{
    Task<string> GetResponseAsync(string prompt);
}
