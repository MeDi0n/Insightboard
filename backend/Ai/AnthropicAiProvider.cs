using Anthropic;
using Anthropic.Models.Messages;

namespace Insightboard.Api.Ai;

public class AnthropicAiProvider : IAiProvider
{
    private readonly AnthropicClient _client;

    public AnthropicAiProvider(IConfiguration config)
    {
        _client = new AnthropicClient { ApiKey = config["Anthropic:ApiKey"]!.Trim() };
    }

    public async Task<AiResponse> SendMessageAsync(string prompt)
    {
        var message = await _client.Messages.Create(
            new MessageCreateParams
            {
                Model = "claude-haiku-4-5",
                MaxTokens = 1024,
                Messages = [new() { Role = Role.User, Content = prompt }],
            }
        );
        var text = message.Content.Select(b => b.Value).OfType<TextBlock>().First().Text;
        return new AiResponse
        {
            Text = text,
            Model = message.Model,
            InputTokens = (int)message.Usage.InputTokens,
            OutputTokens = (int)message.Usage.OutputTokens,
        };
    }
}
