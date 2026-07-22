using backend.Models;
using backend.Ai;
using System.Text.Json;

namespace backend.Generation;

public class DashboardGenerator
{
    private readonly IAiProvider _ai;

    public DashboardGenerator(IAiProvider ai)
    {
        _ai = ai;
    }

    public async Task<DashboardSpec> Generate(IFormFile file)
    {
        var response = await _ai.GetResponseAsync("prompt");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var result = JsonSerializer.Deserialize<DashboardSpec>(response, options);
        return result;
    }
}
