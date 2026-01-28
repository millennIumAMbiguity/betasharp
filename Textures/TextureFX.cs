using betareborn.Rendering;
using Silk.NET.OpenGL.Legacy;

namespace betareborn.Textures
{
    public class TextureFX : java.lang.Object
    {
        public byte[] imageData = new byte[1024];
        public int iconIndex;
        public bool anaglyphEnabled = false;
        public int textureId = 0;
        public int tileSize = 1;
        public int tileImage = 0;

        public TextureFX(int var1)
        {
            iconIndex = var1;
        }

        public virtual void onTick()
        {
        }

        public void bindImage(RenderEngine var1)
        {
            if (tileImage == 0)
            {
                GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var1.getTexture("/terrain.png"));
            }
            else if (tileImage == 1)
            {
                GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var1.getTexture("/gui/items.png"));

            }

        }
    }
}