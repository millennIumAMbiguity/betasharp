using java.io;

namespace betareborn.Packets
{
    public class Packet31RelEntityMove : Packet30Entity
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet31RelEntityMove).TypeHandle);

        public override void readPacketData(DataInputStream var1)
        {
            base.readPacketData(var1);
            this.xPosition = (sbyte)var1.readByte();
            this.yPosition = (sbyte)var1.readByte();
            this.zPosition = (sbyte)var1.readByte();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            base.writePacketData(var1);
            var1.writeByte(this.xPosition);
            var1.writeByte(this.yPosition);
            var1.writeByte(this.zPosition);
        }

        public override int getPacketSize()
        {
            return 7;
        }
    }

}