using System;
using System.Drawing;
using System.Runtime.InteropServices;
using GlBindings;

namespace openGlTest
{
    public class Program
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
                return -1;
            }
            Glfw.DefaultWindowHints(true);
            Glfw.GlfwWindow window = Glfw.CreateWindow(640, 480, ".NET Core GL");
            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Error creating context window.");
                return -1;
            }
            Glfw.MakeContextCurrent(window);
            Gl.LoadGl();
            Gl.Enable(0x92E0);
            Gl.GlErrorCallbackDelegate glErrorCallbackDelegate = GlErrorCallback;
            Gl.SetViewport(0, 0, 640, 480);
            Gl.DebugMessageCallback(glErrorCallbackDelegate);

            Glfw.KeyCallback keyCallbackDelegate = KeyCallback;
            _ = Glfw.SetKeyCallback(window, Marshal.GetFunctionPointerForDelegate(keyCallbackDelegate));

            float[] vertices = new float[]{
            0.5f,  0.5f, 0.0f,  // top right
            0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left
            };
            int[] indices = new int[]{
                0, 1, 3,
                1, 2, 3
            };

            Bitmap bp = (Bitmap)Image.FromFile("wall.jpg");
            int tex = Gl.GenTextures();

            VertexBufferObject vbo = new VertexBufferObject(BufferType.GL_ARRAY_BUFFER);
            VertexArrayObject vao = new VertexArrayObject();
            ElementBufferObject ebo = new ElementBufferObject();

            vao.Bind();

            vbo.Bind();
            vbo.BufferData(vertices, DrawType.GL_STATIC_DRAW);

            ebo.Bind();
            ebo.BufferData(indices, DrawType.GL_STATIC_DRAW);

            _ = vao.AddAttribute(3, DataType.GL_FLOAT, false, 0);

            ShaderProgram shaderProgram = GenerateShaderProgram();

            while (!Glfw.WindowShouldClose(window))
            {
                float time = (float)Glfw.GetTime();

                Gl.ClearColor(0.392f, 0.584f, 0.929f, 1.0f);
                Gl.Clear(0b100000000000000 | 0b100000000);
                shaderProgram.Bind();
                vao.Bind();
                shaderProgram.SetUniform("time", time);

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
            }

            Shader fs = new Shader(Shader.ShaderTypes.GL_FRAGMENT_SHADER);
            fs.LoadShaderSourceFromFile("Fragment_Shader.glsl");
            if (!fs.TryCompile(out string fsInfoLog))
            {
                Console.WriteLine($"Compilation failed:\n {fsInfoLog}");
            }

            ShaderProgram shaderProgram = new ShaderProgram(vs, fs);
            if (!shaderProgram.TryLink(out string linkingInfoLog))
            {
                Console.WriteLine($"Linking failed:\n {linkingInfoLog}");
            }

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
