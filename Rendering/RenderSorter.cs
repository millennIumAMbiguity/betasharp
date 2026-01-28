using betareborn.Entities;
using betareborn.Worlds;

namespace betareborn.Rendering
{
    public class RenderSorter : IComparer<WorldRenderer>
    {
        private EntityLiving baseEntity;

        public RenderSorter(EntityLiving entity)
        {
            baseEntity = entity;
        }

        public int Compare(WorldRenderer? var1, WorldRenderer? var2)
        {
            if (var1 == null || var2 == null)
            {
                throw new Exception("Null world renderer");
            }

            bool var3 = var1.isInFrustum;
            bool var4 = var2.isInFrustum;

            if (var3 && !var4)
            {
                return 1;
            }
            else if (var4 && !var3)
            {
                return -1;
            }
            else
            {
                double var5 = (double)var1.distanceToEntitySquared(baseEntity);
                double var7 = (double)var2.distanceToEntitySquared(baseEntity);
                return var5 < var7 ? 1 : (var5 > var7 ? -1 : (var1.chunkIndex < var2.chunkIndex ? 1 : -1));
            }
        }
    }
}