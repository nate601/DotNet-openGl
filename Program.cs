using System;
using System.Runtime.InteropServices;
using GlBindings;

namespace openGlTest
{
    public static class Program
    {
        public static int Main()
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

            float[] vertices = new float[]{
            //position location1   texture location2
             0.5f,  0.5f, 0.0f,  1.0f, 1.0f, // top    right
             0.5f, -0.5f, 0.0f,  1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f,  0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f,  0.0f, 1.0f  // top    left
            };
            int[] indices = new int[]{
                0, 1, 3,
                1, 2, 3
            };

            Texture tex = new Texture();
            tex.SetTextureData("wall.jpg");
            tex.GenerateMipmap();
            tex.SetActiveTexture(0);

            VertexBufferObject vbo = new VertexBufferObject(BufferType.GL_ARRAY_BUFFER);
            VertexArrayObject vao = new VertexArrayObject();
            ElementBufferObject ebo = new ElementBufferObject();

            vao.Bind();

            vbo.Bind();
            vbo.BufferData(vertices, DrawType.GL_STATIC_DRAW);

            ebo.Bind();
            ebo.BufferData(indices, DrawType.GL_STATIC_DRAW);

            // location = 0 position
            _ = vao.AddAttribute(3, DataType.GL_FLOAT, false, Marshal.SizeOf(typeof(float)) * 5);
            // location = 1 texture map
            _ = vao.AddAttribute(2, DataType.GL_FLOAT, false, Marshal.SizeOf(typeof(float)) * 5);

            ShaderProgram shaderProgram = GenerateShaderProgram();
            shaderProgram.Bind();
            shaderProgram.SetUniform("tex", 0);

            Vector3D wallPosition = new Vector3D(0, 0, 0);
            Vector3D cameraPosition = new Vector3D(0, 0, -3);


            float lastFrameTime = 0;
            while (!Glfw.WindowShouldClose(window))
            {
                float time = (float)Glfw.GetTime();

                float deltaTime = time - lastFrameTime;
                lastFrameTime = time;
                float speed = 10f / 100f;
                /* Console.WriteLine(deltaTime); */

                Gl.ClearColor(0.392f, 0.584f, 0.929f, 1.0f);
                Gl.Clear(0b100000000000000 | 0b100000000);
                shaderProgram.Bind();
                vao.Bind();
                shaderProgram.SetUniform("time", time);

                float[,] model = MatrixProjections.Transform(MatrixProjections.identity, wallPosition, 0);
                model = MatrixProjections.Transform(model, wallPosition);
                float[,] view = MatrixProjections.Translation(cameraPosition);
                float[,] projection = MatrixProjections.GetPerspectiveProjection(45, 640, 480, 0.1f, 100);
                wallPosition = new Vector3D(wallPosition.x, wallPosition.y + (deltaTime * speed), wallPosition.z);
                Console.WriteLine(wallPosition.y);


                Console.WriteLine(model.ToStringPretty());
                Console.WriteLine(view.ToStringPretty());
                Console.WriteLine(projection.ToStringPretty());

                shaderProgram.SetUniform("model", model);
                shaderProgram.SetUniform("view", view);
                shaderProgram.SetUniform("projection", projection);


                Gl.DrawElements(0x0004, 6, 0x1405, 0);
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
            return 0;
        }

        private static ShaderProgram GenerateShaderProgram()
        {
            Shader vs = new Shader(Shader.ShaderTypes.GL_VERTEX_SHADER);
            vs.LoadShaderSourceFromFile("Vertex_Shader.glsl");
            if (!vs.TryCompile(out string vsInfolog))
            {
                Console.WriteLine($"Compilation failed:\n {vsInfolog}");
                throw new Exception(vsInfolog);
            }

            Shader fs = new Shader(Shader.ShaderTypes.GL_FRAGMENT_SHADER);
            fs.LoadShaderSourceFromFile("Fragment_Shader.glsl");
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
            //Console.WriteLine($"Key: {key} , Action: {action}");
            if (key == 69 && action == 1)
            {
                Glfw.SetWindowShouldClose(window, 1);
            }
        }
    }
}
