namespace BetaSharp.Worlds.Core.Systems;

public class WorldSettings
{
    public const int DefaultWorldHeight = 128;

    public WorldSettings(long seed, WorldType terrainType, string generatorOptions) : this(seed, terrainType, DefaultWorldHeight, generatorOptions) { }

    public WorldSettings(long seed, WorldType terrainType, int worldHeight = DefaultWorldHeight, string generatorOptions = "")
    {
        Seed = seed;
        TerrainType = terrainType;
        GeneratorOptions = generatorOptions;
        WorldHeight = worldHeight;
    }

    public long Seed { get; }
    public WorldType TerrainType { get; }
    public string GeneratorOptions { get; }
    public int WorldHeight { get; }
}
