﻿using System;
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
            SetWindowHints();
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
            Gl.SetViewport(0, 0, 640, 480);
            Gl.DebugMessageCallback(Marshal.GetFunctionPointerForDelegate(glErrorCallbackDelegate));

            {
                const int GL_VENDOR = 0x1F00;
                Console.WriteLine($"OpenGL reports the vendor responsible for this implementation as: {Gl.GetGlString(GL_VENDOR)}");
            }

            Glfw.KeyCallback keyCallbackDelegate = KeyCallback;
            _ = Glfw.SetKeyCallback(window, Marshal.GetFunctionPointerForDelegate(keyCallbackDelegate));

            float[] points = {    0.0f,  0.5f,  0.0f,
                                  0.5f, -0.5f,  0.0f,
                                 -0.5f, -0.5f,  0.0f,
                                 };
            /* float[] vertices = new float[]{ */
            /* 0.5f,  0.5f, 0.0f,  // top right */
            /* 0.5f, -0.5f, 0.0f,  // bottom right */
            /* -0.5f, -0.5f, 0.0f,  // bottom left */
            /* -0.5f,  0.5f, 0.0f   // top left */
            /* }; */
            /* uint[] indices = new uint[]{ */
            /*     0, 1, 3, */
            /*     1, 2, 3 */
            /* } */

            VertexBufferObject vbo = new VertexBufferObject(BufferType.GL_ARRAY_BUFFER);
            vbo.Bind();
            vbo.BufferData(points, DrawType.GL_STATIC_DRAW);
            /* uint ebo = Gl.GenBuffers(0x8893); */
            /* Gl.BindBuffer(0x8893, ebo); */

            /*             Gl.BufferData(); */
            VertexArrayObject vao = new VertexArrayObject();
            vao.Bind();
            vao.EnableVertexAttribArray(0);
            vao.VertexAttribPointer(0, 3, DataType.GL_FLOAT, false, 0);

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

            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.AttachShader(vs, fs);
            shaderProgram.Link();


            while (!Glfw.WindowShouldClose(window))
            {
                double time = Glfw.GetTime();

                Gl.ClearColor(0.392f, 0.584f, 0.929f, 1.0f);
                Gl.Clear(0b100000000000000 | 0b100000000);
                shaderProgram.Bind();
                vao.Bind();
                Gl.DrawArrays(0x0004, 0, 3);
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
            return 0;
        }

        private static void SetWindowHints()
        {
            const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
            const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
            const int GLFW_CONTEXT_VERSION_MINOR = 0x00022003;
            const int GLFW_OPENGL_PROFILE = 0x00022008;
            const int GLFW_OPENGL_CORE_PROFILE = 0x00032001;
            Glfw.WindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
            Glfw.WindowHint(GLFW_OPENGL_DEBUG_CONTEXT, 1);
            Glfw.WindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
            Glfw.WindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
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
