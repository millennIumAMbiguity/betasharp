namespace betareborn
{
    public class NibbleArray : java.lang.Object
    {
        public readonly byte[] data;

        public NibbleArray(int var1)
        {
            data = new byte[var1 >> 1];
        }

        public NibbleArray(byte[] var1)
        {
            data = var1;
        }

        public int getNibble(int var1, int var2, int var3)
        {
            int var4 = var1 << 11 | var3 << 7 | var2;
            int var5 = var4 >> 1;
            int var6 = var4 & 1;
            return var6 == 0 ? data[var5] & 15 : data[var5] >> 4 & 15;
        }

        public void setNibble(int var1, int var2, int var3, int var4)
        {
            int var5 = var1 << 11 | var3 << 7 | var2;
            int var6 = var5 >> 1;
            int var7 = var5 & 1;
            if (var7 == 0)
            {
                data[var6] = (byte)(data[var6] & 240 | var4 & 15);
            }
            else
            {
                data[var6] = (byte)(data[var6] & 15 | (var4 & 15) << 4);
            }

        }

        public bool isValid()
        {
            return data != null;
        }
    }
}