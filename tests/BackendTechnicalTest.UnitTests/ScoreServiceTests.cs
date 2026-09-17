using BackendTechnicalTest.Application.Services;

namespace BackendTechnicalTest.UnitTests;

public sealed class ScoreServiceTests
{
    private readonly ScoreCalculatorService _sut = new();

    [Fact]
    public void CalculateScore_ShouldReturn11_ForProvidedExample()
    {
        // Arrange
        var numbers = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = _sut.CalculateScore(numbers);

        // Assert
        Assert.Equal(11, result);
    }

    [Fact]
    public void CalculateScore_ShouldReturn9_ForAllOddNumbers()
    {
        // Arrange
        var numbers = new[] { 15, 25, 35 };

        // Act
        var result = _sut.CalculateScore(numbers);

        // Assert
        Assert.Equal(9, result);
    }

    [Fact]
    public void CalculateScore_ShouldReturn12_ForTwoEights()
    {
        // Arrange
        var numbers = new[] { 8, 8 };

        // Act
        var result = _sut.CalculateScore(numbers);

        // Assert
        Assert.Equal(12, result);
    }

    [Fact]
    public void CalculateScore_ShouldReturn6_ForSingleEight()
    {
        // Arrange
        var numbers = new[] { 8 };

        // Act
        var result = _sut.CalculateScore(numbers);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void CalculateScore_ShouldReturnZero_ForEmptyArray()
    {
        // Arrange
        var numbers = Array.Empty<int>();

        // Act
        var result = _sut.CalculateScore(numbers);

        // Assert
        Assert.Equal(0, result);
    }
}