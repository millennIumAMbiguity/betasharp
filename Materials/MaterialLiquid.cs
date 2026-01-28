namespace betareborn.Materials
{
    public class MaterialLiquid : Material
    {

        public MaterialLiquid(MapColor var1) : base(var1)
        {
            setIsGroundCover();
            setNoPushMobility();
        }

        public override bool getIsLiquid()
        {
            return true;
        }

        public override bool getIsSolid()
        {
            return false;
        }

        public override bool isSolid()
        {
            return false;
        }
    }

}