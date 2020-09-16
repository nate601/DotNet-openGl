using System;
using System.Runtime.InteropServices;
using GlBindings;

namespace openGlTest
{
    class Program
    {
        static int Main(string[] args)
        {
            Glfw.ErrorFunc errorCallbackDelegate = ErrorCallback;
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
            IntPtr window = Glfw.CreateWindow(640, 480, ".NET Core GL");
            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Error creating context window.");
                return -1;
            }
            Glfw.MakeContextCurrent(window);
            Gl.LoadGl();
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
        public static void ErrorCallback(int errorCode, string description)
        {
            Console.WriteLine($"ERROR:{errorCode} : {description}");
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
