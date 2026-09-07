namespace Insightboard.Api.Parsing;

public class TableData
{
    public List<string> Columns { get; set; } = new();
    public List<Dictionary<string, string>> Rows { get; set; } = new();
}
