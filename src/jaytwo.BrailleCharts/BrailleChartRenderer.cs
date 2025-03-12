using System;
using System.Text;

namespace jaytwo.BrailleCharts;

public class BrailleChartRenderer
{
    public const int XDotsPerChar = 2;
    public const int YDotsPerChar = 4;

    public BrailleChartRenderer(int rows, int columns, double? yMin = default, double? yMax = default)
    {
        Rows = rows;
        Columns = columns;
        YMin = yMin;
        YMax = yMax;
    }

    public int Rows { get; }

    public int Columns { get; }

    public double? YMin { get; }

    public double? YMax { get; }

    public int XDotsPerSample { get; set; } = 1;

    public string GetRenderedChart(double[] samples, bool invertYAxis = false)
    {
        var result = new StringBuilder();

        foreach (var row in GetRenderedChartRows(samples, invertYAxis))
        {
            result.AppendLine(row);
        }

        return result.ToString();
    }

    public string[] GetRenderedChartRows(double[] samples, bool invertYAxis = false)
    {
        var visibleSamples = GetVisibleSamples(samples, Columns, XDotsPerSample);
        var yMin = YMin ?? visibleSamples.Min();
        var yMax = YMax ?? visibleSamples.Max();
        var samplesAsDots = GetSamplesAsDots(samples, Rows, Columns, XDotsPerSample, yMin, yMax, invertYAxis);

        var result = new string[Rows];

        for (int i = 0; i < Rows; i++)
        {
            CalculateRowYDotsMinMax(i, Rows, invertYAxis, out var minRowDots, out var maxRowDots);
            result[i] = GetBrailleCharacters(samplesAsDots, Columns, minRowDots, maxRowDots);
        }

        return result;
    }

    internal static void CalculateRowYDotsMinMax(int rowIndex, int totalRows, bool invertYAxis, out int minRowDots, out int maxRowDots)
    {
        minRowDots = invertYAxis
            ? -(rowIndex + 1) * YDotsPerChar
            : ((totalRows - rowIndex - 1) * YDotsPerChar) + 1;

        maxRowDots = minRowDots + YDotsPerChar - 1;
    }

    internal static double[] GetVisibleSamples(double[] samples, int columns, int xDotsPerSample)
    {
        var maxVisibleSamples = (int)Math.Ceiling(columns / (double)xDotsPerSample * XDotsPerChar);
        var result = samples.Skip(Math.Max(0, samples.Length - maxVisibleSamples)).ToArray();
        return result;
    }

    internal static int[] GetSamplesAsDots(double[] samples, int rows, int columns, int xDotsPerSample, double yMin, double yMax, bool invertYAxis)
    {
        var visibleSamples = GetVisibleSamples(samples, columns, xDotsPerSample);
        var visibleSampleDotCount = visibleSamples.Length * xDotsPerSample;
        var visibleDotsPerRow = XDotsPerChar * columns;
        var paddingDots = visibleDotsPerRow - visibleSampleDotCount;
        var yDots = rows * YDotsPerChar;
        var yRange = yMax - yMin;
        var yNear = invertYAxis ? yMax : yMin;

        var result = Enumerable.Repeat(0, paddingDots)
            .Concat(visibleSamples
                .Select(x => GetSampleAsDots(x, yRange, yNear, yDots, invertYAxis))
                .SelectMany(x => Enumerable.Repeat(x, xDotsPerSample)))
            .ToArray();

        return result;
    }

    internal static int GetSampleAsDots(double sample, double yRange, double yNear, int yDots, bool invertYAxis)
    {
        var magnitudeRatio = (sample - yNear) / yRange;
        var magnitudeDots = (int)Math.Round(magnitudeRatio * yDots, MidpointRounding.AwayFromZero);
        return magnitudeDots;
    }

    internal static string GetBrailleCharacters(int[] samplesAsDots, int columns, int minRowDots, int maxRowDots)
    {
        var result = new char[columns];

        for (int i = 0; i < columns; i++)
        {
            var (leftSample, rightSample) = GetSampleDotsForColumn(samplesAsDots, i);
            var leftDot = TranslateSampleForRow(leftSample, minRowDots, maxRowDots);
            var rightDot = TranslateSampleForRow(rightSample, minRowDots, maxRowDots);
            result[i] = BrailleCharacters.GetCharacter(leftDot, rightDot);
        }

        return new string(result);
    }

    internal static (int, int) GetSampleDotsForColumn(int[] samplesAsDots, int column)
    {
        var leftIndex = column * XDotsPerChar;
        var rightIndex = leftIndex + 1;
        return (samplesAsDots[leftIndex], samplesAsDots[rightIndex]);
    }

    internal static int TranslateSampleForRow(int sampleAsDots, int minRowDots, int maxRowDots)
    {
        if (minRowDots < 0)
        {
            var isBelowMin = sampleAsDots <= minRowDots;
            if (isBelowMin)
            {
                return -4;
            }

            var isBetweenMinMax = sampleAsDots >= minRowDots && sampleAsDots <= maxRowDots;

            var ceiling = Math.Max(minRowDots, sampleAsDots);
            var delta = Math.Min(0, ceiling - maxRowDots - 1);

            if (delta > 0)
            {
                throw new Exception();
            }

            return delta;
        }

        return Math.Max(0, Math.Min(sampleAsDots, maxRowDots) - minRowDots + 1);
    }
}
