using betareborn.NBT;

namespace betareborn
{
    public abstract class MapDataBase : java.lang.Object
    {
        public readonly string field_28168_a;
        private bool dirty;

        public MapDataBase(JString var1)
        {
            field_28168_a = var1.value;
        }

        public abstract void readFromNBT(NBTTagCompound var1);

        public abstract void writeToNBT(NBTTagCompound var1);

        public void markDirty()
        {
            setDirty(true);
        }

        public void setDirty(bool var1)
        {
            dirty = var1;
        }

        public bool isDirty()
        {
            return dirty;
        }
    }

}