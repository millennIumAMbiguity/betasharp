using java.util;
using Silk.NET.OpenGL.Legacy;

namespace betareborn.Guis
{
    public abstract class GuiSlot
    {
        private readonly Minecraft mc;
        private readonly int width;
        private readonly int height;
        protected readonly int top;
        protected readonly int bottom;
        private readonly int right;
        private readonly int left;
        protected readonly int posZ;
        private int scrollUpButtonID;
        private int scrollDownButtonID;
        private float initialClickY = -2.0F;
        private float scrollMultiplier;
        private float amountScrolled;
        private int selectedElement = -1;
        private long lastClicked = 0L;
        private bool field_25123_p = true;
        private bool field_27262_q;
        private int field_27261_r;

        public GuiSlot(Minecraft var1, int var2, int var3, int var4, int var5, int var6)
        {
            mc = var1;
            width = var2;
            height = var3;
            top = var4;
            bottom = var5;
            posZ = var6;
            left = 0;
            right = var2;
        }

        public void func_27258_a(bool var1)
        {
            field_25123_p = var1;
        }

        protected void func_27259_a(bool var1, int var2)
        {
            field_27262_q = var1;
            field_27261_r = var2;
            if (!var1)
            {
                field_27261_r = 0;
            }

        }

        public abstract int getSize();

        protected abstract void elementClicked(int var1, bool var2);

        protected abstract bool isSelected(int var1);

        protected virtual int getContentHeight()
        {
            return getSize() * posZ + field_27261_r;
        }

        protected abstract void drawBackground();

        protected abstract void drawSlot(int var1, int var2, int var3, int var4, Tessellator var5);

        protected virtual void func_27260_a(int var1, int var2, Tessellator var3)
        {
        }

        protected virtual void func_27255_a(int var1, int var2)
        {
        }

        protected virtual void func_27257_b(int var1, int var2)
        {
        }

        public int func_27256_c(int var1, int var2)
        {
            int var3 = width / 2 - 110;
            int var4 = width / 2 + 110;
            int var5 = var2 - top - field_27261_r + (int)amountScrolled - 4;
            int var6 = var5 / posZ;
            return var1 >= var3 && var1 <= var4 && var6 >= 0 && var5 >= 0 && var6 < getSize() ? var6 : -1;
        }

        public void registerScrollButtons(List var1, int var2, int var3)
        {
            scrollUpButtonID = var2;
            scrollDownButtonID = var3;
        }

        private void bindAmountScrolled()
        {
            int var1 = getContentHeight() - (bottom - top - 4);
            if (var1 < 0)
            {
                var1 /= 2;
            }

            if (amountScrolled < 0.0F)
            {
                amountScrolled = 0.0F;
            }

            if (amountScrolled > (float)var1)
            {
                amountScrolled = (float)var1;
            }

        }

        public void actionPerformed(GuiButton var1)
        {
            if (var1.enabled)
            {
                if (var1.id == scrollUpButtonID)
                {
                    amountScrolled -= (float)(posZ * 2 / 3);
                    initialClickY = -2.0F;
                    bindAmountScrolled();
                }
                else if (var1.id == scrollDownButtonID)
                {
                    amountScrolled += (float)(posZ * 2 / 3);
                    initialClickY = -2.0F;
                    bindAmountScrolled();
                }

            }
        }

