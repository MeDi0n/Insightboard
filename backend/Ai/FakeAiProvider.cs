namespace Insightboard.Api.Ai;

public class FakeAiProvider : IAiProvider
{
    public Task<string> SendMessageAsync(string prompt)
    {
        return Task.FromResult(
            """
            {"charts":[{"type":"bar","title":"sales","x":"month","y":"sales"}]}
            """
        );
    }
}
