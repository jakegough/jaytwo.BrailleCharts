namespace jaytwo.BrailleCharts;

internal class BrailleCharacters()
{
    public const char Braille00 = ' ';

    public const char BraillePos10 = '⡀';
    public const char BraillePos01 = '⢀';
    public const char BraillePos11 = '⣀';
    public const char BraillePos20 = '⡄';
    public const char BraillePos21 = '⣄';
    public const char BraillePos02 = '⢠';
    public const char BraillePos12 = '⣠';
    public const char BraillePos22 = '⣤';
    public const char BraillePos30 = '⡆';
    public const char BraillePos31 = '⣆';
    public const char BraillePos32 = '⣦';
    public const char BraillePos03 = '⢰';
    public const char BraillePos13 = '⣰';
    public const char BraillePos23 = '⣴';
    public const char BraillePos33 = '⣶';
    public const char BraillePos40 = Braille40;
    public const char BraillePos41 = '⣇';
    public const char BraillePos42 = '⣧';
    public const char BraillePos43 = '⣷';
    public const char BraillePos04 = Braille04;
    public const char BraillePos14 = '⣸';
    public const char BraillePos24 = '⣼';
    public const char BraillePos34 = '⣾';
    public const char BraillePos44 = Braille44;

    public const char BrailleNeg10 = '⠁';
    public const char BrailleNeg01 = '⠈';
    public const char BrailleNeg11 = '⠉';
    public const char BrailleNeg20 = '⠃';
    public const char BrailleNeg21 = '⠋';
    public const char BrailleNeg02 = '⠘';
    public const char BrailleNeg12 = '⠙';
    public const char BrailleNeg22 = '⠛';
    public const char BrailleNeg30 = '⠇';
    public const char BrailleNeg31 = '⠏';
    public const char BrailleNeg32 = '⠟';
    public const char BrailleNeg03 = '⠸';
    public const char BrailleNeg13 = '⠹';
    public const char BrailleNeg23 = '⠻';
    public const char BrailleNeg33 = '⠿';
    public const char BrailleNeg40 = Braille40;
    public const char BrailleNeg41 = '⡏';
    public const char BrailleNeg42 = '⡟';
    public const char BrailleNeg43 = '⡿';
    public const char BrailleNeg04 = Braille04;
    public const char BrailleNeg14 = '⢹';
    public const char BrailleNeg24 = '⢻';
    public const char BrailleNeg34 = '⢿';
    public const char BrailleNeg44 = Braille44;

    private const char Braille40 = '⡇';
    private const char Braille04 = '⢸';
    private const char Braille44 = '⣿';

    public static char GetCharacter(int leftDots, int rightDots)
    {
        if (leftDots < -4 || leftDots > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(leftDots));
        }

        if (rightDots < -4 || rightDots > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(rightDots));
        }

        return (leftDots, rightDots) switch
        {
            (4, 4) => BraillePos44,
            (4, 3) => BraillePos43,
            (4, 2) => BraillePos42,
            (4, 1) => BraillePos41,
            (4, 0) => BraillePos40,
            (3, 4) => BraillePos34,
            (3, 3) => BraillePos33,
            (3, 2) => BraillePos32,
            (3, 1) => BraillePos31,
            (3, 0) => BraillePos30,
            (2, 4) => BraillePos24,
            (2, 3) => BraillePos23,
            (2, 2) => BraillePos22,
            (2, 1) => BraillePos21,
            (2, 0) => BraillePos20,
            (1, 4) => BraillePos14,
            (1, 3) => BraillePos13,
            (1, 2) => BraillePos12,
            (1, 1) => BraillePos11,
            (1, 0) => BraillePos10,
            (0, 4) => BraillePos04,
            (0, 3) => BraillePos03,
            (0, 2) => BraillePos02,
            (0, 1) => BraillePos01,
            (0, 0) => Braille00,
            (0, -1) => BrailleNeg01,
            (0, -2) => BrailleNeg02,
            (0, -3) => BrailleNeg03,
            (0, -4) => BrailleNeg04,
            (-1, 0) => BrailleNeg10,
            (-1, -1) => BrailleNeg11,
            (-1, -2) => BrailleNeg12,
            (-1, -3) => BrailleNeg13,
            (-1, -4) => BrailleNeg14,
            (-2, 0) => BrailleNeg20,
            (-2, -1) => BrailleNeg21,
            (-2, -2) => BrailleNeg22,
            (-2, -3) => BrailleNeg23,
            (-2, -4) => BrailleNeg24,
            (-3, 0) => BrailleNeg30,
            (-3, -1) => BrailleNeg31,
            (-3, -2) => BrailleNeg32,
            (-3, -3) => BrailleNeg33,
            (-3, -4) => BrailleNeg34,
            (-4, 0) => BrailleNeg40,
            (-4, -1) => BrailleNeg41,
            (-4, -2) => BrailleNeg42,
            (-4, -3) => BrailleNeg43,
            (-4, -4) => BrailleNeg44,
            _ => throw new Exception($"No mapping for combination: [{leftDots}, {rightDots}]"),
        };
    }
}
