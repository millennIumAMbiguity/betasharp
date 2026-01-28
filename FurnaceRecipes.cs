using betareborn.Blocks;
using betareborn.Items;
using java.lang;
using java.util;

namespace betareborn
{
    public class FurnaceRecipes
    {
        private static readonly FurnaceRecipes smeltingBase = new();
        private Map smeltingList = new HashMap();

        public static FurnaceRecipes smelting()
        {
            return smeltingBase;
        }

        private FurnaceRecipes()
        {
            addSmelting(Block.oreIron.blockID, new ItemStack(Item.ingotIron));
            addSmelting(Block.oreGold.blockID, new ItemStack(Item.ingotGold));
            addSmelting(Block.oreDiamond.blockID, new ItemStack(Item.diamond));
            addSmelting(Block.sand.blockID, new ItemStack(Block.glass));
            addSmelting(Item.porkRaw.shiftedIndex, new ItemStack(Item.porkCooked));
            addSmelting(Item.fishRaw.shiftedIndex, new ItemStack(Item.fishCooked));
            addSmelting(Block.cobblestone.blockID, new ItemStack(Block.stone));
            addSmelting(Item.clay.shiftedIndex, new ItemStack(Item.brick));
            addSmelting(Block.cactus.blockID, new ItemStack(Item.dyePowder, 1, 2));
            addSmelting(Block.wood.blockID, new ItemStack(Item.coal, 1, 1));
        }

        public void addSmelting(int var1, ItemStack var2)
        {
            smeltingList.put(Integer.valueOf(var1), var2);
        }

        public ItemStack getSmeltingResult(int var1)
        {
            return (ItemStack)smeltingList.get(Integer.valueOf(var1));
        }

        public Map getSmeltingList()
        {
            return smeltingList;
        }
    }

}