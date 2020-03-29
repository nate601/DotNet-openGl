using System;
using GlBindings;

namespace openGlTest
{
    class Program
    {
        static int Main(string[] args)
        {
            Glfw.SetErrorCallback(ErrorCallback);
            if (Glfw.Init())
            {
                Console.WriteLine("Glfw has successfully initialized");
            }
            else
            {
                Console.WriteLine("Glfw has failed to successfully initialize");
                return -1;
            }
            var window = Glfw.CreateWindow(640, 480, ".NET Core GL");
            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Error creating context window.");
                return -1;
            }
            Glfw.MakeContextCurrent(window);
            Gl.LoadGl();

            {
                const int GL_VENDOR = 0x1F00;
                Console.WriteLine($"OpenGL reports the vendor responsible for this implementation as: {Gl.GetGlString(GL_VENDOR)}");
            }

            while (!Glfw.WindowShouldClose(window))
            {

		Glfw.PollEvents();
            }

            Glfw.Terminate();
            return 0;
        }
        public static void ErrorCallback(int errorCode, string description)
        {
            Console.WriteLine($"ERROR:{errorCode} : {description}");
        }
    }
}
