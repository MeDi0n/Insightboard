namespace Insightboard.Api.Building;

public class PromptBuilder
{
    public string Build(List<string> columns, List<string> errors)
    {
        var prompt = $$"""
            You are an assistant that builds a dashboard. Columns: {{string.Join(", ", columns)}}. Respond ONLY with JSON like: {"charts":[{"type":"bar","title":"...","x":"...","y":"..."}]}. type must be "bar" or "line"; x and y must be from the columns.  Return ONLY the raw JSON object. Do NOT wrap it in markdown code fences (no ``` or ```json). Do NOT add any explanation before or after.
        """;

        if(errors.Count > 0)
        {
            prompt += $"previous errors: {string.Join(", ", errors)} - fix them";
        }
        return prompt;
    }

}
