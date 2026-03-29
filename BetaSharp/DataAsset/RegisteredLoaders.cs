using BetaSharp.GameMode;

namespace BetaSharp.DataAsset;

public static class RegisteredLoaders
{
    public static void Init() { }

    static RegisteredLoaders()
    {
        GameModes.Init();
    }
}
