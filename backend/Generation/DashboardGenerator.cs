using backend.Models;
using backend.Ai;
using backend.Validation;
using backend.Parsing;
using backend.Building;

namespace backend.Generation;

public class DashboardGenerator
{
    private readonly IAiProvider _ai;
    private readonly Validator _validator;
    private readonly PromptBuilder _builder;

    public DashboardGenerator(IAiProvider ai, Validator validator, PromptBuilder builder)
    {
        _ai = ai;
        _validator = validator;
        _builder = builder;
    }

    public async Task<DashboardSpec?> Generate(TableData parsed)
    {
        var columns = parsed.Columns;
        var errors = new List<string>();
        for(int i = 0; i < 3; i++)
        {
            var prompt = _builder.Build(columns, errors);
            var response = await _ai.GetResponseAsync(prompt);
            var result = _validator.Validate(response, columns);
            if(result.IsValid)
            {
                result.Spec!.Data = parsed.Rows;
                return result.Spec;
            }
            errors = result.Errors ?? new List<string>();
        }
        return null;
    }
}
