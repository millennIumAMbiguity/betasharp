using java.io;

namespace betareborn.Packets
{
    public class Packet6SpawnPosition : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet6SpawnPosition).TypeHandle);

        public int xPosition;
        public int yPosition;
        public int zPosition;

        public override void readPacketData(DataInputStream var1)
        {
            this.xPosition = var1.readInt();
            this.yPosition = var1.readInt();
            this.zPosition = var1.readInt();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeInt(this.xPosition);
            var1.writeInt(this.yPosition);
            var1.writeInt(this.zPosition);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.handleSpawnPosition(this);
        }

        public override int getPacketSize()
        {
            return 12;
        }
    }

}