namespace Insightboard.Api.Ai;

public class FakeAiProvider : IAiProvider
{
    public Task<AiResponse> SendMessageAsync(string prompt)
    {
        return Task.FromResult(
            new AiResponse
            {
                Text = """
                {"charts":[{"type":"bar","title":"sales","x":"month","y":"sales"}]}
                """,
                Model = "fake",
                InputTokens = 0,
                OutputTokens = 0,
            }
        );
    }
}
