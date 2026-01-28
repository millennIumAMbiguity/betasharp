namespace betareborn.Guis
{
    public class GuiChat : GuiScreen
    {

        protected String message = "";
        private int updateCounter = 0;
        private static readonly String field_20082_i = ChatAllowedCharacters.allowedCharacters;

        public override void initGui()
        {
            Keyboard.enableRepeatEvents(true);
        }

        public override void onGuiClosed()
        {
            Keyboard.enableRepeatEvents(false);
        }

        public override void updateScreen()
        {
            ++updateCounter;
        }

        protected override void keyTyped(char var1, int var2)
        {
            if (var2 == 1)
            {
                mc.displayGuiScreen((GuiScreen)null);
            }
            else if (var2 == 28)
            {
                String var3 = message.Trim();
                if (var3.Length > 0)
                {
                    String var4 = message.Trim();
                    if (!mc.lineIsCommand(var4))
                    {
                        mc.thePlayer.sendChatMessage(var4);
                    }
                }

                mc.displayGuiScreen((GuiScreen)null);
            }
            else
            {
                if (var2 == 14 && message.Length > 0)
                {
                    message = message.Substring(0, message.Length - 1);
                }

                if (field_20082_i.IndexOf(var1) >= 0 && message.Length < 100)
                {
                    message = message + var1;
                }

            }
        }

        public override void drawScreen(int var1, int var2, float var3)
        {
            drawRect(2, height - 14, width - 2, height - 2, java.lang.Integer.MIN_VALUE);
            drawString(fontRenderer, "> " + message + (updateCounter / 6 % 2 == 0 ? "_" : ""), 4, height - 12, 14737632);
            base.drawScreen(var1, var2, var3);
        }

        protected override void mouseClicked(int var1, int var2, int var3)
        {
            if (var3 == 0)
            {
                if (mc.ingameGUI.field_933_a != null)
                {
                    if (message.Length > 0 && !message.EndsWith(" "))
                    {
                        message = message + " ";
                    }

                    message = message + mc.ingameGUI.field_933_a;
                    byte var4 = 100;
                    if (message.Length > var4)
                    {
                        message = message.Substring(0, var4);
                    }
                }
                else
                {
                    base.mouseClicked(var1, var2, var3);
                }
            }

        }
    }

}