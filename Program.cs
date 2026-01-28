//using betareborn.Models;
//using java.awt.image;
//using javax.imageio;
//using Silk.NET.GLFW;
//using Silk.NET.Maths;
//using Silk.NET.OpenGL.Legacy;

//namespace betareborn
//{
//    public class Program
//    {
//        // test zombie model rendering and other basic functionality

//        public static unsafe uint LoadTextureFromBufferedImage(BufferedImage image)
//        {
//            int width = image.getWidth();
//            int height = image.getHeight();

//            // Extract pixel data from BufferedImage
//            int[] pixels = new int[width * height];
//            image.getRGB(0, 0, width, height, pixels, 0, width);

//            // Convert ARGB to RGBA byte array
//            byte[] rgba = new byte[width * height * 4];
//            for (int i = 0; i < pixels.Length; i++)
//            {
//                int pixel = pixels[i];
//                rgba[i * 4 + 0] = (byte)((pixel >> 16) & 0xFF); // R
//                rgba[i * 4 + 1] = (byte)((pixel >> 8) & 0xFF);  // G
//                rgba[i * 4 + 2] = (byte)(pixel & 0xFF);         // B
//                rgba[i * 4 + 3] = (byte)((pixel >> 24) & 0xFF); // A
//            }

//            // Generate and bind texture
//            uint textureId = GLManager.GL.GenTexture();
//            GLManager.GL.BindTexture(GLEnum.Texture2D, textureId);

//            // Upload texture data
//            fixed (byte* ptr = rgba)
//            {
//                GLManager.GL.TexImage2D(
//                    GLEnum.Texture2D,
//                    0,                           // mipmap level
//                    (int)GLEnum.Rgba,           // internal format
//                    (uint)width,
//                    (uint)height,
//                    0,                           // border
//                    GLEnum.Rgba,                // format
//                    GLEnum.UnsignedByte,        // type
//                    ptr
//                );
//            }

//            // Set texture parameters
//            GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Nearest);
//            GLManager.GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);

//            return textureId;
//        }

//        public static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");
//            Console.WriteLine(StringTranslate.getInstance().translateKey("menu.quit"));
//            Console.WriteLine(ChatAllowedCharacters.allowedCharacters);
//            Console.WriteLine(AchievementMap.getGuid(1000));

//            unsafe
//            {
//                var glfw = Glfw.GetApi();

//                // Initialize GLFW
//                if (!glfw.Init())
//                {
//                    throw new Exception("Failed to initialize GLFW");
//                }

//                // Set window hints (OpenGL version, profile, etc.)
//                glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
//                glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
//                glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Compat);

//                // Create window
//                var window = glfw.CreateWindow(800, 600, "OpenGL with GLFW", null, null);
//                if (window == null)
//                {
//                    glfw.Terminate();
//                    throw new Exception("Failed to create GLFW window");
//                }

//                // Make the OpenGL context current
//                glfw.MakeContextCurrent(window);

//                // Initialize OpenGL
//                var contextSource = new GlfwContext(glfw, window);

//                // Initialize OpenGL
//                var gl = GL.GetApi(contextSource);
//                GLManager.Init(gl);
//                Minecraft mc = new();
//                mc.startGame();

//                Mouse.Create(glfw, window, 800, 600);
//                Keyboard.Create(glfw, window);

//                // Set viewport
//                glfw.GetFramebufferSize(window, out int width, out int height);
//                gl.Viewport(0, 0, (uint)width, (uint)height);

//                // Optional: set clear color
//                gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

//                gl.Enable(EnableCap.Texture2D);

//                gl.Enable(EnableCap.Blend);
//                gl.Enable(EnableCap.AlphaTest);
//                gl.AlphaFunc(AlphaFunction.Greater, 0.1f);

//                uint zombieTex = LoadTextureFromBufferedImage(ImageIO.read(new java.io.File("zombie.png")));


//                ModelZombie model = new();
//                float rot = 0.0f;
//                float tx = 0.0f;
//                float tz = 0.0f;

//                // Main loop
//                while (!glfw.WindowShouldClose(window))
//                {
//                    // Clear
//                    gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

//                    gl.Enable(EnableCap.DepthTest);
//                    // Your rendering code here

