using java.io;

namespace betareborn
{
    public class RegionFileChunkBuffer : ByteArrayOutputStream
    {
        private readonly int field_22283_b;
        private readonly int field_22285_c;
        private readonly RegionFile field_22284_a;

        public RegionFileChunkBuffer(RegionFile var1, int var2, int var3) : base(8096)
        {
            field_22284_a = var1;
            field_22283_b = var2;
            field_22285_c = var3;
        }

        public override void close()
        {
            field_22284_a.write(field_22283_b, field_22285_c, buf, count);
        }
    }
}