namespace betareborn.Items
{
    public class ItemPiston : ItemBlock
    {

        public ItemPiston(int var1) : base(var1)
        {
        }

        public override int getPlacedBlockMetadata(int var1)
        {
            return 7;
        }
    }

}