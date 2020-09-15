using System;
using System.Runtime.InteropServices;

namespace GlBindings
{
    internal static class Glew
    {
        [DllImport("glew", EntryPoint = "glewInit")]
        public static extern int GlewInit();
    }
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
        #endregion
    }
}
