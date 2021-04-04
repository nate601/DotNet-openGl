using System;
using System.Numerics;
using GameGrid;
using GlBindings;
using openGlTest.EngineObjects;

namespace openGlTest
{
    public static class Program
    {
        internal static Glfw.GlfwWindow window;
        internal static ShaderProgram shaderProgram;
        internal static Action<float> Update;
        public static int Main()
        {
            window = Glfw.InitializeEngine(out object[] callbacks);

            /* Texture tex = new Texture("wall"); */
            /* tex.SetTextureData("wall.jpg"); */
            /* tex.GenerateMipmap(); */
            /* tex.SetActiveTexture(0); */
            Texture tex = TextureManager.GetTexture("wall.jpg");

            shaderProgram = GenerateShaderProgram();
            shaderProgram.Bind();
            shaderProgram.SetUniform("tex", 0);

            Sprite testSprite = new Sprite(tex, shaderProgram);
            Camera camera = new Camera();
            CameraControls camCon = new CameraControls(camera, (100f / 100f));
            camera.transform.position = new Vector3(0, 0, -3);

            Grid grid = new Grid();
            grid.Start();

            float lastFrameTime = 0;
            while (!Glfw.WindowShouldClose(window))
            {
                float time = (float)Glfw.GetTime();

                float deltaTime = time - lastFrameTime;
                lastFrameTime = time;

                Console.WriteLine(MathF.Round(1f / deltaTime));

                Gl.ClearColor(0.392f, 0.584f, 0.929f, 1.0f); // #6495ED
                Gl.Clear(0x4000 | 0x100);


                if (InputManager.GetKeyDown(69))
                {
                    Glfw.SetWindowShouldClose(window, 1);
                }

                Update?.Invoke(deltaTime);

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
    }
}
