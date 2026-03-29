namespace BetaSharp.DataAsset;

/// <summary>
/// Essentially a safe pointer.<br/>
/// Allows the Asset to be updated from the <see cref="AssetLoader{T}"/>
/// </summary>
public class AssetRef<T>(T asset)
{
    public T Asset { get; internal set; } = asset;

    public static implicit operator T(AssetRef<T> n) => n.Asset;
}
