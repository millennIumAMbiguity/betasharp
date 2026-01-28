using java.io;

namespace betareborn.Packets
{
    public class Packet70Bed : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet70Bed).TypeHandle);

        public static readonly String[] field_25020_a = new String[] { "tile.bed.notValid", null, null };
        public int field_25019_b;

        public override void readPacketData(DataInputStream var1)
        {
            this.field_25019_b = (sbyte)var1.readByte();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeByte(this.field_25019_b);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.func_25118_a(this);
        }

        public override int getPacketSize()
        {
            return 1;
        }
    }

}