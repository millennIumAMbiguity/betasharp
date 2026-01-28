using betareborn.Blocks;
using betareborn.NBT;
using betareborn.Worlds;
using java.lang;
using java.util;

namespace betareborn.TileEntities
{
    public class TileEntity : java.lang.Object
    {
        public static readonly Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(TileEntity).TypeHandle);
        private static readonly Map nameToClassMap = new HashMap();
        private static readonly Map classToNameMap = new HashMap();
        public World worldObj;
        public int xCoord;
        public int yCoord;
        public int zCoord;
        protected bool field_31007_h;

        public TileEntity()
        {
        }

        private static void addMapping(Class var0, string var1)
        {
            if (classToNameMap.containsKey(var1))
            {
                throw new IllegalArgumentException("Duplicate id: " + var1);
            }
            else
            {
                nameToClassMap.put(var1, var0);
                classToNameMap.put(var0, var1);
            }
        }

        public virtual void readFromNBT(NBTTagCompound var1)
        {
            xCoord = var1.getInteger("x");
            yCoord = var1.getInteger("y");
            zCoord = var1.getInteger("z");
        }

        public virtual void writeToNBT(NBTTagCompound var1)
        {
            string var2 = (string)classToNameMap.get(getClass());
            if (var2 == null)
            {
                throw new RuntimeException(getClass() + " is missing a mapping! This is a bug!");
            }
            else
            {
                var1.setString("id", var2);
                var1.setInteger("x", xCoord);
                var1.setInteger("y", yCoord);
                var1.setInteger("z", zCoord);
            }
        }

        public virtual void updateEntity()
        {
        }

        public static TileEntity createAndLoadEntity(NBTTagCompound var0)
        {
            TileEntity var1 = null;

            try
            {
                Class var2 = (Class)nameToClassMap.get(var0.getString("id"));
                if (var2 != null)
                {
                    var1 = (TileEntity)var2.newInstance();
                }
            }
            catch (java.lang.Exception var3)
            {
                var3.printStackTrace();
            }

            if (var1 != null)
            {
                var1.readFromNBT(var0);
            }
            else
            {
                java.lang.System.@out.println("Skipping TileEntity with id " + var0.getString("id"));
            }

            return var1;
        }

        public virtual int getBlockMetadata()
        {
            return worldObj.getBlockMetadata(xCoord, yCoord, zCoord);
        }

        public void onInventoryChanged()
        {
            if (worldObj != null)
            {
                worldObj.func_698_b(xCoord, yCoord, zCoord, this);
            }

        }

        public double getDistanceFrom(double var1, double var3, double var5)
        {
            double var7 = (double)xCoord + 0.5D - var1;
            double var9 = (double)yCoord + 0.5D - var3;
            double var11 = (double)zCoord + 0.5D - var5;
            return var7 * var7 + var9 * var9 + var11 * var11;
        }

        public Block getBlockType()
        {
            return Block.blocksList[worldObj.getBlockId(xCoord, yCoord, zCoord)];
        }

        public bool func_31006_g()
        {
            return field_31007_h;
        }

        public void func_31005_i()
        {
            field_31007_h = true;
        }

        public void func_31004_j()
        {
            field_31007_h = false;
        }

        static TileEntity()
        {
            addMapping(new TileEntityFurnace().getClass(), "Furnace");
            addMapping(new TileEntityChest().getClass(), "Chest");
            addMapping(new TileEntityRecordPlayer().getClass(), "RecordPlayer");
            addMapping(new TileEntityDispenser().getClass(), "Trap");
            addMapping(new TileEntitySign().getClass(), "Sign");
            addMapping(new TileEntityMobSpawner().getClass(), "MobSpawner");
            addMapping(new TileEntityNote().getClass(), "Music");
            addMapping(new TileEntityPiston().getClass(), "Piston");
        }
    }
}