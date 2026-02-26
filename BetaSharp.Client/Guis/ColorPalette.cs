namespace BetaSharp.Client.Guis;

public readonly partial struct Color
{
    public static readonly Color Black = new(0, 0, 0);

    /// <summary>
    /// TextColorTitle
    /// </summary>
    public static readonly Color White = new(0xFF, 0xFF, 0xFF);

    public static readonly Color Blue = new(0x00, 0x00, 0xFF);
    public static readonly Color BlueAlpha = new(0x00, 0x00, 0xFF, 130);

    /// <summary>
    /// TextColorNormal
    /// </summary>
    public static readonly Color GrayE0 = new(0xE0, 0xE0, 0xE0);

    // ReSharper disable once InconsistentNaming
    public static readonly Color GrayCC = new(0xCC, 0xCC, 0xCC);

    // ReSharper disable once InconsistentNaming
    public static readonly Color GrayAA = new(0xAA, 0xAA, 0xAA);

    /// <summary>
    /// TextColorKey
    /// </summary>
    public static readonly Color GrayA0 = new(0xA0, 0xA0, 0xA0);

    public static readonly Color Gray90 = new(0x90, 0x90, 0x90);
    public static readonly Color Gray80 = new(0x80, 0x80, 0x80);
    public static readonly Color Gray70 = new(0x70, 0x70, 0x70);
    public static readonly Color Gray50 = new(0x40, 0x40, 0x40);
    public static readonly Color Gray40 = new(0x40, 0x40, 0x40);
    public static readonly Color Yellow = new(0xFF, 0xFF, 0);
    public static readonly Color BlackAlphaC0 = new(0, 0, 0, 0xC0);

    /// <summary>
    /// BlackAlpha80
    /// </summary>
    public static readonly Color BackgroundBlackAlpha = new(0, 0, 0, 0x80);

    /// <summary>
    /// WhiteAlpha80
    /// </summary>
    public static readonly Color BackgroundWhiteAlpha = new(0xFF, 0xFF, 0xFF, 0x80);

    public static readonly Color WhiteAlpha20 = new(0xFF, 0xFF, 0xFF, 0x20);
    public static readonly Color GameOverBackgroundDarkRed = new(0, 0, 0x50, 0x60);
    public static readonly Color GameOverBackgroundRed = new(0x30, 0x30, 0x80, 0x60);
    public static readonly Color WorldBackgroundDark = new(0x10, 0x10, 0x10, 0xC0);
    public static readonly Color WorldBackground = new(0x10, 0x10, 0x10, 0xD0);

    public static readonly Color HoverYellow = new(0xFF, 0xFF, 0xA0);

    public static readonly Color AchievementTakenBlue = new(0x90, 0x90, 0xFF);
    public static readonly Color AchievementRequiresRed = new(0x70, 0x50, 0x50);
    public static readonly Color AchievementChallengeYellow = new(0xFF, 0xFF, 0x80);
    public static readonly Color AchievementChallengeLockedYellow = new(0x80, 0x80, 0x40);
}
