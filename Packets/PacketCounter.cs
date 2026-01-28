namespace betareborn.Packets
{
    public class PacketCounter
    {
        private int totalPackets;
        private long totalBytes;

        private PacketCounter()
        {
        }

        public void addPacket(int var1)
        {
            ++totalPackets;
            totalBytes += (long)var1;
        }

        public PacketCounter(Empty1 var1) : this()
        {
        }
    }

}