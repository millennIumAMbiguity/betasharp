using java.io;

namespace betareborn.Packets
{
    public class Packet22Collect : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet22Collect).TypeHandle);

        public int collectedEntityId;
        public int collectorEntityId;

        public override void readPacketData(DataInputStream var1)
        {
            this.collectedEntityId = var1.readInt();
            this.collectorEntityId = var1.readInt();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeInt(this.collectedEntityId);
            var1.writeInt(this.collectorEntityId);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.handleCollect(this);
        }

        public override int getPacketSize()
        {
            return 8;
        }
    }

}