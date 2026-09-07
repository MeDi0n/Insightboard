using System.Text;
using Microsoft.AspNetCore.Http;
using Insightboard.Api.Parsing;

namespace CsvParserTests;

public class CsvParserTests {

private static IFormFile MakeFile(string content, string name)
{
    var bytes = Encoding.UTF8.GetBytes(content);
    var stream = new MemoryStream(bytes);
    return new FormFile(stream, 0, bytes.Length, "file", name);
}

    [Fact]
    public void Parse_ValidCsv_ReturnsColumnsAndRows()
    {
        var parser = new CsvParser();

        // Arrange
        var csv = """
        month,sales
        Jan,100
        Feb,150
        """;

        // Act
        var csvFile = MakeFile(csv, "test.csv");

        var result = parser.Parse(csvFile);

        // Assert
        Assert.Equal(2, result.Columns.Count);
        Assert.Equal(2, result.Rows.Count);
        Assert.Equal("Jan", result.Rows[0]["month"]);
    }

    [Fact]
    public void Parse_RaggedRow_FillsMissingCellsWithEmpty()
    {
        var parser = new CsvParser();

        // Arrange
        var csv = """
        month,sales,awards
        Jan,100,2
        Feb,150
        """;

        // Act
        var csvFile = MakeFile(csv, "test.csv");

        var result = parser.Parse(csvFile);

        // Assert
        Assert.Equal(3, result.Columns.Count);
        Assert.Equal(2, result.Rows.Count);
        Assert.Equal("", result.Rows[1]["awards"]);
    }

    [Fact]
    public void Parse_EmptyHeader_Throws()
    {
        var parser = new CsvParser();

        // Arrange
        var csv = """

        Jan,100
        """;


        // Act
        var csvFile = MakeFile(csv, "test.csv");

        // Assert
        Assert.Throws<InvalidDataException>(() => parser.Parse(csvFile));
    }
}
