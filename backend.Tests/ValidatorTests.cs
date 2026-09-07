using Insightboard.Api.Validation;
using DocumentFormat.OpenXml.Math;

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
        Assert.NotNull(result.Spec);
        Assert.Single(result.Spec!.Charts);
        Assert.Equal("sales", result.Spec!.Charts[0].Title);
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
        Assert.Contains("invalid Json", result.Errors!);
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

    [Fact]
    public void Validate_EmptyChartFields_ReturnsAllErrors()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts":[{"type":"","title":"","x":"","y":""}]}
        """;
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.Equal(4, result.Errors!.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_BrokenJson_ReturnsSingleError()
    {
        // Arrange
        var validator = new Validator();
        var example = "hello";
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.Single(result.Errors!);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_EmptyChartType_ReturnsStructureError()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts":[{"type":"","title":"sales","x":"month","y":"sales"}]}
        """;
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.Contains("chart type is empty", result.Errors!);
        Assert.DoesNotContain("unsupported chart type", result.Errors!);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_NullCharts_ReturnsInvalid()
    {
        // Arrange
        var validator = new Validator();
        var example = """
        {"charts": null}
        """;
        List<string> columns = ["month", "sales"];

        // Act
        var result = validator.Validate(example, columns);

        // Assert
        Assert.False(result.IsValid);
    }
}
