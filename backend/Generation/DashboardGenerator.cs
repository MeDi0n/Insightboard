using backend.Models;
using backend.Ai;
using System.Text.Json;
using backend.Validation;

namespace backend.Generation;

public class DashboardGenerator
{
    private readonly IAiProvider _ai;
    private readonly Validator _validator;

    public DashboardGenerator(IAiProvider ai, Validator validator)
    {
        _ai = ai;
        _validator = validator;
    }

    public async Task<DashboardSpec?> Generate(IFormFile file)
    {
        var columns = new List<string> {"month", "sales", "awards"};

        for(int i = 0; i < 3; i++)
        {
            var response = await _ai.GetResponseAsync("prompt");
            var result = _validator.Validate(response, columns);
            if(result.IsValid)
            {
                return result.Spec;
            }
        }
        return null;
    }
}
