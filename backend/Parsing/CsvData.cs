namespace backend.Parsing;

public class CsvData
{
    public List<string> Columns { get; set; } = new();
    public List<Dictionary<string, string>> Rows { get; set; } = new();
}
