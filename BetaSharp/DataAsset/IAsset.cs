using System.Text.Json.Serialization;

namespace BetaSharp.DataAsset;

public interface IAsset
{
    public string Name { get; internal set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int NamespaceId { get; internal set; }
}
