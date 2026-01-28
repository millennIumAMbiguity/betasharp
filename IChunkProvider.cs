using betareborn.Chunks;

namespace betareborn
{
    public interface IChunkProvider
    {
        bool chunkExists(int var1, int var2);

        Chunk provideChunk(int var1, int var2);

        Chunk prepareChunk(int var1, int var2);

        void populate(IChunkProvider var1, int var2, int var3);

        bool saveChunks(bool var1, IProgressUpdate var2);

        bool unload100OldestChunks();
        void markChunksForUnload(int renderDistanceChunks);

        bool canSave();

        string makeString();
    }

}