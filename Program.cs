using System;
using System.Numerics;
using System.Runtime.InteropServices;
using GameGrid;
using GlBindings;
using openGlTest.EngineObjects;

namespace openGlTest
{
    public static class Program
    {
        internal static Glfw.GlfwWindow window;
        public static int Main()
        {
            window = InitializeEngine(out object[] callbacks);

            Texture tex = new Texture();
            tex.SetTextureData("wall.jpg");
            tex.GenerateMipmap();
            tex.SetActiveTexture(0);

            ShaderProgram shaderProgram = GenerateShaderProgram();
            shaderProgram.Bind();
            shaderProgram.SetUniform("tex", 0);

            Sprite testSprite = new Sprite(tex, shaderProgram);
            Camera camera = new Camera();
            CameraControls camCon = new CameraControls(camera, (100f/100f));
            camera.transform.position = new Vector3(0, 0, -3);

            Grid grid = new Grid();

            Sprite otherSprite = new Sprite(tex, shaderProgram);
            float lastFrameTime = 0;
            Renderer.subscribeToRender.Add(otherSprite);
            while (!Glfw.WindowShouldClose(window))
            {
                float time = (float)Glfw.GetTime();

                float deltaTime = time - lastFrameTime;
                lastFrameTime = time;
                /* const float speed = 10f / 100f; */
                Console.WriteLine(MathF.Round(1f/deltaTime));

                Gl.ClearColor(0.392f, 0.584f, 0.929f, 1.0f); // #6495ED
                Gl.Clear(0x4000 | 0x100);

                /* testSprite.transform.position = (testSprite.transform.position + new Vector3(deltaTime*speed, 0, 0)); */

                if(InputManager.GetKeyDown(69))
                {
                    Glfw.SetWindowShouldClose(window, 1);
                }

                camCon.Update(deltaTime);

                /* Renderer.subscribeToRender.Add(testSprite); */
                Renderer.Update();

                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            foreach (var callback in callbacks)
            {
                GC.KeepAlive(callback);
            }

            Glfw.Terminate();
            return 0;
        }

        private static Glfw.GlfwWindow InitializeEngine(out object[] callbacks)
        {
            Glfw.ErrorFunc errorCallbackDelegate = GlfwErrorCallback;
            _ = Glfw.SetErrorCallback(Marshal.GetFunctionPointerForDelegate(errorCallbackDelegate));
            if (Glfw.Init())
            {
                Console.WriteLine("Glfw has successfully initialized");
            }
            else
            {
                Console.WriteLine("Glfw has failed to successfully initialize");
                throw new Exception("Glfw has failed to successfully initialize");
            }
            Glfw.DefaultWindowHints(true);
            Glfw.GlfwWindow window = Glfw.CreateWindow(640, 480, ".NET Core GL");
            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Error creating context window.");
                throw new Exception("Error creating context window.");
            }
            Glfw.MakeContextCurrent(window);
            Gl.LoadDelegates();
            Gl.Enable(0x92E0);
            Gl.GlErrorCallbackDelegate glErrorCallbackDelegate = GlErrorCallback;
            Gl.SetViewport(0, 0, 640, 480);
            Gl.DebugMessageCallback(glErrorCallbackDelegate);

            Glfw.KeyCallback keyCallbackDelegate = KeyCallback;
            _ = Glfw.SetKeyCallback(window, Marshal.GetFunctionPointerForDelegate(keyCallbackDelegate));
            callbacks = new object[] { (object)keyCallbackDelegate, (object)errorCallbackDelegate };
            return window;
        }

        private static ShaderProgram GenerateShaderProgram()
        {
            Shader vs = new Shader(Shader.ShaderTypes.GL_VERTEX_SHADER);
            /* vs.LoadShaderSourceFromFile("Vertex_Shader.glsl"); */
            vs.LoadShaderFromShaderFolder("BaseShader", Shader.ShaderTypes.GL_VERTEX_SHADER);
            if (!vs.TryCompile(out string vsInfolog))
            {
                Console.WriteLine($"Compilation failed:\n {vsInfolog}");
                throw new Exception(vsInfolog);
            }

            Shader fs = new Shader(Shader.ShaderTypes.GL_FRAGMENT_SHADER);
            fs.LoadShaderFromShaderFolder("BaseShader", Shader.ShaderTypes.GL_FRAGMENT_SHADER);
            /* fs.LoadShaderSourceFromFile("Fragment_Shader.glsl"); */
            if (!fs.TryCompile(out string fsInfolog))
            {
                Console.WriteLine($"Compilation failed:\n {fsInfolog}");
                throw new Exception(fsInfolog);
            }

            ShaderProgram shaderProgram = new ShaderProgram(vs, fs);
            if (!shaderProgram.TryLink(out string linkingInfolog))
            {
                Console.WriteLine($"Linking failed:\n {linkingInfolog}");
                throw new Exception(linkingInfolog);
            }
            vs.Delete();
            fs.Delete();

            return shaderProgram;
        }

        public static void GlfwErrorCallback(int errorCode, string description)
        {
            Console.WriteLine($"GLFW ERROR:{errorCode} : {description}");
        }
        public static void GlErrorCallback(int source, int type, int id, int severity, int length, string message, IntPtr userParam)
        {
            Console.WriteLine("GL ERROR: " + message);
        }
        public static void KeyCallback(IntPtr window, int key, int scancode, int action, int modifiers)
        {
            InputManager.KeyEvent(window, key, scancode, action, modifiers);
        }
    }
}
