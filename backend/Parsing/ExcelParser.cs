namespace Insightboard.Api.Parsing;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;

public class ExcelParser : IFileParser
{
    public bool AllowedExtension(string extension)
    {
        return string.Equals(extension, ".xlsx", StringComparison.OrdinalIgnoreCase);
    }

    public TableData Parse(IFormFile file)
    {
        using var workbook = new XLWorkbook(file.OpenReadStream());


        var sheet = workbook.Worksheet(1);
        var rows = sheet.RowsUsed().ToList();

        if(rows.Count == 0)
        {
            throw new InvalidDataException("file has no header row");
        }

         var columns = new List<string>();
         foreach(var cell in rows[0].Cells())
        {
            columns.Add(cell.GetString());
        }

        var tableRows = new List<Dictionary<string, string>>();


        for(int a = 1; a < rows.Count; a++)
        {
            var dictionary = new Dictionary<string, string>();

            for(int b = 0; b < columns.Count; b++)
            {
            dictionary[columns[b]] = rows[a].Cell(b + 1).GetString();
            }


            tableRows.Add(dictionary);
        }


        return new TableData
        {
            Columns = columns,
            Rows = tableRows
        };
    }
}
