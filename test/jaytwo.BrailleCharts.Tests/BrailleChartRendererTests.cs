using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit;

namespace jaytwo.BrailleCharts.Tests;

public class BrailleChartRendererTests
{
    [Theory]
    [InlineData(0, 1, 4, 0)]
    [InlineData(1, 1, 4, 1)]
    [InlineData(2, 1, 4, 2)]
    [InlineData(3, 1, 4, 3)]
    [InlineData(4, 1, 4, 4)]
    [InlineData(1, 5, 8, 0)]
    [InlineData(2, 5, 8, 0)]
    [InlineData(3, 5, 8, 0)]
    [InlineData(4, 5, 8, 0)]
    [InlineData(5, 5, 8, 1)]
    [InlineData(6, 5, 8, 2)]
    [InlineData(7, 5, 8, 3)]
    [InlineData(8, 5, 8, 4)]
    [InlineData(0, -4, -1, 0)]
    [InlineData(-1, -4, -1, -1)]
    [InlineData(-2, -4, -1, -2)]
    [InlineData(-3, -4, -1, -3)]
    [InlineData(-4, -4, -1, -4)]
    [InlineData(-1, -8, -5, 0)]
    [InlineData(-2, -8, -5, 0)]
    [InlineData(-3, -8, -5, 0)]
    [InlineData(-4, -8, -5, 0)]
    [InlineData(-5, -8, -5, -1)]
    [InlineData(-6, -8, -5, -2)]
    [InlineData(-7, -8, -5, -3)]
    [InlineData(-8, -8, -5, -4)]
    public void TranslateSampleForRow_Test(int sampleAsDots, int minRowDots, int maxRowDots, int expected)
    {
        // arrange

        // act
        var actual = BrailleChartRenderer.TranslateSampleForRow(sampleAsDots, minRowDots, maxRowDots);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1,2,3,4,5,6,7,8", 0, "1,2")]
    [InlineData("1,2,3,4,5,6,7,8", 1, "3,4")]
    [InlineData("1,2,3,4,5,6,7,8", 2, "5,6")]
    [InlineData("1,2,3,4,5,6,7,8", 3, "7,8")]
    [InlineData("-1,-2,-3,-4,-5,-6,-7,-8", 0, "-1,-2")]
    [InlineData("-1,-2,-3,-4,-5,-6,-7,-8", 1, "-3,-4")]
    [InlineData("-1,-2,-3,-4,-5,-6,-7,-8", 2, "-5,-6")]
    [InlineData("-1,-2,-3,-4,-5,-6,-7,-8", 3, "-7,-8")]
    public void GetSampleDotsForColumn_Test(string sampleAsDotsString, int column, string expectedString)
    {
        // arrange
        var sampleAsDots = sampleAsDotsString.Split(',').Select(c => int.Parse(c)).ToArray();
        var expectedArray = expectedString.Split(',').Select(c => int.Parse(c)).ToArray();
        var expected = (expectedArray[0], expectedArray[1]);

        // act
        var actual = BrailleChartRenderer.GetSampleDotsForColumn(sampleAsDots, column);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("0,1,2", 1, 1, "1,2")]
    [InlineData("0,1", 1, 1, "0,1")]
    [InlineData("0,1", 1, 2, "1")]
    [InlineData("1", 2, 1, "1")]
    [InlineData("0,1,2,3,4,5", 2, 1, "2,3,4,5")]
    [InlineData("0,1,2,3,4,5", 2, 2, "4,5")]
    public void GetVisibleSamples_Test(string samplesString, int columns, int xDotsPerSample, string expectedString)
    {
        // arrange
        var samples = samplesString.Split(',').Select(c => double.Parse(c)).ToArray();
        var expected = expectedString.Split(',').Select(c => double.Parse(c)).ToArray();

        // act
        var actual = BrailleChartRenderer.GetVisibleSamples(samples, columns, xDotsPerSample);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("0,1", 1, 1, 4, "⢀")]
    [InlineData("0,0,0,1", 2, 1, 4, " ⢀")]
    [InlineData("1,2", 1, 1, 4, "⣠")]
    [InlineData("1,2,3,4", 2, 1, 4, "⣠⣾")]
    [InlineData("1,2,3,4,5,6,7,8", 4, 1, 4, "⣠⣾⣿⣿")]
    [InlineData("1,2,3,4,5,6,7,8", 4, 5, 8, "  ⣠⣾")]
    [InlineData("0,-1", 1, -4, -1, "⠈")]
    [InlineData("0,0,0,-1", 2, -4, -1, " ⠈")]
    [InlineData("-1,-2", 1, -4, -1, "⠙")]
    [InlineData("-1,-2,-3,-4", 2, -4, -1, "⠙⢿")]
    [InlineData("-1,-2,-3,-4,-5,-6,-7,-8", 4, -4, -1, "⠙⢿⣿⣿")]
    [InlineData("-1,-2,-3,-4,-5,-6,-7,-8", 4, -8, -5, "  ⠙⢿")]
    public void GetBrailleCharacters_Test(string sampleAsDotsString, int columns, int minRowDots, int maxRowDots, string expected)
    {
        // arrange
        var sampleAsDots = sampleAsDotsString.Split(',').Select(c => int.Parse(c)).ToArray();

        // act
        var actual = BrailleChartRenderer.GetBrailleCharacters(sampleAsDots, columns, minRowDots, maxRowDots);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1", 1, 1, 2, 0, 4, false, "1,1")]
    [InlineData("0.1", 1, 1, 2, 0, 0.4, false, "1,1")]
    [InlineData("10", 1, 1, 2, 0, 40, false, "1,1")]
    [InlineData("-1", 1, 1, 2, -4, 0, true, "-1,-1")]
    [InlineData("-0.1", 1, 1, 2, -0.4, 0, true, "-1,-1")]
    [InlineData("-10", 1, 1, 2, -40, 0, true, "-1,-1")]
    public void GetSamplesAsDots_Test(string samplesString, int rows, int columns, int xDotsPerSample, double yMin, double yMax, bool invert, string expectedString)
    {
        // arrange
        var samples = samplesString.Split(',').Select(c => double.Parse(c)).ToArray();
        var expected = expectedString.Split(',').Select(c => int.Parse(c)).ToArray();

        // act
        var actual = BrailleChartRenderer.GetSamplesAsDots(samples, rows, columns, xDotsPerSample, yMin, yMax, invert);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1, 4, 0, 4, true, -1)]
    [InlineData(-2, 4, 0, 4, true, -2)]
    [InlineData(-3, 4, 0, 4, true, -3)]
    [InlineData(-4, 4, 0, 4, true, -4)]
    [InlineData(-1, 8, 0, 8, true, -1)]
    [InlineData(-2, 8, 0, 8, true, -2)]
    [InlineData(-3, 8, 0, 8, true, -3)]
    [InlineData(-4, 8, 0, 8, true, -4)]
    [InlineData(-5, 8, 0, 8, true, -5)]
    [InlineData(-6, 8, 0, 8, true, -6)]
    [InlineData(-7, 8, 0, 8, true, -7)]
    [InlineData(-8, 8, 0, 8, true, -8)]
    [InlineData(-2, 16, 0, 8, true, -1)]
    [InlineData(-4, 16, 0, 8, true, -2)]
    [InlineData(-6, 16, 0, 8, true, -3)]
    [InlineData(-8, 16, 0, 8, true, -4)]
    [InlineData(-10, 16, 0, 8, true, -5)]
    [InlineData(-12, 16, 0, 8, true, -6)]
    [InlineData(-14, 16, 0, 8, true, -7)]
    [InlineData(-16, 16, 0, 8, true, -8)]
    [InlineData(1, 4, 0, 4, false, 1)]
    [InlineData(2, 4, 0, 4, false, 2)]
    [InlineData(3, 4, 0, 4, false, 3)]
    [InlineData(4, 4, 0, 4, false, 4)]
    [InlineData(1, 8, 0, 8, false, 1)]
    [InlineData(2, 8, 0, 8, false, 2)]
    [InlineData(3, 8, 0, 8, false, 3)]
    [InlineData(4, 8, 0, 8, false, 4)]
    [InlineData(5, 8, 0, 8, false, 5)]
    [InlineData(6, 8, 0, 8, false, 6)]
    [InlineData(7, 8, 0, 8, false, 7)]
    [InlineData(8, 8, 0, 8, false, 8)]
    [InlineData(2, 16, 0, 8, true, 1)]
    [InlineData(4, 16, 0, 8, true, 2)]
    [InlineData(6, 16, 0, 8, true, 3)]
    [InlineData(8, 16, 0, 8, true, 4)]
    [InlineData(10, 16, 0, 8, true, 5)]
    [InlineData(12, 16, 0, 8, true, 6)]
    [InlineData(14, 16, 0, 8, true, 7)]
    [InlineData(16, 16, 0, 8, true, 8)]
    public void GetSampleAsDots_Test(double sample, double yRange, double yNear, int yDots, bool invertYAxis, int expected)
    {
        // arrange

        // act
        var actual = BrailleChartRenderer.GetSampleAsDots(sample, yRange, yNear, yDots, invertYAxis);

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0, 1, false, 1, 4)]
    [InlineData(0, 2, false, 5, 8)]
    [InlineData(1, 2, false, 1, 4)]
    [InlineData(0, 1, true, -4, -1)]
    [InlineData(0, 2, true, -4, -1)]
    [InlineData(1, 2, true, -8, -5)]
    public void CalculateRowYDotsMinMax_Test(int rowIndex, int totalRows, bool invertYAxis, int expectedMinRowDots, int expectedMaxRowDots)
    {
        // arrange

        // act
        BrailleChartRenderer.CalculateRowYDotsMinMax(rowIndex, totalRows, invertYAxis, out var actualMinRowDots, out var actualMaxRowDots);

        // assert
        Assert.Equal((expectedMinRowDots, expectedMaxRowDots), (actualMinRowDots, actualMaxRowDots));
    }
}
