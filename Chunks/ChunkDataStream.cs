using java.io;

namespace betareborn.Chunks
{
    public class ChunkDataStream(DataInputStream stream, byte compressionType)
    {
        private readonly DataInputStream stream = stream;
        private readonly byte compressionType = compressionType;

        public DataInputStream getInputStream() => stream;
        public byte getCompressionType() => compressionType;
    }
}
