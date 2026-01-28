using betareborn.Textures;
using java.awt;
using java.awt.image;
using java.io;
using java.lang;
using java.nio;
using java.util;
using javax.imageio;
using Silk.NET.OpenGL.Legacy;

namespace betareborn.Rendering
{
    public class RenderEngine : java.lang.Object
    {
        public static bool useMipmaps = false;
        private readonly HashMap textureMap = [];
        private readonly HashMap field_28151_c = [];
        private readonly HashMap textureNameToImageMap = [];
        private readonly IntBuffer singleIntBuffer = GLAllocation.createDirectIntBuffer(1);
        private readonly ByteBuffer imageData = GLAllocation.createDirectByteBuffer(1048576);
        private readonly java.util.List textureList = new ArrayList();
        private readonly Map urlToImageDataMap = new HashMap();
        private readonly GameSettings options;
        private bool clampTexture = false;
        private bool blurTexture = false;
        private readonly TexturePackList texturePack;
        private readonly BufferedImage missingTextureImage = new(64, 64, 2);

        public RenderEngine(TexturePackList var1, GameSettings var2)
        {
            texturePack = var1;
            options = var2;
            Graphics var3 = missingTextureImage.getGraphics();
            var3.setColor(Color.WHITE);
            var3.fillRect(0, 0, 64, 64);
            var3.setColor(Color.BLACK);
            var3.drawString("missingtex", 1, 10);
            var3.dispose();
        }

        public int[] func_28149_a(string var1)
        {
            TexturePackBase var2 = texturePack.selectedTexturePack;
            int[] var3 = (int[])field_28151_c.get(var1);
            if (var3 != null)
            {
                return var3;
            }
            else
            {
                try
                {
                    object var6 = null;
                    if (var1.StartsWith("##"))
                    {
                        var3 = func_28148_b(unwrapImageByColumns(readTextureImage(var2.getResourceAsStream(var1[2..]))));
                    }
                    else if (var1.StartsWith("%clamp%"))
                    {
                        clampTexture = true;
                        var3 = func_28148_b(readTextureImage(var2.getResourceAsStream(var1[7..])));
                        clampTexture = false;
                    }
                    else if (var1.StartsWith("%blur%"))
                    {
                        blurTexture = true;
                        var3 = func_28148_b(readTextureImage(var2.getResourceAsStream(var1[6..])));
                        blurTexture = false;
                    }
                    else
                    {
                        InputStream var7 = var2.getResourceAsStream(var1);
                        if (var7 == null)
                        {
                            var3 = func_28148_b(missingTextureImage);
                        }
                        else
                        {
                            var3 = func_28148_b(readTextureImage(var7));
                        }
                    }

                    field_28151_c.put(var1, var3);
                    return var3;
                }
                catch (java.io.IOException var5)
                {
                    var5.printStackTrace();
                    int[] var4 = func_28148_b(missingTextureImage);
                    field_28151_c.put(var1, var4);
                    return var4;
                }
            }
        }

        private int[] func_28148_b(BufferedImage var1)
        {
            int var2 = var1.getWidth();
            int var3 = var1.getHeight();
            int[] var4 = new int[var2 * var3];
            var1.getRGB(0, 0, var2, var3, var4, 0, var2);
            return var4;
        }

        private int[] func_28147_a(BufferedImage var1, int[] var2)
        {
            int var3 = var1.getWidth();
            int var4 = var1.getHeight();
            var1.getRGB(0, 0, var3, var4, var2, 0, var3);
            return var2;
        }

