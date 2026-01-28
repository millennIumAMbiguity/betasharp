using betareborn.Entities;
using betareborn.NBT;

namespace betareborn.TileEntities
{
    public class TileEntityMobSpawner : TileEntity
    {
        public static readonly new java.lang.Class Class = ikvm.runtime.Util.getClassFromTypeHandle(typeof(TileEntityMobSpawner).TypeHandle);

        public int delay = -1;
        private string mobID = "Pig";
        public double yaw;
        public double yaw2 = 0.0D;

        public TileEntityMobSpawner()
        {
            delay = 20;
        }

        public string getMobID()
        {
            return mobID;
        }

        public void setMobID(string var1)
        {
            mobID = var1;
        }

        public bool anyPlayerInRange()
        {
            return worldObj.getClosestPlayer((double)xCoord + 0.5D, (double)yCoord + 0.5D, (double)zCoord + 0.5D, 16.0D) != null;
        }

        public override void updateEntity()
        {
            yaw2 = yaw;
            if (anyPlayerInRange())
            {
                double var1 = (double)((float)xCoord + worldObj.rand.nextFloat());
                double var3 = (double)((float)yCoord + worldObj.rand.nextFloat());
                double var5 = (double)((float)zCoord + worldObj.rand.nextFloat());
                worldObj.spawnParticle("smoke", var1, var3, var5, 0.0D, 0.0D, 0.0D);
                worldObj.spawnParticle("flame", var1, var3, var5, 0.0D, 0.0D, 0.0D);

                for (yaw += (double)(1000.0F / ((float)delay + 200.0F)); yaw > 360.0D; yaw2 -= 360.0D)
                {
                    yaw -= 360.0D;
                }

                if (!worldObj.multiplayerWorld)
                {
                    if (delay == -1)
                    {
                        updateDelay();
                    }

                    if (delay > 0)
                    {
                        --delay;
                        return;
                    }

                    byte var7 = 4;

                    for (int var8 = 0; var8 < var7; ++var8)
                    {
                        EntityLiving var9 = (EntityLiving)((EntityLiving)EntityList.createEntityInWorld(mobID, worldObj));
                        if (var9 == null)
                        {
                            return;
                        }

                        int var10 = worldObj.getEntitiesWithinAABB(var9.getClass(), AxisAlignedBB.getBoundingBoxFromPool((double)xCoord, (double)yCoord, (double)zCoord, (double)(xCoord + 1), (double)(yCoord + 1), (double)(zCoord + 1)).expand(8.0D, 4.0D, 8.0D)).Count;
                        if (var10 >= 6)
                        {
                            updateDelay();
                            return;
                        }

                        if (var9 != null)
                        {
                            double var11 = (double)xCoord + (worldObj.rand.nextDouble() - worldObj.rand.nextDouble()) * 4.0D;
                            double var13 = (double)(yCoord + worldObj.rand.nextInt(3) - 1);
                            double var15 = (double)zCoord + (worldObj.rand.nextDouble() - worldObj.rand.nextDouble()) * 4.0D;
                            var9.setLocationAndAngles(var11, var13, var15, worldObj.rand.nextFloat() * 360.0F, 0.0F);
                            if (var9.getCanSpawnHere())
                            {
                                worldObj.entityJoinedWorld(var9);

                                for (int var17 = 0; var17 < 20; ++var17)
                                {
                                    var1 = (double)xCoord + 0.5D + ((double)worldObj.rand.nextFloat() - 0.5D) * 2.0D;
                                    var3 = (double)yCoord + 0.5D + ((double)worldObj.rand.nextFloat() - 0.5D) * 2.0D;
                                    var5 = (double)zCoord + 0.5D + ((double)worldObj.rand.nextFloat() - 0.5D) * 2.0D;
                                    worldObj.spawnParticle("smoke", var1, var3, var5, 0.0D, 0.0D, 0.0D);
                                    worldObj.spawnParticle("flame", var1, var3, var5, 0.0D, 0.0D, 0.0D);
                                }

                                var9.spawnExplosionParticle();
                                updateDelay();
                            }
                        }
                    }
                }

                base.updateEntity();
            }
        }

        private void updateDelay()
        {
            delay = 200 + worldObj.rand.nextInt(600);
        }

        public override void readFromNBT(NBTTagCompound var1)
        {
            base.readFromNBT(var1);
            mobID = var1.getString("EntityId");
            delay = var1.getShort("Delay");
        }

        public override void writeToNBT(NBTTagCompound var1)
        {
            base.writeToNBT(var1);
            var1.setString("EntityId", mobID);
            var1.setShort("Delay", (short)delay);
        }
    }

}