        public void drawScreen(int var1, int var2, float var3)
        {
            drawBackground();
            int var4 = getSize();
            int var5 = width / 2 + 124;
            int var6 = var5 + 6;
            int var9;
            int var10;
            int var11;
            int var13;
            int var19;
            if (Mouse.isButtonDown(0))
            {
                if (initialClickY == -1.0F)
                {
                    bool var7 = true;
                    if (var2 >= top && var2 <= bottom)
                    {
                        int var8 = width / 2 - 110;
                        var9 = width / 2 + 110;
                        var10 = var2 - top - field_27261_r + (int)amountScrolled - 4;
                        var11 = var10 / posZ;
                        if (var1 >= var8 && var1 <= var9 && var11 >= 0 && var10 >= 0 && var11 < var4)
                        {
                            bool var12 = var11 == selectedElement && java.lang.System.currentTimeMillis() - lastClicked < 250L;
                            elementClicked(var11, var12);
                            selectedElement = var11;
                            lastClicked = java.lang.System.currentTimeMillis();
                        }
                        else if (var1 >= var8 && var1 <= var9 && var10 < 0)
                        {
                            func_27255_a(var1 - var8, var2 - top + (int)amountScrolled - 4);
                            var7 = false;
                        }

                        if (var1 >= var5 && var1 <= var6)
                        {
                            scrollMultiplier = -1.0F;
                            var19 = getContentHeight() - (bottom - top - 4);
                            if (var19 < 1)
                            {
                                var19 = 1;
                            }

                            var13 = (int)((float)((bottom - top) * (bottom - top)) / (float)getContentHeight());
                            if (var13 < 32)
                            {
                                var13 = 32;
                            }

                            if (var13 > bottom - top - 8)
                            {
                                var13 = bottom - top - 8;
                            }

                            scrollMultiplier /= (float)(bottom - top - var13) / (float)var19;
                        }
                        else
                        {
                            scrollMultiplier = 1.0F;
                        }

                        if (var7)
                        {
                            initialClickY = (float)var2;
                        }
                        else
                        {
                            initialClickY = -2.0F;
                        }
                    }
                    else
                    {
                        initialClickY = -2.0F;
                    }
                }
                else if (initialClickY >= 0.0F)
                {
                    amountScrolled -= ((float)var2 - initialClickY) * scrollMultiplier;
                    initialClickY = (float)var2;
                }
            }
            else
            {
                initialClickY = -1.0F;
            }

            bindAmountScrolled();
            GLManager.GL.Disable(GLEnum.Lighting);
            GLManager.GL.Disable(GLEnum.Fog);
            Tessellator var16 = Tessellator.instance;
            GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)mc.renderEngine.getTexture("/gui/background.png"));
            GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            float var17 = 32.0F;
            var16.startDrawingQuads();
            var16.setColorOpaque_I(2105376);
            var16.addVertexWithUV((double)left, (double)bottom, 0.0D, (double)((float)left / var17), (double)((float)(bottom + (int)amountScrolled) / var17));
            var16.addVertexWithUV((double)right, (double)bottom, 0.0D, (double)((float)right / var17), (double)((float)(bottom + (int)amountScrolled) / var17));
            var16.addVertexWithUV((double)right, (double)top, 0.0D, (double)((float)right / var17), (double)((float)(top + (int)amountScrolled) / var17));
            var16.addVertexWithUV((double)left, (double)top, 0.0D, (double)((float)left / var17), (double)((float)(top + (int)amountScrolled) / var17));
            var16.draw();
            var9 = width / 2 - 92 - 16;
            var10 = top + 4 - (int)amountScrolled;
            if (field_27262_q)
            {
                func_27260_a(var9, var10, var16);
            }

            int var14;
            for (var11 = 0; var11 < var4; ++var11)
            {
                var19 = var10 + var11 * posZ + field_27261_r;
                var13 = posZ - 4;
                if (var19 <= bottom && var19 + var13 >= top)
                {
                    if (field_25123_p && isSelected(var11))
                    {
                        var14 = width / 2 - 110;
                        int var15 = width / 2 + 110;
                        GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
                        GLManager.GL.Disable(GLEnum.Texture2D);
                        var16.startDrawingQuads();
                        var16.setColorOpaque_I(8421504);
                        var16.addVertexWithUV((double)var14, (double)(var19 + var13 + 2), 0.0D, 0.0D, 1.0D);
                        var16.addVertexWithUV((double)var15, (double)(var19 + var13 + 2), 0.0D, 1.0D, 1.0D);
                        var16.addVertexWithUV((double)var15, (double)(var19 - 2), 0.0D, 1.0D, 0.0D);
                        var16.addVertexWithUV((double)var14, (double)(var19 - 2), 0.0D, 0.0D, 0.0D);
                        var16.setColorOpaque_I(0);
                        var16.addVertexWithUV((double)(var14 + 1), (double)(var19 + var13 + 1), 0.0D, 0.0D, 1.0D);
                        var16.addVertexWithUV((double)(var15 - 1), (double)(var19 + var13 + 1), 0.0D, 1.0D, 1.0D);
                        var16.addVertexWithUV((double)(var15 - 1), (double)(var19 - 1), 0.0D, 1.0D, 0.0D);
                        var16.addVertexWithUV((double)(var14 + 1), (double)(var19 - 1), 0.0D, 0.0D, 0.0D);
                        var16.draw();
                        GLManager.GL.Enable(GLEnum.Texture2D);
                    }

                    drawSlot(var11, var9, var19, var13, var16);
                }
            }