        public int getTexture(string var1)
        {
            TexturePackBase var2 = texturePack.selectedTexturePack;
            Integer var3 = (Integer)textureMap.get(var1);
            if (var3 != null)
            {
                return var3.intValue();
            }
            else
            {
                try
                {
                    singleIntBuffer.clear();
                    GLAllocation.generateTextureNames(singleIntBuffer);
                    int var6 = singleIntBuffer.get(0);
                    if (var1.StartsWith("##"))
                    {
                        setupTexture(unwrapImageByColumns(readTextureImage(var2.getResourceAsStream(var1[2..]))), var6);
                    }
                    else if (var1.StartsWith("%clamp%"))
                    {
                        clampTexture = true;
                        setupTexture(readTextureImage(var2.getResourceAsStream(var1[7..])), var6);
                        clampTexture = false;
                    }
                    else if (var1.StartsWith("%blur%"))
                    {
                        blurTexture = true;
                        setupTexture(readTextureImage(var2.getResourceAsStream(var1[6..])), var6);
                        blurTexture = false;
                    }
                    else
                    {
                        InputStream var7 = var2.getResourceAsStream(var1);
                        if (var7 == null)
                        {
                            setupTexture(missingTextureImage, var6);
                        }
                        else
                        {
                            setupTexture(readTextureImage(var7), var6);
                        }
                    }

                    textureMap.put(var1, Integer.valueOf(var6));
                    return var6;
                }
                catch (java.io.IOException var5)
                {
                    var5.printStackTrace();
                    GLAllocation.generateTextureNames(singleIntBuffer);
                    int var4 = singleIntBuffer.get(0);
                    setupTexture(missingTextureImage, var4);
                    textureMap.put(var1, Integer.valueOf(var4));
                    return var4;
                }
            }
        }

        private BufferedImage unwrapImageByColumns(BufferedImage var1)
        {
            int var2 = var1.getWidth() / 16;
            BufferedImage var3 = new(16, var1.getHeight() * var2, 2);
            Graphics var4 = var3.getGraphics();

            for (int var5 = 0; var5 < var2; ++var5)
            {
                var4.drawImage(var1, -var5 * 16, var5 * var1.getHeight(), (ImageObserver)null);
            }

            var4.dispose();
            return var3;
        }

        public int allocateAndSetupTexture(BufferedImage var1)
        {
            singleIntBuffer.clear();
            GLAllocation.generateTextureNames(singleIntBuffer);
            int var2 = singleIntBuffer.get(0);
            setupTexture(var1, var2);
            textureNameToImageMap.put(Integer.valueOf(var2), var1);
            return var2;
        }

        public unsafe void setupTexture(BufferedImage var1, int var2)
        {
            GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var2);

