using System;
using System.Runtime.InteropServices;
using GlBindings;

namespace openGlTest
{
    class Program
    {
        static int Main(string[] args)
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
            {
                const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
                Glfw.WindowHint(GLFW_OPENGL_DEBUG_CONTEXT, 1);
            }
            IntPtr window = Glfw.CreateWindow(640, 480, ".NET Core GL");
            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Error creating context window.");
                return -1;
            }
            Glfw.MakeContextCurrent(window);
            Gl.LoadGl();
            Gl.Enable(0x92E0);
            Gl.GlErrorCallbackDelegate glErrorCallbackDelegate = GlErrorCallback;
            Gl.DebugMessageCallback(Marshal.GetFunctionPointerForDelegate(glErrorCallbackDelegate));

            {
                const int GL_VENDOR = 0x1F00;
                Console.WriteLine($"OpenGL reports the vendor responsible for this implementation as: {Gl.GetGlString(GL_VENDOR)}");
            }

            Glfw.KeyCallback keyCallbackDelegate = KeyCallback;
            _ = Glfw.SetKeyCallback(window, Marshal.GetFunctionPointerForDelegate(keyCallbackDelegate));
            uint vaoIndex = 255;
            uint shaderProgramIdentifier = 255;
            {
                uint bufferIndex = Gl.GenBuffers(1);
                const int GL_ARRAY_BUFFER = 0x8892;
                const int GL_STATIC_DRAW = 0x88E4;
                float[] points = {0.0f,  0.5f,  0.0f,
                                  0.5f, -0.5f,  0.0f,
                                 -0.5f, -0.5f,  0.0f
                                 };
                Gl.BindBuffer(GL_ARRAY_BUFFER, bufferIndex);
                Gl.BufferData(GL_ARRAY_BUFFER, 9 * Marshal.SizeOf<float>(), points, GL_STATIC_DRAW);

                vaoIndex = Gl.GenVertexArrays();
                Gl.BindVertexArray(vaoIndex);
                Gl.EnableVertexAttribArray(0);
                Gl.BindBuffer(GL_ARRAY_BUFFER, bufferIndex);
                const int GL_FLOAT = 0x1406;
                Gl.VertexAttribPointer(0, 3, GL_FLOAT, false, 0, default);

                Shader vs = new Shader(Shader.ShaderTypes.GL_VERTEX_SHADER);
                vs.LoadShaderSourceFromFile("Vertex_Shader.glsl");
                vs.Compile();
                Shader fs = new Shader(Shader.ShaderTypes.GL_FRAGMENT_SHADER);
                fs.LoadShaderSourceFromFile("Fragment_Shader.glsl");
                fs.Compile();

                ShaderProgram shaderProgram = new ShaderProgram();
                shaderProgram.AttachShader(vs);
                shaderProgram.AttachShader(fs);
                shaderProgram.Link();
                shaderProgramIdentifier = shaderProgram.programIdentifier;
            }

            while (!Glfw.WindowShouldClose(window))
            {
                double time = Glfw.GetTime();

                Gl.ClearColor(0.392f, 0.584f, 0.929f, 1.0f);
                Gl.Clear(0b100000000000000 | 0b100000000);
                Gl.UseProgram(shaderProgramIdentifier);
                Gl.BindVertexArray(vaoIndex);
                Gl.DrawArrays(0x0004, 0, 3);
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
            return 0;
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
