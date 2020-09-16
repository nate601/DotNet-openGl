using System;
using System.Runtime.InteropServices;

namespace GlBindings
{
    internal static class Gl
    {
        public static void LoadGl()
        {
            _GetGlString = GetGlMethod<glGetString>();
            _DrawArrays = GetGlMethod<glDrawArrays>();
            _Enable = GetGlMethod<glEnable>();
            _DepthFunction = GetGlMethod<glDepthFunc>();
            _GenBuffers = GetGlMethod<glGenBuffers>();
            _BindBuffer = GetGlMethod<glBindBuffer>();
            _BufferData = GetGlMethod<glBufferData>();
        }

        private static T GetGlMethod<T>()
        {
#pragma warning disable CS0618
            return GetGlMethod<T>(typeof(T).Name);
#pragma warning restore CS0618
        }

        [Obsolete("Use GetGlMethod without the string name, strings should match in cpp and csharp")]
        private static T GetGlMethod<T>(string procName)
        {
            Console.WriteLine($"Attempting to find function {procName} for delegate {typeof(T).Name}");
            IntPtr pointer = Glfw.GetGlFunctionAddress(procName);
            if (pointer == IntPtr.Zero)
            {
                Console.WriteLine($"FAILURE: Unable to find function {procName} for delegate {typeof(T).Name}");
                return default;
            }
            Console.WriteLine($"SUCCESS: Pointer {procName} for delegate {typeof(T).Name} is {pointer.ToString("X")}");
            return Marshal.GetDelegateForFunctionPointer<T>(pointer);
        }

        #region internalFunctions
        private static glDrawArrays _DrawArrays;
        private static glGetString _GetGlString;
        private static glEnable _Enable;
        private static glDepthFunc _DepthFunction;
        private static glGenBuffers _GenBuffers;
        private static glBindBuffer _BindBuffer;
        private static glBufferData _BufferData;
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glDrawArrays(int mode, int first, int count);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate IntPtr glGetString(int gl_reference);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glEnable(int cap);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glDepthFunc(int depthFunctionMethod);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGenBuffers(int size, ref int buffers);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glBindBuffer(int type, int bufferIndex);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glBufferData(int type, int size, ref float[] data, int usage);
        #endregion


        #region ExternalFunctions
        public static string GetGlString(int gl_reference)
        {
            return Marshal.PtrToStringAuto(_GetGlString(gl_reference));
        }
        public static void DrawArrays(int mod, int first, int count)
        {
            _DrawArrays(mod, first, count);
        }
        public static void Enable(int cap)
        {
            _Enable(cap);
        }
        public static void DepthFunction(int depthFunctionMethod)
        {
            _DepthFunction(depthFunctionMethod);
        }
        public static int GenBuffers(int size = 1)
        {
            int buffers = -1;
            _GenBuffers(size, ref buffers);
            return buffers;
        }
        public static void BindBuffer(int type, int bufferIndex)
        {
            _BindBuffer(type, bufferIndex);
        }
        public static void BufferData(int type, int size, ref float[] data, int usage)
        {
            _BufferData(type, size, ref data, usage);
        }

        #endregion
    }
}
