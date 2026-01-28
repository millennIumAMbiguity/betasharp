using java.io;

namespace betareborn.Packets
{
    public class Packet38EntityStatus : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet38EntityStatus).TypeHandle);

        public int entityId;
        public sbyte entityStatus;

        public override void readPacketData(DataInputStream var1)
        {
            this.entityId = var1.readInt();
            this.entityStatus = (sbyte)var1.readByte();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeInt(this.entityId);
            var1.writeByte(this.entityStatus);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.func_9447_a(this);
        }

        public override int getPacketSize()
        {
            return 5;
        }
    }

}