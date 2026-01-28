using betareborn.NBT;

namespace betareborn.TileEntities
{
    public class TileEntityRecordPlayer : TileEntity
    {
        public int record;

        public override void readFromNBT(NBTTagCompound var1)
        {
            base.readFromNBT(var1);
            record = var1.getInteger("Record");
        }

        public override void writeToNBT(NBTTagCompound var1)
        {
            base.writeToNBT(var1);
            if (record > 0)
            {
                var1.setInteger("Record", record);
            }

        }
    }

}