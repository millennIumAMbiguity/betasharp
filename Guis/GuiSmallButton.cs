namespace betareborn.Guis
{
    public class GuiSmallButton : GuiButton
    {

        private readonly EnumOptions enumOptions;

        public GuiSmallButton(int var1, int var2, int var3, String var4) : this(var1, var2, var3, null, var4)
        {
        }

        public GuiSmallButton(int var1, int var2, int var3, int var4, int var5, String var6) : base(var1, var2, var3, var4, var5, var6)
        {
            enumOptions = null;
        }

        public GuiSmallButton(int var1, int var2, int var3, EnumOptions var4, String var5) : base(var1, var2, var3, 150, 20, var5)
        {
            enumOptions = var4;
        }

        public EnumOptions returnEnumOptions()
        {
            return enumOptions;
        }
    }

}