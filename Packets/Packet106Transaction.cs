using java.io;

namespace betareborn.Packets
{
    public class Packet106Transaction : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet106Transaction).TypeHandle);

        public int windowId;
        public short field_20028_b;
        public bool field_20030_c;

        public Packet106Transaction()
        {
        }

        public Packet106Transaction(int var1, short var2, bool var3)
        {
            this.windowId = var1;
            this.field_20028_b = var2;
            this.field_20030_c = var3;
        }

        public override void processPacket(NetHandler var1)
        {
            var1.func_20089_a(this);
        }

        public override void readPacketData(DataInputStream var1)
        {
            this.windowId = (sbyte)var1.readByte();
            this.field_20028_b = var1.readShort();
            this.field_20030_c = (sbyte)var1.readByte() != 0;
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeByte(this.windowId);
            var1.writeShort(this.field_20028_b);
            var1.writeByte(this.field_20030_c ? 1 : 0);
        }

        public override int getPacketSize()
        {
            return 4;
        }
    }

}