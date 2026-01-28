using java.io;

namespace betareborn.Packets
{
    public class Packet32EntityLook : Packet30Entity
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet32EntityLook).TypeHandle);

        public Packet32EntityLook()
        {
            this.rotating = true;
        }

        public override void readPacketData(DataInputStream var1)
        {
            base.readPacketData(var1);
            this.yaw = (sbyte)var1.readByte();
            this.pitch = (sbyte)var1.readByte();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            base.writePacketData(var1);
            var1.writeByte(this.yaw);
            var1.writeByte(this.pitch);
        }

        public override int getPacketSize()
        {
            return 6;
        }
    }

}