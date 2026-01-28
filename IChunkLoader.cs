using betareborn.Chunks;
using betareborn.Worlds;

namespace betareborn
{
    public interface IChunkLoader
    {
        Chunk loadChunk(World world, int chunkX, int chunkZ);

        void saveChunk(World world, Chunk chunk, Action onSave, long sequence);

        void saveExtraChunkData(World world, Chunk chunk);

        void func_814_a();

        void saveExtraData();

        void flushToDisk();
    }

}