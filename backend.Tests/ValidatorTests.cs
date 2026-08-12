using backend.Validation;

namespace ValidatorTests;

public class ValidatorTests
{
    [Fact]
    public void Validate_ValidResponse_ReturnsValid()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts":[{"type":"bar","title":"sales","x":"month","y":"sales"}]}
        """;
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_BrokenJson_ReturnsInvalid()
    {
        // Arrange
        var validator = new Validator();
        var example = "hello";
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_NoColumns_ReturnsInvalid()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts":[{"type":"bar","title":"sales","x":"month","y":"sales"}]}
        """;
        List<string> columns = [];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_NoCharts_ReturnsInvalid()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts":[{}]}
        """;
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_UnknownColumn_ReturnsInvalid()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts":[{"type":"bar","title":"sales","x":"banana","y":"sales"}]}
        """;
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.False(result.IsValid);
    }
}

