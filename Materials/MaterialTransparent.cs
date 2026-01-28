namespace betareborn.Materials
{
    public class MaterialTransparent : Material
    {

        public MaterialTransparent(MapColor var1) : base(var1)
        {
            setIsGroundCover();
        }

        public override bool isSolid()
        {
            return false;
        }

        public override bool getCanBlockGrass()
        {
            return false;
        }

        public override bool getIsSolid()
        {
            return false;
        }
    }

}