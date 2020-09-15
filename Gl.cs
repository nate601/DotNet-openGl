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
            Console.WriteLine($"SUCCESS: Pointer {procName} for delegate {typeof(T).Name} is {pointer}");
            return Marshal.GetDelegateForFunctionPointer<T>(pointer);
        }

        #region internalFunctions
        private static glDrawArrays _DrawArrays;
        private static glGetString _GetGlString;
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal delegate void glDrawArrays(int mode, int first, int count);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal delegate IntPtr glGetString(int gl_reference);
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
        #endregion
    }
}
