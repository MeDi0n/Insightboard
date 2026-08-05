namespace backend.Parsing;

public interface IFileParser
{
    bool AllowedExtension(string value);
    CsvData Parse(IFormFile file);
}
