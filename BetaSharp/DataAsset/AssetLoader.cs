using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace BetaSharp.DataAsset;

public abstract class AssetLoader
{
    private protected static readonly ILogger s_logger = Log.Instance.For(typeof(AssetLoader).FullName ?? nameof(AssetLoader));

    private static readonly List<AssetLoader> s_assetLoaders = new();

    private static bool s_worldAssetsLoaded = false;

    private protected static JsonSerializerOptions s_jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenReading
    };

    private protected readonly LoadLocations Locations;

    private protected LoadLocations LoadedAssetsModify;

    private static string? s_lastDataPath = null;
    private static string s_lastWorldDataPath = null!;
    private static string s_lastResourcePath = null!;

    private protected AssetLoader(LoadLocations locations)
    {
        s_assetLoaders.Add(this);
        Locations = locations;
    }

    public static Task LoadBaseAssets() => LoadBaseAssets(LoadLocations.None);

    private static async Task LoadBaseAssets(LoadLocations filter)
    {
        RegisteredLoaders.Init();

        const string p = "assets";
        if (!Directory.Exists(p))
        {
            Directory.CreateDirectory(p);
        }

        foreach (var loader in s_assetLoaders)
        {
            if (!loader.Locations.HasFlag(LoadLocations.Assets)) continue;
            if (!loader.LoadedAssetsModify.HasFlag(filter)) continue;

            await loader.OnLoadAssets(p, false, LoadLocations.Assets);
        }
    }

    public static Task LoadDatapackAssets(string? path) => LoadDatapackAssets(path, LoadLocations.None);

    private static async Task LoadDatapackAssets(string? path, LoadLocations filter)
    {
        s_lastDataPath = path;
        string p = path != null ? Path.Combine(path, "datapacks") : "datapacks";
        if (!Directory.Exists(p))
        {
            Directory.CreateDirectory(p);
        }

        foreach (string pack in Directory.EnumerateDirectories(p))
        {
            if (pack.EndsWith(".disabled")) continue;
            string assets = Path.Join(pack, "data");
            if (!Directory.Exists(pack)) continue;
            foreach (var loader in s_assetLoaders)
            {
                if (!loader.Locations.HasFlag(LoadLocations.GameDatapack)) continue;
                if (!loader.LoadedAssetsModify.HasFlag(filter)) continue;

                await loader.OnLoadAssets(assets, true, LoadLocations.GameDatapack);
            }
        }
    }

    public static Task LoadWorldAssets(string path) => LoadWorldAssets(path, LoadLocations.None);

    private static async Task LoadWorldAssets(string path, LoadLocations filter)
    {
        s_lastWorldDataPath = path;
        s_worldAssetsLoaded = true;
        string p = Path.Combine(path, "datapacks");
        if (!Directory.Exists(p))
        {
            Directory.CreateDirectory(p);
        }

        foreach (string pack in Directory.EnumerateDirectories(p))
        {
            if (pack.EndsWith(".disabled")) continue;
            string assets = Path.Join(pack, "data");
            if (!Directory.Exists(pack)) continue;

            foreach (var loader in s_assetLoaders)
            {
                if (!loader.Locations.HasFlag(LoadLocations.WorldDatapack)) continue;
                if (!loader.LoadedAssetsModify.HasFlag(filter)) continue;

                await loader.OnLoadAssets(assets, true, LoadLocations.WorldDatapack);
            }
        }
    }

    public static Task LoadResourcepackAssets(string path) => LoadResourcepackAssets(path, LoadLocations.None);

    private static async Task LoadResourcepackAssets(string path, LoadLocations filter)
    {
        s_lastResourcePath = path;
        string p = Path.Combine(path, "resourcepacks");
        if (!Directory.Exists(p))
        {
            Directory.CreateDirectory(p);
        }

        foreach (string pack in Directory.EnumerateDirectories(p))
        {
            if (pack.EndsWith(".disabled")) continue;
            string assets = Path.Join(pack, "data");
            if (!Directory.Exists(pack)) continue;

            foreach (var loader in s_assetLoaders)
            {
                if (!loader.Locations.HasFlag(LoadLocations.Resourcepack)) continue;
                if (!loader.LoadedAssetsModify.HasFlag(filter)) continue;

                await loader.OnLoadAssets(assets, true, LoadLocations.Resourcepack);
            }
        }
    }

    public static async Task UnloadWorldAssets()
    {
        if (!s_worldAssetsLoaded) return;

        foreach (var loader in s_assetLoaders)
        {
            if (!loader.Locations.HasFlag(LoadLocations.WorldDatapack)) continue;
            if (!loader.LoadedAssetsModify.HasFlag(LoadLocations.WorldDatapack)) continue;
            loader.Clear();
        }

        await LoadBaseAssets(LoadLocations.WorldDatapack);
        await LoadDatapackAssets(s_lastDataPath, LoadLocations.WorldDatapack);
        await LoadWorldAssets(s_lastWorldDataPath, LoadLocations.WorldDatapack);
        await LoadResourcepackAssets(s_lastResourcePath, LoadLocations.WorldDatapack);
    }

    public static async Task ResetResourcepackAssets()
    {
        foreach (var loader in s_assetLoaders)
        {
            if (!loader.Locations.HasFlag(LoadLocations.Resourcepack)) continue;
            if (!loader.LoadedAssetsModify.HasFlag(LoadLocations.Resourcepack)) continue;
            loader.Clear();
        }

        await LoadBaseAssets(LoadLocations.Resourcepack);
        await LoadDatapackAssets(s_lastDataPath, LoadLocations.Resourcepack);
        await LoadWorldAssets(s_lastWorldDataPath, LoadLocations.Resourcepack);
        await LoadResourcepackAssets(s_lastResourcePath, LoadLocations.Resourcepack);
    }

    private protected abstract Task OnLoadAssets(string path, bool namespaced, LoadLocations location);
    private protected abstract void Clear();
}
