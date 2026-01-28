using java.lang;

namespace betareborn.Chunks
{
    public readonly record struct ChunkCoordIntPair
    {
        public readonly int chunkXPos;
        public readonly int chunkZPos;

        public ChunkCoordIntPair(int var1, int var2)
        {
            chunkXPos = var1;
            chunkZPos = var2;
        }

        public static int chunkXZ2Int(int var0, int var1)
        {
            return (var0 < 0 ? Integer.MIN_VALUE : 0) | (var0 & Short.MAX_VALUE) << 16 | (var1 < 0 ? -Short.MIN_VALUE : 0) | var1 & Short.MAX_VALUE;
        }

        public override int GetHashCode()
        {
            return chunkXZ2Int(chunkXPos, chunkZPos);
        }

        public readonly bool Equals(ChunkCoordIntPair var1)
        {
            return var1.chunkXPos == chunkXPos && var1.chunkZPos == chunkZPos;
        }
    }

}