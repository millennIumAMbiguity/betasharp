using betareborn.Entities;

namespace betareborn
{
    public class MapInfo
    {
        public readonly EntityPlayer entityplayerObj;
        public int[] field_28119_b;
        public int[] field_28124_c;
        private int field_28122_e;
        private int field_28121_f;
        readonly MapData mapDataObj;

        public MapInfo(MapData var1, EntityPlayer var2)
        {
            mapDataObj = var1;
            field_28119_b = new int[128];
            field_28124_c = new int[128];
            field_28122_e = 0;
            field_28121_f = 0;
            entityplayerObj = var2;

            for (int var3 = 0; var3 < field_28119_b.Length; ++var3)
            {
                field_28119_b[var3] = 0;
                field_28124_c[var3] = 127;
            }

        }
    }

}