//                    gl.MatrixMode(MatrixMode.Projection);
//                    gl.LoadIdentity();
//                    float aspect = 800 / 600.0f;
//                    var proj = Matrix4X4.CreatePerspectiveFieldOfView((float)java.lang.Math.toRadians(70), aspect, 0.1f, 100f);
//                    unsafe
//                    {
//                        float* ptr = (float*)&proj;
//                        gl.LoadMatrix(ptr);
//                    }

//                    gl.MatrixMode(MatrixMode.Modelview);
//                    gl.LoadIdentity();
//                    var view = Matrix4X4.CreateLookAt(new Vector3D<float>(0.0f, 1.5f, 5.0f), new Vector3D<float>(0.0f, 1.0f, 0.0f), new Vector3D<float>(0.0f, 1.0f, 0.0f));
//                    unsafe
//                    {
//                        float* ptr = (float*)&view;
//                        gl.LoadMatrix(ptr);
//                    }

//                    gl.PushMatrix();
//                    gl.Translate(0.0f, 1.0f, 0.0f);
//                    gl.Scale(1.0f, -1.0f, -1.0f);
//                    gl.Rotate(rot, 0.0f, 1.0f, 0.0f);
//                    gl.Color3(1.0f, 1.0f, 1.0f);
//                    gl.BindTexture(GLEnum.Texture2D, zombieTex);

//                    while (Mouse.next())
//                    {
//                        float sx = Mouse.getEventX() / 800.0f;
//                        float sy = Mouse.getEventY() / 600.0f;

//                        sx = Math.Clamp(sx, 0.0f, 1.0f);
//                        sy = Math.Clamp(sy, 0.0f, 1.0f);
//                        sx -= 0.5f;
//                        sy -= 0.5f;
//                        sx *= 4.0f;
//                        sy *= 4.0f;
//                        tx = sx;
//                        tz = sy;
//                    }

//                    while (Keyboard.next())
//                    {
//                        Console.WriteLine(Keyboard.getEventCharacter());
//                        Console.WriteLine(Keyboard.getEventKey());
//                        Console.WriteLine(Keyboard.getKeyName(Keyboard.getEventKey()));
//                    }

//                    gl.Translate(tx, 0.0f, tz);


//                    model.render(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f / 16.0f);
//                    gl.PopMatrix();

//                    rot += 0.01f;

//                    GLManager.GL.PushMatrix();
//                    GLManager.GL.MatrixMode(GLEnum.Projection);
//                    GLManager.GL.LoadIdentity();

//                    // Set up orthographic projection (2D)
//                    // Origin at top-left, like typical UI rendering
//                    GLManager.GL.Ortho(0, 400, 300, 0, -1, 1);

//                    // Switch to modelview matrix
//                    GLManager.GL.MatrixMode(GLEnum.Modelview);
//                    GLManager.GL.LoadIdentity();

//                    GLManager.GL.Disable(GLEnum.DepthTest);

//                    mc.fontRenderer.drawStringWithShadow("§4Red §9Blue §aGreen", 10, 30, 0xffffff);

//                    // Centered text
//                    string text = "Hello Minecraft!";
//                    int w = mc.fontRenderer.getStringWidth(text);
//                    mc.fontRenderer.drawStringWithShadow(text, (400 - w) / 2, 100, 0xFFFF00);

//                    // Multiple lines
//                    mc.fontRenderer.drawString("Line 1", 10, 50, 0xffffff);
//                    mc.fontRenderer.drawString("Line 2", 10, 58, 0xffffff); // 8 pixels apart
//                    mc.fontRenderer.drawString("Line 3", 10, 66, 0xffffff);

//                    // Wrapped text
//                    mc.fontRenderer.func_27278_a(
//                        "This is a really long message that will automatically wrap to fit within the specified width!",
//                        10, 150, 300, 0xffffff
//                    );
//                    GLManager.GL.PopMatrix();

//                    // Swap buffers and poll events
//                    glfw.SwapBuffers(window);
//                    glfw.PollEvents();
//                }

//                // Cleanup
//                glfw.DestroyWindow(window);
//                glfw.Terminate();
//            }
//        }
//    }
//}