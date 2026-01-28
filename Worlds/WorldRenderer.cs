using betareborn.Blocks;
using betareborn.Chunks;
using betareborn.Entities;
using betareborn.Rendering;
using Silk.NET.OpenGL.Legacy;
using System.Diagnostics;

namespace betareborn.Worlds
{
    public class WorldRenderer
    {
        public World worldObj;
        private readonly int glRenderList = -1;
        private static Tessellator tessellator = Tessellator.instance;
        public static int chunksUpdated = 0;
        public int posX;
        public int posY;
        public int posZ;
        public int sizeWidth;
        public int sizeHeight;
        public int sizeDepth;
        public int posXMinus;
        public int posYMinus;
        public int posZMinus;
        public int posXClip;
        public int posYClip;
        public int posZClip;
        public bool isInFrustum = false;
        public bool[] skipRenderPass = new bool[2];
        public int posXPlus;
        public int posYPlus;
        public int posZPlus;
        public float rendererRadius;
        public bool needsUpdate;
        public AxisAlignedBB rendererBoundingBox;
        public int chunkIndex;
        public bool isVisible = true;
        public bool isWaitingOnOcclusionQuery;
        public int glOcclusionQuery;
        public bool isChunkLit;
        private bool isInitialized = false;

        public WorldRenderer(World var1, int var3, int var4, int var5, int var6, int var7)
        {
            worldObj = var1;
            sizeWidth = sizeHeight = sizeDepth = var6;
            rendererRadius = MathHelper.sqrt_float((float)(sizeWidth * sizeWidth + sizeHeight * sizeHeight + sizeDepth * sizeDepth)) / 2.0F;
            glRenderList = var7;
            posX = -999;
            setPosition(var3, var4, var5);
            needsUpdate = false;
        }

        public void setPosition(int var1, int var2, int var3)
        {
            if (var1 != posX || var2 != posY || var3 != posZ)
            {
                setDontDraw();
                posX = var1;
                posY = var2;
                posZ = var3;
                posXPlus = var1 + sizeWidth / 2;
                posYPlus = var2 + sizeHeight / 2;
                posZPlus = var3 + sizeDepth / 2;
                posXClip = var1 & 1023;
                posYClip = var2;
                posZClip = var3 & 1023;
                posXMinus = var1 - posXClip;
                posYMinus = var2 - posYClip;
                posZMinus = var3 - posZClip;
                float var4 = 6.0F;
                rendererBoundingBox = AxisAlignedBB.getBoundingBox((double)((float)var1 - var4), (double)((float)var2 - var4), (double)((float)var3 - var4), (double)((float)(var1 + sizeWidth) + var4), (double)((float)(var2 + sizeHeight) + var4), (double)((float)(var3 + sizeDepth) + var4));
                markDirty();
            }
        }

        private void setupGLTranslation()
        {
            GLManager.GL.Translate((float)posXClip, (float)posYClip, (float)posZClip);
        }

