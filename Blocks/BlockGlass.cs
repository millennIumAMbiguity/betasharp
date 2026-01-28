using betareborn.Materials;

namespace betareborn.Blocks
{
    public class BlockGlass : BlockBreakable
    {
        public BlockGlass(int var1, int var2, Material var3, bool var4) : base(var1, var2, var3, var4)
        {
        }

        public override int quantityDropped(java.util.Random var1)
        {
            return 0;
        }

        public override int getRenderBlockPass()
        {
            return 0;
        }
    }

}