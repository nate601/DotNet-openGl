using System;
using System.Runtime.InteropServices;

namespace GlBindings
{
    internal static class Glfw
    {
        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void ErrorFunc(int errorCode, string description);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void KeyCallback(IntPtr window, int key, int scanCode, int action, int mods);
        #endregion

        #region functions
        [DllImport("glfw", EntryPoint = "glfwInit")]
        public static extern bool Init();
        [DllImport("glfw", EntryPoint = "glfwTerminate")]
        public static extern void Terminate();
        [DllImport("glfw", EntryPoint = "glfwSetErrorCallback")]
        public static extern ErrorFunc SetErrorCallback(IntPtr callback);
        [DllImport("glfw", EntryPoint = "glfwCreateWindow")]
        public static extern IntPtr CreateWindow(int width, int height, string title, IntPtr monitor = default, IntPtr share = default);
        [DllImport("glfw", EntryPoint = "glfwWindowShouldClose")]
        public static extern bool WindowShouldClose(IntPtr window);
        [DllImport("glfw", EntryPoint = "glfwGetTime")]
        public static extern double GetTime();
        [DllImport("glfw", EntryPoint = "glfwSetKeyCallback")]
        public static extern KeyCallback SetKeyCallback(IntPtr window, KeyCallback callback);
        [DllImport("glfw", EntryPoint = "glfwMakeContextCurrent")]
        public static extern void MakeContextCurrent(IntPtr window);
        [DllImport("glfw", EntryPoint = "glfwGetProcAddress", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetGlFunctionAddress(string processName);
        [DllImport("glfw", EntryPoint = "glfwExtensionSupported")]
        public static extern bool IsGlExtensionSupported(string extensionName);
        [DllImport("glfw", EntryPoint = "glfwPollEvents")]
        public static extern void PollEvents();
        [DllImport("glfw", EntryPoint = "glfwSetWindowShouldClose")]
        public static extern void SetWindowShouldClose(IntPtr window, int value);
        [DllImport("glfw", EntryPoint = "glfwSetKeyCallback")]
        public static extern IntPtr SetKeyCallback(IntPtr window, IntPtr cbFun);
        [DllImport("glfw", EntryPoint = "glfwSwapBuffers")]
        public static extern void SwapBuffers(IntPtr window);
        [DllImport("glfw", EntryPoint = "glfwWindowHint")]
        public static extern void WindowHint(int target, int hint);
        [DllImport("glfw", EntryPoint = "glfwGetKey")]
        public static extern int GetKey(IntPtr window, int key);
        [DllImport("glfw", EntryPoint = "glfwGetCursorPos")]
        public static extern void GetCursorPos(IntPtr window, out double xpos, out double ypos);
        #endregion functions
        public static void DefaultWindowHints(bool debug)
        {
            const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
            const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
            const int GLFW_CONTEXT_VERSION_MINOR = 0x00022003;
            const int GLFW_OPENGL_PROFILE = 0x00022008;
            const int GLFW_OPENGL_CORE_PROFILE = 0x00032001;
            const int GLFW_FLOATING = 0x00020007;
            WindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
            WindowHint(GLFW_OPENGL_DEBUG_CONTEXT, debug ? 1 : 0);
            WindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
            WindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
            WindowHint(GLFW_FLOATING, 1);
        }
        public class GlfwWindow
        {
            private readonly IntPtr windowPointer;
            public GlfwWindow(IntPtr windowptr)
            {
                windowPointer = windowptr;
            }
            public static implicit operator IntPtr(GlfwWindow w)
            {
                return w.windowPointer;
            }
            public static implicit operator GlfwWindow(IntPtr i)
            {
                return new GlfwWindow(i);
            }
        }
    }
}