        public bool updateRenderer()
        {
            if (needsUpdate)
            {
                Stopwatch sw = Stopwatch.StartNew();
                ++chunksUpdated;
                int var1 = posX;
                int var2 = posY;
                int var3 = posZ;
                int var4 = posX + sizeWidth;
                int var5 = posY + sizeHeight;
                int var6 = posZ + sizeDepth;
                byte var8 = 1;

                if (!worldObj.checkChunksExist(var1 - var8, var2 - var8, var3 - var8,
                                       var4 + var8, var5 + var8, var6 + var8))
                {
                    return false;
                }

                for (int var7 = 0; var7 < 2; ++var7)
                {
                    skipRenderPass[var7] = true;
                }

                Chunk.isLit = false;
                Stopwatch sw2 = Stopwatch.StartNew();
                ChunkCache var9 = new(worldObj, var1 - var8, var2 - var8, var3 - var8, var4 + var8, var5 + var8, var6 + var8);
                sw2.Stop();
                if (sw2.Elapsed.TotalMilliseconds > 1.0)
                {
                    Console.WriteLine($"sw2 ms: {sw2.Elapsed.TotalMilliseconds:F4}");
                }
                RenderBlocks var10 = new(var9);

                for (int var11 = 0; var11 < 2; ++var11)
                {
                    bool var12 = false;
                    bool var13 = false;
                    bool var14 = false;

                    for (int var15 = var2; var15 < var5; ++var15)
                    {
                        for (int var16 = var3; var16 < var6; ++var16)
                        {
                            for (int var17 = var1; var17 < var4; ++var17)
                            {
                                int var18 = var9.getBlockId(var17, var15, var16);
                                if (var18 > 0)
                                {
                                    if (!var14)
                                    {
                                        var14 = true;
                                        GLManager.GL.NewList((uint)(glRenderList + var11), GLEnum.Compile);
                                        GLManager.GL.PushMatrix();
                                        setupGLTranslation();
                                        float var19 = 1.000001F;
                                        GLManager.GL.Translate((float)(-sizeDepth) / 2.0F, (float)(-sizeHeight) / 2.0F, (float)(-sizeDepth) / 2.0F);
                                        GLManager.GL.Scale(var19, var19, var19);
                                        GLManager.GL.Translate((float)sizeDepth / 2.0F, (float)sizeHeight / 2.0F, (float)sizeDepth / 2.0F);
                                        tessellator.startDrawingQuads();
                                        tessellator.setTranslationD((double)(-posX), (double)(-posY), (double)(-posZ));
                                    }

                                    Block var24 = Block.blocksList[var18];
                                    int var20 = var24.getRenderBlockPass();
                                    if (var20 != var11)
                                    {
                                        var12 = true;
                                    }
                                    else if (var20 == var11)
                                    {
                                        var13 |= var10.renderBlockByRenderType(var24, var17, var15, var16);
                                    }
                                }
                            }
                        }
                    }

                    if (var14)
                    {
                        tessellator.draw();
                        GLManager.GL.PopMatrix();
                        GLManager.GL.EndList();
                        tessellator.setTranslationD(0.0D, 0.0D, 0.0D);
                    }
                    else
                    {
                        var13 = false;
                    }

                    if (var13)
                    {
                        skipRenderPass[var11] = false;
                    }

                    if (!var12)
                    {
                        break;
                    }
                }

                isChunkLit = Chunk.isLit;
                isInitialized = true;

                sw.Stop();
                long ticks = sw.ElapsedTicks;
                if (sw2.Elapsed.TotalMilliseconds > 6.0)
                {
                    Console.WriteLine($"sw ms: {sw.Elapsed.TotalMilliseconds:F4}");
                }
            }
            
            return true;
        }

        public float distanceToEntitySquared(Entity var1)
        {
            float var2 = (float)(var1.posX - (double)posXPlus);
            float var3 = (float)(var1.posY - (double)posYPlus);
            float var4 = (float)(var1.posZ - (double)posZPlus);
            return var2 * var2 + var3 * var3 + var4 * var4;
        }

        public void setDontDraw()
        {
            for (int var1 = 0; var1 < 2; ++var1)
            {
                skipRenderPass[var1] = true;
            }

            isInFrustum = false;
            isInitialized = false;
        }

        public void func_1204_c()
        {
            setDontDraw();
            worldObj = null;
        }

        public int getGLCallListForPass(int var1)
        {
            return !isInFrustum ? -1 : (!skipRenderPass[var1] ? glRenderList + var1 : -1);
        }

        public void updateInFrustrum(ICamera var1)
        {
            isInFrustum = var1.isBoundingBoxInFrustum(rendererBoundingBox);
        }

        public void callOcclusionQueryList()
        {
            GLManager.GL.CallList((uint)(glRenderList + 2));
        }

        public bool skipAllRenderPasses()
        {
            return !isInitialized ? false : skipRenderPass[0] && skipRenderPass[1];
        }

        public void markDirty()
        {
            needsUpdate = true;
        }
    }

}