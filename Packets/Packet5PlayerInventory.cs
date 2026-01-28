using java.io;

namespace betareborn.Packets
{
    public class Packet5PlayerInventory : Packet
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(Packet5PlayerInventory).TypeHandle);

        public int entityID;
        public int slot;
        public int itemID;
        public int itemDamage;

        public override void readPacketData(DataInputStream var1)
        {
            this.entityID = var1.readInt();
            this.slot = var1.readShort();
            this.itemID = var1.readShort();
            this.itemDamage = var1.readShort();
        }

        public override void writePacketData(DataOutputStream var1)
        {
            var1.writeInt(this.entityID);
            var1.writeShort(this.slot);
            var1.writeShort(this.itemID);
            var1.writeShort(this.itemDamage);
        }

        public override void processPacket(NetHandler var1)
        {
            var1.handlePlayerInventory(this);
        }

        public override int getPacketSize()
        {
            return 8;
        }
    }

}