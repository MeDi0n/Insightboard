using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;

namespace backend.Parsing;

public class CsvParser : IFileParser
{
    public bool AllowedExtension(string extension)
    {
        return string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase);
    }
    public TableData Parse(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var headerLine = reader.ReadLine();
        if(string.IsNullOrWhiteSpace(headerLine))
        {
            throw new InvalidDataException("file has no header row");
        }
        var columns = headerLine.Split(",").ToList();
        var rows = new List<Dictionary<string, string>>();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var values = line.Split(",");

            var dictionary = new Dictionary<string, string>();
            for(int i = 0; i < columns.Count; i++)
            {
                if(i < values.Length)
                {
                    dictionary[columns[i]] = values[i];
                }
                else
                {
                    dictionary[columns[i]] = "";
                }

            }
            rows.Add(dictionary);
        }

        return new TableData {Columns = columns, Rows = rows};
    }
}
