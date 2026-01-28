using java.io;

namespace betareborn.Packets
{
    public class Packet8UpdateHealth : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet8UpdateHealth).TypeHandle);

        public int healthMP;

        public override void readPacketData(DataInputStream var1)
        {
            this.healthMP = var1.readShort();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeShort(this.healthMP);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.handleHealth(this);
        }

        public override int getPacketSize()
        {
            return 2;
        }
    }

}