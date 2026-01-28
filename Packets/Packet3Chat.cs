using java.io;

namespace betareborn.Packets
{
    public class Packet3Chat : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet3Chat).TypeHandle);

        public String message;

        public Packet3Chat()
        {
        }

        public Packet3Chat(String var1)
        {
            if (var1.Length > 119)
            {
                var1 = var1.Substring(0, 119);
            }

            this.message = var1;
        }

        public override void readPacketData(DataInputStream var1)
        {
            this.message = readString(var1, 119);
        }

        public override void writePacketData(DataOutputStream var1)
        {
            writeString(this.message, var1);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.handleChat(this);
        }

        public override int getPacketSize()
        {
            return this.message.Length;
        }
    }

}