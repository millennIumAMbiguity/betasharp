namespace betareborn.Worlds
{
    public class WorldBlockPositionType
    {
        public int field_1202_a;
        public int field_1201_b;
        public int field_1207_c;
        public int field_1206_d;
        public int field_1205_e;
        public int field_1204_f;
        public readonly WorldClient field_1203_g;

        public WorldBlockPositionType(WorldClient var1, int var2, int var3, int var4, int var5, int var6)
        {
            field_1203_g = var1;
            field_1202_a = var2;
            field_1201_b = var3;
            field_1207_c = var4;
            field_1206_d = 80;
            field_1205_e = var5;
            field_1204_f = var6;
        }
    }

}