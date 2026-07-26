namespace backend.Parsing;

public class CsvParser
{
    public List<string> Parse(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var readerResult = reader.ReadLine();
        if(readerResult == null)
        {
            throw new InvalidDataException("csv file is empty");
        }
        return readerResult.Split(',').ToList();
    }
}
