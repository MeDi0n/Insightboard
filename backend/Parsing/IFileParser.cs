namespace Insightboard.Api.Parsing;

public interface IFileParser
{
    bool AllowedExtension(string value);
    TableData Parse(Stream stream);
}
