namespace BetaSharp.DataAsset;

public static class AssetNamespace
{
    private static readonly List<string> s_idToName = ["betasharp"];

    private static readonly Dictionary<string, int> s_nameToId = new(1)
    {
        { s_idToName[0], 0 }
    };

    public static int GetIndex(string name)
    {
        if (s_nameToId.TryGetValue(name, out int value)) return value;
        value = s_idToName.Count;
        s_idToName.Add(name);
        s_nameToId.Add(name, value);
        return value;
    }

    public static string? GetName(int id)
    {
        if (id < 0 || id >= s_idToName.Count) return null;
        return s_idToName[id];
    }

    public static int FindIndex(string name, bool allowShortName)
    {
        if (string.IsNullOrEmpty(name)) return -1;
        if (s_nameToId.TryGetValue(name, out int value)) return value;
        if (!allowShortName) return -1;
        if (name.Length == 1)
        {
            for (int i = 0; i < s_idToName.Count; i++)
            {
                if (s_idToName[i][0] == name[0]) return i;
            }
        }
        else
        {
            for (int i = 0; i < s_idToName.Count; i++)
            {
                string s = s_idToName[i];
                if (s.Length <= name.Length) continue;
                if (s.Substring(0, name.Length) == name) return i;
            }
        }

        return -1;
    }
}
