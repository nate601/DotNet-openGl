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
                const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
                Glfw.WindowHint(GLFW_CONTEXT_VERSION_MAJOR, 2);
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
            int glewErr = Glew.GlewInit();

            Console.WriteLine(glewErr);
            {
                const int GL_VENDOR = 0x1F00;
                Console.WriteLine($"OpenGL reports the vendor responsible for this implementation as: {Gl.GetGlString(GL_VENDOR)}");
            }

            Glfw.KeyCallback keyCallbackDelegate = KeyCallback;
            _ = Glfw.SetKeyCallback(window, Marshal.GetFunctionPointerForDelegate(keyCallbackDelegate));
            {
                const int GL_DEPTH_TEST = 0x0B71;
                const int GL_LESS = 0x0201;
                Gl.Enable(GL_DEPTH_TEST);
                Gl.DepthFunction(GL_LESS);
                int bufferIndex = Gl.GenBuffers(1);
                Console.WriteLine($"Buffer assigned {bufferIndex}");
                const int GL_ARRAY_BUFFER = 0x8892;
                const int GL_STATIC_DRAW = 0x88E4;
                float[] points = {0.0f,  0.5f,  0.0f,
                                  0.5f, -0.5f,  0.0f,
                                 -0.5f, -0.5f,  0.0f
                                 };
                Gl.BindBuffer(GL_ARRAY_BUFFER, bufferIndex);
                Gl.BufferData(GL_ARRAY_BUFFER, 9 * Marshal.SizeOf<float>(), ref points, GL_STATIC_DRAW);

                int vaoIndex = Gl.GenVertexArrays();
                Console.WriteLine($"Vertex array assigned {vaoIndex}");
                Gl.BindVertexArray(vaoIndex);
                Gl.EnableVertexAttribArray(0);
                Gl.BindBuffer(GL_ARRAY_BUFFER, bufferIndex);
                const int GL_FLOAT = 0x1406;
                Gl.VertexAttribPointer(0, 3, GL_FLOAT, false, 0, IntPtr.Zero);
                const string vertex_shader =
                    "#version 140\n" +
                    "in vec3 vp;\n" +
                    "void main() {\n" +
                    "  gl_Position = vec4(vp, 1.0);\n" +
                    "}\n";
                const int GL_VERTEX_SHADER = 0x8B31;
                int vs = Gl.CreateShader(GL_VERTEX_SHADER);
                Console.WriteLine("Vertex shader assigned " + vs);
                Gl.ShaderSource(vs, vertex_shader);
                Gl.CompileShader(vs);
                // 0x8B4F
                const int GL_COMPILE_STATUS = 0x8B81;
                int vsCompiledSuccessfully = Gl.GetShader(vs, GL_COMPILE_STATUS);
                Console.WriteLine($"Status: {vsCompiledSuccessfully == 1}");
                if (vsCompiledSuccessfully != 1)
                {
                    Console.Write(Gl.GetShaderInfoLog(vs));
                }
                const string fragment_shader =
                    "#version 140\n" +
                    "out vec4 frag_colour;" +
                    "void main() {" +
                    "  frag_colour = vec4(0.5, 0.0, 0.5, 1.0);" +
                    "}";

                int fs = Gl.CreateShader(0x8B30);
                Gl.ShaderSource(fs, fragment_shader);
                Gl.CompileShader(fs);
                int fsCompiledSuccessfully = Gl.GetShader(vs, GL_COMPILE_STATUS);
                Console.WriteLine($"Status: {fsCompiledSuccessfully == 1}");
                if (fsCompiledSuccessfully != 1)
                {
                    Console.Write(Gl.GetShaderInfoLog(vs));
                }
                int shaderProgram = Gl.CreateProgram();
                Gl.AttachShader(shaderProgram, vs);
                Gl.AttachShader(shaderProgram, fs);
                Gl.LinkProgram(shaderProgram);
            }

            while (!Glfw.WindowShouldClose(window))
            {
                double time = Glfw.GetTime();

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
