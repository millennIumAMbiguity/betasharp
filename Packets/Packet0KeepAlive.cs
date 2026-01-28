using java.io;

namespace betareborn.Packets
{
    public class Packet0KeepAlive : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet0KeepAlive).TypeHandle);

        public override void processPacket(NetHandler var1)
        {
        }

        public override void readPacketData(DataInputStream var1)
        {
        }

        public override void writePacketData(DataOutputStream var1)
        {
        }

        public override int getPacketSize()
        {
            return 0;
        }
    }

}