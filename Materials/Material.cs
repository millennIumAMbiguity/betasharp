namespace betareborn.Materials
{
    public class Material : java.lang.Object
    {
        public static readonly Material air = new MaterialTransparent(MapColor.airColor);
        public static readonly Material grassMaterial = new(MapColor.grassColor);
        public static readonly Material ground = new(MapColor.dirtColor);
        public static readonly Material wood = (new Material(MapColor.woodColor)).setBurning();
        public static readonly Material rock = (new Material(MapColor.stoneColor)).setNoHarvest();
        public static readonly Material iron = (new Material(MapColor.ironColor)).setNoHarvest();
        public static readonly Material water = (new MaterialLiquid(MapColor.waterColor)).setNoPushMobility();
        public static readonly Material lava = (new MaterialLiquid(MapColor.tntColor)).setNoPushMobility();
        public static readonly Material leaves = (new Material(MapColor.foliageColor)).setBurning().setIsTranslucent().setNoPushMobility();
        public static readonly Material plants = (new MaterialLogic(MapColor.foliageColor)).setNoPushMobility();
        public static readonly Material sponge = new(MapColor.clothColor);
        public static readonly Material cloth = (new Material(MapColor.clothColor)).setBurning();
        public static readonly Material fire = (new MaterialTransparent(MapColor.airColor)).setNoPushMobility();
        public static readonly Material sand = new(MapColor.sandColor);
        public static readonly Material circuits = (new MaterialLogic(MapColor.airColor)).setNoPushMobility();
        public static readonly Material glass = (new Material(MapColor.airColor)).setIsTranslucent();
        public static readonly Material tnt = (new Material(MapColor.tntColor)).setBurning().setIsTranslucent();
        public static readonly Material field_4262_q = (new Material(MapColor.foliageColor)).setNoPushMobility();
        public static readonly Material ice = (new Material(MapColor.iceColor)).setIsTranslucent();
        public static readonly Material snow = (new MaterialLogic(MapColor.snowColor)).setIsGroundCover().setIsTranslucent().setNoHarvest().setNoPushMobility();
        public static readonly Material builtSnow = (new Material(MapColor.snowColor)).setNoHarvest();
        public static readonly Material cactus = (new Material(MapColor.foliageColor)).setIsTranslucent().setNoPushMobility();
        public static readonly Material clay = new(MapColor.clayColor);
        public static readonly Material pumpkin = (new Material(MapColor.foliageColor)).setNoPushMobility();
        public static readonly Material portal = (new MaterialPortal(MapColor.airColor)).setImmovableMobility();
        public static readonly Material cakeMaterial = (new Material(MapColor.airColor)).setNoPushMobility();
        public static readonly Material field_31068_A = (new Material(MapColor.clothColor)).setNoHarvest().setNoPushMobility();
        public static readonly Material field_31067_B = (new Material(MapColor.stoneColor)).setImmovableMobility();
        private bool canBurn;
        private bool groundCover;
        private bool isOpaque;
        public readonly MapColor materialMapColor;
        private bool canHarvest = true;
        private int mobilityFlag;

        public Material(MapColor var1)
        {
            materialMapColor = var1;
        }

        public virtual bool getIsLiquid()
        {
            return false;
        }

        public virtual bool isSolid()
        {
            return true;
        }

        public virtual bool getCanBlockGrass()
        {
            return true;
        }

        public virtual bool getIsSolid()
        {
            return true;
        }

        private Material setIsTranslucent()
        {
            isOpaque = true;
            return this;
        }

        private Material setNoHarvest()
        {
            canHarvest = false;
            return this;
        }

        private Material setBurning()
        {
            canBurn = true;
            return this;
        }

        public bool getBurning()
        {
            return canBurn;
        }

        public Material setIsGroundCover()
        {
            groundCover = true;
            return this;
        }

        public bool getIsGroundCover()
        {
            return groundCover;
        }

        public bool getIsTranslucent()
        {
            return isOpaque ? false : getIsSolid();
        }

        public bool getIsHarvestable()
        {
            return canHarvest;
        }

        public int getMaterialMobility()
        {
            return mobilityFlag;
        }

        protected Material setNoPushMobility()
        {
            mobilityFlag = 1;
            return this;
        }

        protected Material setImmovableMobility()
        {
            mobilityFlag = 2;
            return this;
        }
    }

}