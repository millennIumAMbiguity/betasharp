using java.io;

namespace betareborn.Packets
{
    public class Packet105UpdateProgressbar : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet105UpdateProgressbar).TypeHandle);

        public int windowId;
        public int progressBar;
        public int progressBarValue;

        public override void processPacket(NetHandler var1)
        {
            var1.func_20090_a(this);
        }

        public override void readPacketData(DataInputStream var1)
        {
            this.windowId = (sbyte)var1.readByte();
            this.progressBar = var1.readShort();
            this.progressBarValue = var1.readShort();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeByte(this.windowId);
            var1.writeShort(this.progressBar);
            var1.writeShort(this.progressBarValue);
        }

        public override int getPacketSize()
        {
            return 5;
        }
    }

}