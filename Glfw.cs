using System;
using System.Runtime.InteropServices;

namespace GlBindings
{
    static class Glfw
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
        public static extern ErrorFunc SetErrorCallback(ErrorFunc callback);
        [DllImport("glfw", EntryPoint = "glfwCreateWindow")]
        public static extern IntPtr CreateWindow(int width, int height, string title, IntPtr monitor = default(IntPtr), IntPtr share = default(IntPtr));
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
        #endregion
    }
    static class Gl
    {
        public static void LoadGl()
        {
            _GetGlString = GetGlMethod<glGetGlString>("glGetString");
        }
        private static T GetGlMethod<T>(string procName)
        {
            Console.WriteLine($"Attempting to find function {procName} for delegate {typeof(T).Name}");
            var pointer = Glfw.GetGlFunctionAddress(procName);
            if (pointer == IntPtr.Zero)
            {
                Console.WriteLine($"FAILURE: Unable to find function {procName} for delegate {typeof(T).Name}");
                return default(T);
            }
            Console.WriteLine($"SUCCESS: Pointer {procName} for delegate {typeof(T).Name} is {pointer}");
            return Marshal.GetDelegateForFunctionPointer<T>(pointer);
        }
        #region internalFunctions
        private static glDrawArrays _DrawArrays;
        private static glGetGlString _GetGlString;
        #endregion
        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal delegate void glDrawArrays(int mode, int first, int count);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal delegate IntPtr glGetGlString(int gl_reference);
        #endregion
        #region ExternalFunctions
        public static string GetGlString(int gl_reference) => Marshal.PtrToStringAuto(_GetGlString(gl_reference));
        #endregion
    }
}
