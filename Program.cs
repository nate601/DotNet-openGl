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
            Glfw.WindowHint(0x00022007, 1);
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
                Console.Write(vertex_shader);
                Console.WriteLine();
                int vs = Gl.CreateShader(0x8B31);
                Gl.ShaderSource(vs, vertex_shader);
                Gl.CompileShader(vs);
                Console.WriteLine($"Status: {Gl.GetShader(vs, 0x8B4F)}");
                // Console.Write(Gl.GetShaderInfoLog(vs));
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
            Console.WriteLine($"ERROR:{errorCode} : {description}");
        }
        public static void GlErrorCallback(int source, int type, int id, int severity, int length, string message, IntPtr userParam)
        {
            Console.WriteLine(message);
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
