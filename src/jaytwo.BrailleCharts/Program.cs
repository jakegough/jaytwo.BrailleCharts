using System.Text;

namespace jaytwo.BrailleCharts;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var renderer = new BrailleChartRenderer(
            columns: 120,
            rows: 5);

        var samples = GenerateSineWave(240, 10);
        var renderedChart = renderer.GetRenderedChart(samples);
        Console.WriteLine(renderedChart);

        var invertedSamples = samples.Select(x => -x).ToArray();
        var negativeRenderedChart = renderer.GetRenderedChart(invertedSamples, invertYAxis: true);
        Console.WriteLine(negativeRenderedChart);
    }

    public static double[] GenerateSineWave(int length, int amplitude)
    {
        // from ChatGPT
        double[] wave = new double[length];

        for (int i = 0; i < length; i++)
        {
            double radians = (2 * Math.PI * i) / length; // Normalize the wave cycle
            wave[i] = Math.Sin(radians) * amplitude;
        }

        return wave;
    }
}