            if (useMipmaps)
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.NearestMipmapLinear);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);
            }
            else
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Nearest);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);
            }

            if (blurTexture)
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Linear);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Linear);
            }

            if (clampTexture)
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GLEnum.Clamp);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GLEnum.Clamp);
            }
            else
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GLEnum.Repeat);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GLEnum.Repeat);
            }

            int var3 = var1.getWidth();
            int var4 = var1.getHeight();
            int[] var5 = new int[var3 * var4];
            byte[] var6 = new byte[var3 * var4 * 4];
            var1.getRGB(0, 0, var3, var4, var5, 0, var3);

            int var7;
            int var8;
            int var9;
            int var10;
            int var11;
            int var12;
            int var13;
            int var14;
            for (var7 = 0; var7 < var5.Length; ++var7)
            {
                var8 = var5[var7] >> 24 & 255;
                var9 = var5[var7] >> 16 & 255;
                var10 = var5[var7] >> 8 & 255;
                var11 = var5[var7] & 255;

                var6[var7 * 4 + 0] = (byte)var9;
                var6[var7 * 4 + 1] = (byte)var10;
                var6[var7 * 4 + 2] = (byte)var11;
                var6[var7 * 4 + 3] = (byte)var8;
            }

            imageData.clear();
            imageData.put(var6);
            imageData.position(0).limit(var6.Length);

            BufferHelper.UsePointer(imageData, (p) =>
            {
                var ptr = (byte*)p;
                GLManager.GL.TexImage2D(GLEnum.Texture2D, 0, (int)GLEnum.Rgba, (uint)var3, (uint)var4, 0, GLEnum.Rgba, GLEnum.UnsignedByte, ptr);

                if (useMipmaps)
                {
                    for (var7 = 1; var7 <= 4; ++var7)
                    {
                        var8 = var3 >> var7 - 1;
                        var9 = var3 >> var7;
                        var10 = var4 >> var7;

                        for (var11 = 0; var11 < var9; ++var11)
                        {
                            for (var12 = 0; var12 < var10; ++var12)
                            {
                                var13 = imageData.getInt((var11 * 2 + 0 + (var12 * 2 + 0) * var8) * 4);
                                var14 = imageData.getInt((var11 * 2 + 1 + (var12 * 2 + 0) * var8) * 4);
                                int var15 = imageData.getInt((var11 * 2 + 1 + (var12 * 2 + 1) * var8) * 4);
                                int var16 = imageData.getInt((var11 * 2 + 0 + (var12 * 2 + 1) * var8) * 4);
                                int var17 = weightedAverageColor(weightedAverageColor(var13, var14), weightedAverageColor(var15, var16));
                                imageData.putInt((var11 + var12 * var9) * 4, var17);
                            }
                        }

                        GLManager.GL.TexImage2D(GLEnum.Texture2D, var7, (int)GLEnum.Rgba, (uint)var9, (uint)var10, 0, GLEnum.Rgba, GLEnum.UnsignedByte, ptr);
                    }
                }
            });
        }

        public unsafe void func_28150_a(int[] var1, int var2, int var3, int var4)
        {
            //TODO: this is probably wrong and will crash

            GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var4);
            if (useMipmaps)
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.NearestMipmapLinear);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);
            }
            else
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Nearest);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);
            }
            if (blurTexture)
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Linear);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Linear);
            }
            if (clampTexture)
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GLEnum.Clamp);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GLEnum.Clamp);
            }
            else
            {
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GLEnum.Repeat);
                GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GLEnum.Repeat);
            }

            byte[] var5 = new byte[var2 * var3 * 4];

            for (int var6 = 0; var6 < var1.Length; ++var6)
            {
                int var7 = var1[var6] >> 24 & 255;
                int var8 = var1[var6] >> 16 & 255;
                int var9 = var1[var6] >> 8 & 255;
                int var10 = var1[var6] & 255;

                var5[var6 * 4 + 0] = (byte)var8;
                var5[var6 * 4 + 1] = (byte)var9;
                var5[var6 * 4 + 2] = (byte)var10;
                var5[var6 * 4 + 3] = (byte)var7;
            }

            imageData.clear();
            imageData.put(var5);
            imageData.position(0).limit(var5.Length);

            byte[] imageArray = imageData.array();
            int offset = imageData.arrayOffset();

            fixed (byte* ptr = imageArray)
            {
                GLManager.GL.TexSubImage2D(GLEnum.Texture2D, 0, 0, 0, (uint)var2, (uint)var3, GLEnum.Rgba, GLEnum.UnsignedByte, ptr + offset);
            }
        }

        public void deleteTexture(int var1)
        {
            textureNameToImageMap.remove(Integer.valueOf(var1));
            singleIntBuffer.clear();
            singleIntBuffer.put(var1);
            singleIntBuffer.flip();
            //GL11.glDeleteTextures(singleIntBuffer);
            GLManager.GL.DeleteTexture((uint)var1);
        }

        //public int getTextureForDownloadableImage(string var1, string var2)
        //{
        //    ThreadDownloadImageData var3 = (ThreadDownloadImageData)urlToImageDataMap.get(var1);
        //    if (var3 != null && var3.image != null && !var3.textureSetupComplete)
        //    {
        //        if (var3.textureName < 0)
        //        {
        //            var3.textureName = allocateAndSetupTexture(var3.image);
        //        }
        //        else
        //        {
        //            setupTexture(var3.image, var3.textureName);
        //        }

        //        var3.textureSetupComplete = true;
        //    }

        //    return var3 != null && var3.textureName >= 0 ? var3.textureName : (var2 == null ? -1 : getTexture(var2));
        //}

        //public ThreadDownloadImageData obtainImageData(string var1, ImageBuffer var2)
        //{
        //    ThreadDownloadImageData var3 = (ThreadDownloadImageData)urlToImageDataMap.get(var1);
        //    if (var3 == null)
        //    {
        //        urlToImageDataMap.put(var1, new ThreadDownloadImageData(var1, var2));
        //    }
        //    else
        //    {
        //        ++var3.referenceCount;
        //    }

        //    return var3;
        //}

        //public void releaseImageData(string var1)
        //{
        //    ThreadDownloadImageData var2 = (ThreadDownloadImageData)urlToImageDataMap.get(var1);
        //    if (var2 != null)
        //    {
        //        --var2.referenceCount;
        //        if (var2.referenceCount == 0)
        //        {
        //            if (var2.textureName >= 0)
        //            {
        //                deleteTexture(var2.textureName);
        //            }

        //            urlToImageDataMap.remove(var1);
        //        }
        //    }

        //}

        public void registerTextureFX(TextureFX var1)
        {
            textureList.add(var1);
            var1.onTick();
        }

        public unsafe void updateDynamicTextures()
        {
            int var1;
            TextureFX var2;
            int var3;
            int var4;
            int var5;
            int var6;
            int var7;
            int var8;
            int var9;
            int var10;
            int var11;
            int var12;

            for (var1 = 0; var1 < textureList.size(); ++var1)
            {
                var2 = (TextureFX)textureList.get(var1);
                var2.onTick();
                imageData.clear();
                imageData.put(var2.imageData);
                imageData.position(0).limit(var2.imageData.Length);
                var2.bindImage(this);

                BufferHelper.UsePointer(imageData, (p =>
                {
                    var ptr = (byte*)p;

                    for (var3 = 0; var3 < var2.tileSize; ++var3)
                    {
                        for (var4 = 0; var4 < var2.tileSize; ++var4)
                        {
                            GLManager.GL.TexSubImage2D(GLEnum.Texture2D, 0, var2.iconIndex % 16 * 16 + var3 * 16, var2.iconIndex / 16 * 16 + var4 * 16, 16, 16, GLEnum.Rgba, GLEnum.UnsignedByte, ptr);

                            if (useMipmaps)
                            {
                                for (var5 = 1; var5 <= 4; ++var5)
                                {
                                    var6 = 16 >> var5 - 1;
                                    var7 = 16 >> var5;
                                    for (var8 = 0; var8 < var7; ++var8)
                                    {
                                        for (var9 = 0; var9 < var7; ++var9)
                                        {
                                            var10 = imageData.getInt((var8 * 2 + 0 + (var9 * 2 + 0) * var6) * 4);
                                            var11 = imageData.getInt((var8 * 2 + 1 + (var9 * 2 + 0) * var6) * 4);
                                            var12 = imageData.getInt((var8 * 2 + 1 + (var9 * 2 + 1) * var6) * 4);
                                            int var13 = imageData.getInt((var8 * 2 + 0 + (var9 * 2 + 1) * var6) * 4);
                                            int var14 = averageColor(averageColor(var10, var11), averageColor(var12, var13));
                                            imageData.putInt((var8 + var9 * var7) * 4, var14);
                                        }
                                    }
                                    GLManager.GL.TexSubImage2D(GLEnum.Texture2D, var5, var2.iconIndex % 16 * var7, var2.iconIndex / 16 * var7, (uint)var7, (uint)var7, GLEnum.Rgba, GLEnum.UnsignedByte, ptr);
                                }
                            }
                        }
                    }
                }));
            }

            for (var1 = 0; var1 < textureList.size(); ++var1)
            {
                var2 = (TextureFX)textureList.get(var1);
                if (var2.textureId > 0)
                {
                    imageData.clear();
                    imageData.put(var2.imageData);
                    imageData.position(0).limit(var2.imageData.Length);
                    GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var2.textureId);

                    BufferHelper.UsePointer(imageData, (p =>
                    {
                        var ptr = (byte*)p;

                        GLManager.GL.TexSubImage2D(GLEnum.Texture2D, 0, 0, 0, 16, 16, GLEnum.Rgba, GLEnum.UnsignedByte, ptr);

                        if (useMipmaps)
                        {
                            for (var3 = 1; var3 <= 4; ++var3)
                            {
                                var4 = 16 >> var3 - 1;
                                var5 = 16 >> var3;
                                for (var6 = 0; var6 < var5; ++var6)
                                {
                                    for (var7 = 0; var7 < var5; ++var7)
                                    {
                                        var8 = imageData.getInt((var6 * 2 + 0 + (var7 * 2 + 0) * var4) * 4);
                                        var9 = imageData.getInt((var6 * 2 + 1 + (var7 * 2 + 0) * var4) * 4);
                                        var10 = imageData.getInt((var6 * 2 + 1 + (var7 * 2 + 1) * var4) * 4);
                                        var11 = imageData.getInt((var6 * 2 + 0 + (var7 * 2 + 1) * var4) * 4);
                                        var12 = averageColor(averageColor(var8, var9), averageColor(var10, var11));
                                        imageData.putInt((var6 + var7 * var5) * 4, var12);
                                    }
                                }
                                GLManager.GL.TexSubImage2D(GLEnum.Texture2D, var3, 0, 0, (uint)var5, (uint)var5, GLEnum.Rgba, GLEnum.UnsignedByte, ptr);
                            }
                        }
                    }));
                }
            }
        }

        private int averageColor(int var1, int var2)
        {
            int var3 = (var1 & -16777216) >> 24 & 255;
            int var4 = (var2 & -16777216) >> 24 & 255;
            return (var3 + var4 >> 1 << 24) + ((var1 & 16711422) + (var2 & 16711422) >> 1);
        }

        private int weightedAverageColor(int var1, int var2)
        {
            int var3 = (var1 & -16777216) >> 24 & 255;
            int var4 = (var2 & -16777216) >> 24 & 255;
            short var5 = 255;
            if (var3 + var4 == 0)
            {
                var3 = 1;
                var4 = 1;
                var5 = 0;
            }

            int var6 = (var1 >> 16 & 255) * var3;
            int var7 = (var1 >> 8 & 255) * var3;
            int var8 = (var1 & 255) * var3;
            int var9 = (var2 >> 16 & 255) * var4;
            int var10 = (var2 >> 8 & 255) * var4;
            int var11 = (var2 & 255) * var4;
            int var12 = (var6 + var9) / (var3 + var4);
            int var13 = (var7 + var10) / (var3 + var4);
            int var14 = (var8 + var11) / (var3 + var4);
            return var5 << 24 | var12 << 16 | var13 << 8 | var14;
        }

        public void refreshTextures()
        {
            TexturePackBase var1 = texturePack.selectedTexturePack;
            Iterator var2 = textureNameToImageMap.keySet().iterator();

            BufferedImage var4;
            while (var2.hasNext())
            {
                int var3 = ((Integer)var2.next()).intValue();
                var4 = (BufferedImage)textureNameToImageMap.get(Integer.valueOf(var3));
                setupTexture(var4, var3);
            }

            //ThreadDownloadImageData var8;
            //for (var2 = urlToImageDataMap.values().iterator(); var2.hasNext(); var8.textureSetupComplete = false)
            //{
            //    var8 = (ThreadDownloadImageData)var2.next();
            //}

            var2 = textureMap.keySet().iterator();

            string var9;
            while (var2.hasNext())
            {
                var9 = (string)var2.next();

                try
                {
                    if (var9.StartsWith("##"))
                    {
                        var4 = unwrapImageByColumns(readTextureImage(var1.getResourceAsStream(var9[2..])));
                    }
                    else if (var9.StartsWith("%clamp%"))
                    {
                        clampTexture = true;
                        var4 = readTextureImage(var1.getResourceAsStream(var9[7..]));
                    }
                    else if (var9.StartsWith("%blur%"))
                    {
                        blurTexture = true;
                        var4 = readTextureImage(var1.getResourceAsStream(var9[6..]));
                    }
                    else
                    {
                        var4 = readTextureImage(var1.getResourceAsStream(var9));
                    }

                    int var5 = ((Integer)textureMap.get(var9)).intValue();
                    setupTexture(var4, var5);
                    blurTexture = false;
                    clampTexture = false;
                }
                catch (java.io.IOException var7)
                {
                    var7.printStackTrace();
                }
            }

            var2 = field_28151_c.keySet().iterator();

            while (var2.hasNext())
            {
                var9 = (string)var2.next();

                try
                {
                    if (var9.StartsWith("##"))
                    {
                        var4 = unwrapImageByColumns(readTextureImage(var1.getResourceAsStream(var9[2..])));
                    }
                    else if (var9.StartsWith("%clamp%"))
                    {
                        clampTexture = true;
                        var4 = readTextureImage(var1.getResourceAsStream(var9[7..]));
                    }
                    else if (var9.StartsWith("%blur%"))
                    {
                        blurTexture = true;
                        var4 = readTextureImage(var1.getResourceAsStream(var9[6..]));
                    }
                    else
                    {
                        var4 = readTextureImage(var1.getResourceAsStream(var9));
                    }

                    func_28147_a(var4, (int[])field_28151_c.get(var9));
                    blurTexture = false;
                    clampTexture = false;
                }
                catch (java.io.IOException var6)
                {
                    var6.printStackTrace();
                }
            }

        }

        private BufferedImage readTextureImage(InputStream var1)
        {
            BufferedImage var2 = ImageIO.read(var1);
            var1.close();
            return var2;
        }

        public void bindTexture(int var1)
        {
            if (var1 >= 0)
            {
                GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var1);
            }
        }
    }
}