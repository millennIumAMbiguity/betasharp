using java.io;

namespace betareborn.Packets
{
    public class Packet29DestroyEntity : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet29DestroyEntity).TypeHandle);

        public int entityId;

        public override void readPacketData(DataInputStream var1)
        {
            this.entityId = var1.readInt();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeInt(this.entityId);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.handleDestroyEntity(this);
        }

        public override int getPacketSize()
        {
            return 4;
        }
    }

}