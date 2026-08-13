namespace backend.Parsing;

using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;


public class PdfParser : IFileParser
{
    public bool AllowedExtension(string extension)
    {
        return string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase);
    }

    public TableData Parse(IFormFile file)
    {

    using var document = PdfDocument.Open(file.OpenReadStream());

    var pages = document.GetPages();

    var lines = new List<string>();
    var columns = new List<string>();

    foreach(var page in pages)
        {
            var text = ContentOrderTextExtractor.GetText(page);
            var separateText = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            lines.AddRange(separateText);
        }

        if(lines.Count == 0)
        {
            throw new InvalidDataException("could not find a table in this pdf");
        }

            var column = lines[0].Split("  ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

        if(column.Count < 2)
        {
            throw new InvalidDataException("columns count is less than 2");
        }

        var tableRows = new List<Dictionary<string, string>>();

        for(int a = 1; a < lines.Count; a++)
        {
            var dictionary = new Dictionary<string, string>();
            var line = lines[a].Split("  ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            for(int i = 0; i < column.Count; i++)
            {
                if(i < line.Length)
                {
                    dictionary[column[i]] = line[i];
                }
                else
                {
                    dictionary[column[i]] = "";
                }
            }
            tableRows.Add(dictionary);
        }

        return new TableData
        {
            Columns = column, Rows = tableRows
        };
    }
}
