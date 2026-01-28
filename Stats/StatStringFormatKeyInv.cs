namespace betareborn.Stats
{

    public class StatStringFormatKeyInv : IStatStringFormat
    {
        readonly Minecraft mc;


        public StatStringFormatKeyInv(Minecraft var1)
        {
            mc = var1;
        }

        public String formatString(String var1)
        {
            return String.Format(var1, new Object[] { Keyboard.getKeyName(this.mc.gameSettings.keyBindInventory.keyCode) });
        }
    }

}