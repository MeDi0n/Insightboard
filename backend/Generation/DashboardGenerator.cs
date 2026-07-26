using backend.Models;
using backend.Ai;
using System.Text.Json;
using backend.Validation;
using backend.Parsing;

namespace backend.Generation;

public class DashboardGenerator
{
    private readonly IAiProvider _ai;
    private readonly Validator _validator;
    private readonly CsvParser _parser;

    public DashboardGenerator(IAiProvider ai, Validator validator, CsvParser parser)
    {
        _ai = ai;
        _validator = validator;
        _parser = parser;
    }

    public async Task<DashboardSpec?> Generate(IFormFile file)
    {
        var columns = _parser.Parse(file);

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
