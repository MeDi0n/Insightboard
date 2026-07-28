using Anthropic;
using Anthropic.Models.Messages;

namespace backend.Ai;

public class AnthropicAiProvider : IAiProvider
{
    private readonly AnthropicClient _client;

    public AnthropicAiProvider(IConfiguration config)
    {
        _client = new AnthropicClient { ApiKey = config["Anthropic:ApiKey"]!.Trim() };
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        var message = await _client.Messages.Create(new MessageCreateParams
    {
        Model = "claude-haiku-4-5",
        MaxTokens = 1024,
        Messages = [ new() { Role = Role.User, Content = prompt } ]
    });
        var text = message.Content.Select(b => b.Value).OfType<TextBlock>().First().Text;
        return text;

    }
}
