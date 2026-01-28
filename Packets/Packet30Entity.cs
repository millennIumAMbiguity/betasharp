using java.io;

namespace betareborn.Packets
{
    public class Packet30Entity : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet30Entity).TypeHandle);

        public int entityId;
        public sbyte xPosition;
        public sbyte yPosition;
        public sbyte zPosition;
        public sbyte yaw;
        public sbyte pitch;
        public bool rotating = false;

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
            var1.handleEntity(this);
        }

        public override int getPacketSize()
        {
            return 4;
        }
    }

}