using BetaSharp.Util.Maths;

namespace BetaSharp.Worlds.Core;

public interface IWorldChuckFormat
{
    int X1 { get; }
    int Y1 { get; }
    int Z1 { get; }

    int GetIndex(Vec3i pos) => GetIndex(pos.X, pos.Y, pos.Z);
    int GetIndex(int x, int y, int z);
    int GetIndex(int x, int z);
    Vec3i GetPos(int i);

    int GetNibIndex(Vec3i pos);
    int GetNibIndex(int x, int y, int z);

    public static int Map(IWorldChuckFormat from, IWorldChuckFormat to, int i) => to.GetIndex(from.GetPos(i));
    public static int MapNib(IWorldChuckFormat from, IWorldChuckFormat to, int i) => to.GetNibIndex(from.GetPos(i));
}

public class JavaChuckFormat : IWorldChuckFormat
{
    public int X1 => 2048;
    public int Y1 => 1;
    public int Z1 => 128;
    public int GetIndex(int x, int y, int z) => (x << 11) | (z << 7) | y;
    public int GetIndex(int x, int z) => (x << 11) | (z << 7);
    public Vec3i GetPos(int i) => new((i >> 11) & 15, i & 127, (i >> 7) & 15);

    public int GetNibIndex(Vec3i pos) => GetIndex(pos.X, pos.Y, pos.Z) >> 1;
    public int GetNibIndex(int x, int y, int z) => GetIndex(x, y, z) >> 1;
}

public class YxzChuckFormat : IWorldChuckFormat
{
    public int X1 => 8;
    public int Y1 => 256;
    public int Z1 => 1;
    public int GetIndex(int x, int y, int z) => (y << 8) | (x << 4) | z;
    public int GetIndex(int x, int z) => (x << 4) | z;
    public Vec3i GetPos(int i) => new ((i >> 4) & 15, (i >> 8) & 255, i & 15);

    public int GetNibIndex(Vec3i pos) => GetIndex(pos.X, pos.Y >> 1, pos.Z);
    public int GetNibIndex(int x, int y, int z) => GetIndex(x, y >> 1, z);
}
