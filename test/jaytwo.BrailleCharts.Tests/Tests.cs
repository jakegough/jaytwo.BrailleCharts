using Xunit;
using Xunit.Abstractions;

namespace jaytwo.BrailleCharts.Tests;

public class Tests
{
    private readonly ITestOutputHelper _output;

    public Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(0, " ")]
    [InlineData(1, "⢀")]
    [InlineData(2, "⢠")]
    [InlineData(3, "⢰")]
    [InlineData(4, "⢸")]
    public void SingleDigitOneToFour(double value, string expected)
    {
        // arrange
        var renderer = new BrailleChartRenderer(1, 1, yMin: 0, yMax: 4);
        var data = new double[] { value };

        // act
        var actual = renderer.GetRenderedChartRows(data);

        // assert
        foreach (var actualLine in actual)
        {
            _output.WriteLine(actualLine);
        }

        Assert.Equal(new[] { expected }, actual);
    }

    [Fact]
    public void PositivePadding()
    {
        // arrange
        var renderer = new BrailleChartRenderer(1, 3, yMin: 0, yMax: 4);
        var data = new double[] { 1, 2 };
        var expected = "  ⣠";

        // act
        var actual = renderer.GetRenderedChartRows(data).Single();

        // assert
        _output.WriteLine(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositivePaddingMultiLine()
    {
        // arrange
        var renderer = new BrailleChartRenderer(2, 3, yMin: 0, yMax: 8);
        var data = new double[] { 1, 2, 5, 6 };
        var expected = new[] { "  ⣠", " ⣠⣿" };

        // act
        var actual = renderer.GetRenderedChartRows(data);

        foreach (var actualLine in actual)
        {
            _output.WriteLine(actualLine);
        }

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NegativePadding()
    {
        // arrange
        var renderer = new BrailleChartRenderer(1, 3, yMin: -4, yMax: 0);
        var data = new double[] { -1, -2 };
        var expected = "  ⠙";

        // act
        var actual = renderer.GetRenderedChartRows(data, invertYAxis: true).Single();

        // assert
        _output.WriteLine(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositiveRampSingleLine()
    {
        // arrange
        var renderer = new BrailleChartRenderer(1, 4, yMin: 0);
        var data = new double[] { 1, 2, 3, 4, 4, 3, 2, 1 };
        var expected = "⣠⣾⣷⣄";

        // act
        var actual = renderer.GetRenderedChartRows(data).Single();

        // assert
        _output.WriteLine(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositiveRampMultiLine()
    {
        // arrange
        var renderer = new BrailleChartRenderer(2, 8, yMin: 0);
        var data = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 3, 2, 1 };
        var expected = new[] { "  ⣠⣾⣷⣄  ", "⣠⣾⣿⣿⣿⣿⣷⣄" };

        // act
        var actual = renderer.GetRenderedChartRows(data);

        foreach (var actualLine in actual)
        {
            _output.WriteLine(actualLine);
        }

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NegativeRampSingleLine()
    {
        // arrange
        var renderer = new BrailleChartRenderer(1, 4, yMax: 0);
        var data = new double[] { -1, -2, -3, -4, -4, -3, -2, -1 };
        var expected = "⠙⢿⡿⠋";

        // act
        var actual = renderer.GetRenderedChartRows(data, true).Single();

        // assert
        _output.WriteLine(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NegativeRampMultiLine()
    {
        // arrange
        var renderer = new BrailleChartRenderer(2, 8, yMax: 0);
        var data = new double[] { -1, -2, -3, -4, -5, -6, -7, -8, -8, -7, -6, -5, -4, -3, -2, -1 };
        var expected = new[] { "⠙⢿⣿⣿⣿⣿⡿⠋", "  ⠙⢿⡿⠋  " };

        // act
        var actual = renderer.GetRenderedChartRows(data, true);

        foreach (var actualLine in actual)
        {
            _output.WriteLine(actualLine);
        }

        // assert
        Assert.Equal(expected, actual);
    }
}
