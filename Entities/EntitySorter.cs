using betareborn.Entities;
using betareborn.Worlds;
using java.util;
using java.util.function;

namespace betareborn.Rendering
{

    public class EntitySorter : Comparator
    {

        private double field_30008_a;
        private double field_30007_b;
        private double field_30009_c;

        public EntitySorter(Entity var1)
        {
            field_30008_a = -var1.posX;
            field_30007_b = -var1.posY;
            field_30009_c = -var1.posZ;
        }

        public int sortByDistanceToEntity(WorldRenderer var1, WorldRenderer var2)
        {
            double var3 = (double)var1.posXPlus + field_30008_a;
            double var5 = (double)var1.posYPlus + field_30007_b;
            double var7 = (double)var1.posZPlus + field_30009_c;
            double var9 = (double)var2.posXPlus + field_30008_a;
            double var11 = (double)var2.posYPlus + field_30007_b;
            double var13 = (double)var2.posZPlus + field_30009_c;
            return (int)((var3 * var3 + var5 * var5 + var7 * var7 - (var9 * var9 + var11 * var11 + var13 * var13)) * 1024.0D);
        }

        public int compare(object var1, object var2)
        {
            return sortByDistanceToEntity((WorldRenderer)var1, (WorldRenderer)var2);
        }

        public Comparator thenComparing(Comparator other)
        {
            throw new NotImplementedException();
        }

        public bool equals(object obj)
        {
            throw new NotImplementedException();
        }

        public Comparator reversed()
        {
            throw new NotImplementedException();
        }

        public Comparator thenComparing(Function keyExtractor, Comparator keyComparator)
        {
            throw new NotImplementedException();
        }

        public Comparator thenComparing(Function keyExtractor)
        {
            throw new NotImplementedException();
        }

        public Comparator thenComparingInt(ToIntFunction keyExtractor)
        {
            throw new NotImplementedException();
        }

        public Comparator thenComparingLong(ToLongFunction keyExtractor)
        {
            throw new NotImplementedException();
        }

        public Comparator thenComparingDouble(ToDoubleFunction keyExtractor)
        {
            throw new NotImplementedException();
        }
    }

}