using betareborn.Blocks;
using betareborn.Items;
using betareborn.Stats;

namespace betareborn
{
    public class Achievement : StatBase
    {
        public readonly int displayColumn;
        public readonly int displayRow;
        public readonly Achievement parentAchievement;
        private readonly String achievementDescription;
        private IStatStringFormat statStringFormatter;
        public readonly ItemStack theItemStack;
        private bool isSpecial;

        public Achievement(int var1, string var2, int var3, int var4, Item var5, Achievement var6) : this(var1, var2, var3, var4, new ItemStack(var5), var6)
        {
        }

        public Achievement(int var1, string var2, int var3, int var4, Block var5, Achievement var6) : this(var1, var2, var3, var4, new ItemStack(var5), var6)
        {
        }

        public Achievement(int var1, string var2, int var3, int var4, ItemStack var5, Achievement var6) : base(5242880 + var1, StatCollector.translateToLocal("achievement." + var2))
        {
            theItemStack = var5;
            achievementDescription = StatCollector.translateToLocal("achievement." + var2 + ".desc");
            displayColumn = var3;
            displayRow = var4;
            if (var3 < AchievementList.minDisplayColumn)
            {
                AchievementList.minDisplayColumn = var3;
            }

            if (var4 < AchievementList.minDisplayRow)
            {
                AchievementList.minDisplayRow = var4;
            }

            if (var3 > AchievementList.maxDisplayColumn)
            {
                AchievementList.maxDisplayColumn = var3;
            }

            if (var4 > AchievementList.maxDisplayRow)
            {
                AchievementList.maxDisplayRow = var4;
            }

            parentAchievement = var6;
        }

        public Achievement func_27089_a()
        {
            field_27088_g = true;
            return this;
        }

        public Achievement setSpecial()
        {
            isSpecial = true;
            return this;
        }

        public Achievement registerAchievement()
        {
            base.registerStat();
            AchievementList.achievementList.add(this);
            return this;
        }

        public override bool func_25067_a()
        {
            return true;
        }

        public string getDescription()
        {
            return statStringFormatter != null ? statStringFormatter.formatString(achievementDescription) : achievementDescription;
        }

        public Achievement setStatStringFormatter(IStatStringFormat var1)
        {
            statStringFormatter = var1;
            return this;
        }

        public bool getSpecial()
        {
            return isSpecial;
        }

        public override StatBase registerStat()
        {
            return registerAchievement();
        }

        public override StatBase func_27082_h()
        {
            return func_27089_a();
        }
    }

}