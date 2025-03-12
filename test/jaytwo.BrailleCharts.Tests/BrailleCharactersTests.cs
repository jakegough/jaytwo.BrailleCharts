using Xunit;

namespace jaytwo.BrailleCharts.Tests;

public class BrailleCharactersTests
{
    [Theory]
    [InlineData(-4, -4, BrailleCharacters.BrailleNeg44)]
    [InlineData(-4, -3, BrailleCharacters.BrailleNeg43)]
    [InlineData(-4, -2, BrailleCharacters.BrailleNeg42)]
    [InlineData(-4, -1, BrailleCharacters.BrailleNeg41)]
    [InlineData(-4, 0, BrailleCharacters.BrailleNeg40)]
    [InlineData(-3, -4, BrailleCharacters.BrailleNeg34)]
    [InlineData(-3, -3, BrailleCharacters.BrailleNeg33)]
    [InlineData(-3, -2, BrailleCharacters.BrailleNeg32)]
    [InlineData(-3, -1, BrailleCharacters.BrailleNeg31)]
    [InlineData(-3, 0, BrailleCharacters.BrailleNeg30)]
    [InlineData(-2, -4, BrailleCharacters.BrailleNeg24)]
    [InlineData(-2, -3, BrailleCharacters.BrailleNeg23)]
    [InlineData(-2, -2, BrailleCharacters.BrailleNeg22)]
    [InlineData(-2, -1, BrailleCharacters.BrailleNeg21)]
    [InlineData(-2, 0, BrailleCharacters.BrailleNeg20)]
    [InlineData(-1, -4, BrailleCharacters.BrailleNeg14)]
    [InlineData(-1, -3, BrailleCharacters.BrailleNeg13)]
    [InlineData(-1, -2, BrailleCharacters.BrailleNeg12)]
    [InlineData(-1, -1, BrailleCharacters.BrailleNeg11)]
    [InlineData(-1, 0, BrailleCharacters.BrailleNeg10)]
    [InlineData(0, -4, BrailleCharacters.BrailleNeg04)]
    [InlineData(0, -3, BrailleCharacters.BrailleNeg03)]
    [InlineData(0, -2, BrailleCharacters.BrailleNeg02)]
    [InlineData(0, -1, BrailleCharacters.BrailleNeg01)]
    [InlineData(0, 0, BrailleCharacters.Braille00)]
    [InlineData(0, 1, BrailleCharacters.BraillePos01)]
    [InlineData(0, 2, BrailleCharacters.BraillePos02)]
    [InlineData(0, 3, BrailleCharacters.BraillePos03)]
    [InlineData(0, 4, BrailleCharacters.BraillePos04)]
    [InlineData(1, 0, BrailleCharacters.BraillePos10)]
    [InlineData(1, 1, BrailleCharacters.BraillePos11)]
    [InlineData(1, 2, BrailleCharacters.BraillePos12)]
    [InlineData(1, 3, BrailleCharacters.BraillePos13)]
    [InlineData(1, 4, BrailleCharacters.BraillePos14)]
    [InlineData(2, 0, BrailleCharacters.BraillePos20)]
    [InlineData(2, 1, BrailleCharacters.BraillePos21)]
    [InlineData(2, 2, BrailleCharacters.BraillePos22)]
    [InlineData(2, 3, BrailleCharacters.BraillePos23)]
    [InlineData(2, 4, BrailleCharacters.BraillePos24)]
    [InlineData(3, 0, BrailleCharacters.BraillePos30)]
    [InlineData(3, 1, BrailleCharacters.BraillePos31)]
    [InlineData(3, 2, BrailleCharacters.BraillePos32)]
    [InlineData(3, 3, BrailleCharacters.BraillePos33)]
    [InlineData(3, 4, BrailleCharacters.BraillePos34)]
    [InlineData(4, 0, BrailleCharacters.BraillePos40)]
    [InlineData(4, 1, BrailleCharacters.BraillePos41)]
    [InlineData(4, 2, BrailleCharacters.BraillePos42)]
    [InlineData(4, 3, BrailleCharacters.BraillePos43)]
    [InlineData(4, 4, BrailleCharacters.BraillePos44)]
    public void TranslateSampleForRow_Test(int leftDots, int rightDots, char expected)
    {
        // arrange

        // act
        var actual = BrailleCharacters.GetCharacter(leftDots, rightDots);

        // assert
        Assert.Equal(expected, actual);
    }
}
