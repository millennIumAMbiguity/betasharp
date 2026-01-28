namespace betareborn.Chunks
{
    public class ChunkPosition : java.lang.Object
    {
        public readonly int x;
        public readonly int y;
        public readonly int z;

        public ChunkPosition(int var1, int var2, int var3)
        {
            x = var1;
            y = var2;
            z = var3;
        }

        public override bool equals(object var1)
        {
            if (var1 is not ChunkPosition)
            {
                return false;
            }
            else
            {
                ChunkPosition var2 = (ChunkPosition)var1;
                return var2.x == x && var2.y == y && var2.z == z;
            }
        }

        public override int hashCode()
        {
            return x * 8976890 + y * 981131 + z;
        }
    }

}