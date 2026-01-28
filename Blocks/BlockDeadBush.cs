namespace betareborn.Blocks
{
    public class BlockDeadBush : BlockFlower
    {

        public BlockDeadBush(int var1, int var2) : base(var1, var2)
        {
            float var3 = 0.4F;
            setBlockBounds(0.5F - var3, 0.0F, 0.5F - var3, 0.5F + var3, 0.8F, 0.5F + var3);
        }

        protected override bool canThisPlantGrowOnThisBlockID(int var1)
        {
            return var1 == Block.sand.blockID;
        }

        public override int getBlockTextureFromSideAndMetadata(int var1, int var2)
        {
            return blockIndexInTexture;
        }

        public override int idDropped(int var1, java.util.Random var2)
        {
            return -1;
        }
    }

}