            GLManager.GL.Disable(GLEnum.DepthTest);
            byte var18 = 4;
            overlayBackground(0, top, 255, 255);
            overlayBackground(bottom, height, 255, 255);
            GLManager.GL.Enable(GLEnum.Blend);
            GLManager.GL.BlendFunc(GLEnum.SrcAlpha, GLEnum.OneMinusSrcAlpha);
            GLManager.GL.Disable(GLEnum.AlphaTest);
            GLManager.GL.ShadeModel(GLEnum.Smooth);
            GLManager.GL.Disable(GLEnum.Texture2D);
            var16.startDrawingQuads();
            var16.setColorRGBA_I(0, 0);
            var16.addVertexWithUV((double)left, (double)(top + var18), 0.0D, 0.0D, 1.0D);
            var16.addVertexWithUV((double)right, (double)(top + var18), 0.0D, 1.0D, 1.0D);
            var16.setColorRGBA_I(0, 255);
            var16.addVertexWithUV((double)right, (double)top, 0.0D, 1.0D, 0.0D);
            var16.addVertexWithUV((double)left, (double)top, 0.0D, 0.0D, 0.0D);
            var16.draw();
            var16.startDrawingQuads();
            var16.setColorRGBA_I(0, 255);
            var16.addVertexWithUV((double)left, (double)bottom, 0.0D, 0.0D, 1.0D);
            var16.addVertexWithUV((double)right, (double)bottom, 0.0D, 1.0D, 1.0D);
            var16.setColorRGBA_I(0, 0);
            var16.addVertexWithUV((double)right, (double)(bottom - var18), 0.0D, 1.0D, 0.0D);
            var16.addVertexWithUV((double)left, (double)(bottom - var18), 0.0D, 0.0D, 0.0D);
            var16.draw();
            var19 = getContentHeight() - (bottom - top - 4);
            if (var19 > 0)
            {
                var13 = (bottom - top) * (bottom - top) / getContentHeight();
                if (var13 < 32)
                {
                    var13 = 32;
                }

                if (var13 > bottom - top - 8)
                {
                    var13 = bottom - top - 8;
                }

                var14 = (int)amountScrolled * (bottom - top - var13) / var19 + top;
                if (var14 < top)
                {
                    var14 = top;
                }

                var16.startDrawingQuads();
                var16.setColorRGBA_I(0, 255);
                var16.addVertexWithUV((double)var5, (double)bottom, 0.0D, 0.0D, 1.0D);
                var16.addVertexWithUV((double)var6, (double)bottom, 0.0D, 1.0D, 1.0D);
                var16.addVertexWithUV((double)var6, (double)top, 0.0D, 1.0D, 0.0D);
                var16.addVertexWithUV((double)var5, (double)top, 0.0D, 0.0D, 0.0D);
                var16.draw();
                var16.startDrawingQuads();
                var16.setColorRGBA_I(8421504, 255);
                var16.addVertexWithUV((double)var5, (double)(var14 + var13), 0.0D, 0.0D, 1.0D);
                var16.addVertexWithUV((double)var6, (double)(var14 + var13), 0.0D, 1.0D, 1.0D);
                var16.addVertexWithUV((double)var6, (double)var14, 0.0D, 1.0D, 0.0D);
                var16.addVertexWithUV((double)var5, (double)var14, 0.0D, 0.0D, 0.0D);
                var16.draw();
                var16.startDrawingQuads();
                var16.setColorRGBA_I(12632256, 255);
                var16.addVertexWithUV((double)var5, (double)(var14 + var13 - 1), 0.0D, 0.0D, 1.0D);
                var16.addVertexWithUV((double)(var6 - 1), (double)(var14 + var13 - 1), 0.0D, 1.0D, 1.0D);
                var16.addVertexWithUV((double)(var6 - 1), (double)var14, 0.0D, 1.0D, 0.0D);
                var16.addVertexWithUV((double)var5, (double)var14, 0.0D, 0.0D, 0.0D);
                var16.draw();
            }

            func_27257_b(var1, var2);
            GLManager.GL.Enable(GLEnum.Texture2D);
            GLManager.GL.ShadeModel(GLEnum.Flat);
            GLManager.GL.Enable(GLEnum.AlphaTest);
            GLManager.GL.Disable(GLEnum.Blend);
        }

        private void overlayBackground(int var1, int var2, int var3, int var4)
        {
            Tessellator var5 = Tessellator.instance;
            GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)mc.renderEngine.getTexture("/gui/background.png"));
            GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            float var6 = 32.0F;
            var5.startDrawingQuads();
            var5.setColorRGBA_I(4210752, var4);
            var5.addVertexWithUV(0.0D, (double)var2, 0.0D, 0.0D, (double)((float)var2 / var6));
            var5.addVertexWithUV((double)width, (double)var2, 0.0D, (double)((float)width / var6), (double)((float)var2 / var6));
            var5.setColorRGBA_I(4210752, var3);
            var5.addVertexWithUV((double)width, (double)var1, 0.0D, (double)((float)width / var6), (double)((float)var1 / var6));
            var5.addVertexWithUV(0.0D, (double)var1, 0.0D, 0.0D, (double)((float)var1 / var6));
            var5.draw();
        }
    }

}