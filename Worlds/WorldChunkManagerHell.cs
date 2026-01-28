using betareborn.Biomes;
using betareborn.Chunks;
using java.util;

namespace betareborn.Worlds
{
    public class WorldChunkManagerHell : WorldChunkManager
    {

        private BiomeGenBase field_4201_e;
        private double field_4200_f;
        private double field_4199_g;

        public WorldChunkManagerHell(BiomeGenBase var1, double var2, double var4)
        {
            field_4201_e = var1;
            field_4200_f = var2;
            field_4199_g = var4;
        }

        public override BiomeGenBase getBiomeGenAtChunkCoord(ChunkCoordIntPair var1)
        {
            return field_4201_e;
        }

        public override BiomeGenBase getBiomeGenAt(int var1, int var2)
        {
            return field_4201_e;
        }

        public override double getTemperature(int var1, int var2)
        {
            return field_4200_f;
        }

        public override BiomeGenBase[] func_4069_a(int var1, int var2, int var3, int var4)
        {
            field_4195_d = loadBlockGeneratorData(field_4195_d, var1, var2, var3, var4);
            return field_4195_d;
        }

        public override double[] getTemperatures(double[] var1, int var2, int var3, int var4, int var5)
        {
            if (var1 == null || var1.Length < var4 * var5)
            {
                var1 = new double[var4 * var5];
            }

            Arrays.fill(var1, 0, var4 * var5, field_4200_f);
            return var1;
        }

        public override BiomeGenBase[] loadBlockGeneratorData(BiomeGenBase[] var1, int var2, int var3, int var4, int var5)
        {
            if (var1 == null || var1.Length < var4 * var5)
            {
                var1 = new BiomeGenBase[var4 * var5];
            }

            if (temperature == null || temperature.Length < var4 * var5)
            {
                temperature = new double[var4 * var5];
                humidity = new double[var4 * var5];
            }

            Arrays.fill(var1, 0, var4 * var5, field_4201_e);
            Arrays.fill(humidity, 0, var4 * var5, field_4199_g);
            Arrays.fill(temperature, 0, var4 * var5, field_4200_f);
            return var1;
        }
    }

}