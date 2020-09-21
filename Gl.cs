using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
            _GenVertexArrays = GetGlMethod<glGenVertexArrays>();
            _BindVertexArray = GetGlMethod<glBindVertexArray>();
            _EnableVertexAttribArray = GetGlMethod<glEnableVertexAttribArray>();
            _VertexAttribPointer = GetGlMethod<glVertexAttribPointer>();
            _CreateShader = GetGlMethod<glCreateShader>();
            _ShaderSource = GetGlMethod<glShaderSource>();
            _CompileShader = GetGlMethod<glCompileShader>();
            _GetShader = GetGlMethod<glGetShaderiv>();
            _GetShaderInfoLog = GetGlMethod<glGetShaderInfoLog>();
            _DebugMessageCallback = GetGlMethod<glDebugMessageCallback>();
            _CreateProgram = GetGlMethod<glCreateProgram>();
            _AttachShader = GetGlMethod<glAttachShader>();
            _LinkProgram = GetGlMethod<glLinkProgram>();
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
        private static glGenVertexArrays _GenVertexArrays;
        private static glBindVertexArray _BindVertexArray;
        private static glEnableVertexAttribArray _EnableVertexAttribArray;
        private static glVertexAttribPointer _VertexAttribPointer;
        private static glCreateShader _CreateShader;
        private static glShaderSource _ShaderSource;
        private static glCompileShader _CompileShader;
        private static glGetShaderiv _GetShader;
        private static glGetShaderInfoLog _GetShaderInfoLog;
        private static glDebugMessageCallback _DebugMessageCallback;
        private static glCreateProgram _CreateProgram;
        private static glAttachShader _AttachShader;
        private static glLinkProgram _LinkProgram;
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGenVertexArrays(int size, ref int arrays);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glBindVertexArray(int array);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glEnableVertexAttribArray(int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glVertexAttribPointer(int index, int size, int type, bool normalized, int stride, IntPtr pointer);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate int glCreateShader(int shaderType);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glShaderSource(int shader, int count, string[] content, int[] lengths);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glCompileShader(int shader);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetShaderiv(int shader, int parameterName, out int results);
        //private delegate void glGetShaderInfoLog(int shaderIndex, int maxLength, ref int size, [MarshalAs(UnmanagedType.LPStr)] ref StringBuilder infoLog);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetShaderInfoLog(int shaderIndex, int maxLength, ref int size, StringBuilder infoLog);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void GlErrorCallbackDelegate(int source, int type, int id, int severeity, int length, string message, IntPtr userParams);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glDebugMessageCallback(IntPtr callback, IntPtr userParam);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate int glCreateProgram();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glAttachShader(int program, int shader);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glLinkProgram(int program);
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
        public static int GenVertexArrays(int size = 1)
        {
            int array = -1;
            _GenVertexArrays(size, ref array);
            return array;
        }
        public static void BindVertexArray(int array)
        {
            _BindVertexArray(array);
        }
        public static void EnableVertexAttribArray(int index)
        {
            _EnableVertexAttribArray(index);
        }
        public static void VertexAttribPointer(int index, int size, int type, bool normalized, int stride, IntPtr pointer)
        {
            _VertexAttribPointer(index, size, type, normalized, stride, pointer);
        }
        public static int CreateShader(int shaderType)
        {
            return _CreateShader(shaderType);
        }
        public static void ShaderSource(int shaderIndex, string[] contents)
        {
            _ShaderSource(shaderIndex, contents.Length, contents, contents.Select(x => x.Length).ToArray());
        }
        public static void ShaderSource(int shaderIndex, string content)
        {
            ShaderSource(shaderIndex, new string[] { content });
        }
        public static void CompileShader(int shaderIndex)
        {
            _CompileShader(shaderIndex);
        }
        public static int GetShader(int shaderIndex, int param)
        {
            //int result = -1;
            _GetShader(shaderIndex, param, out int result);
            return result;
        }

        public static string GetShaderInfoLog(int shaderIndex)
        {
            StringBuilder result = new StringBuilder(128);
            int size = 128;
            _GetShaderInfoLog(shaderIndex, 128, ref size, result);
            return result.ToString();
        }
        public static void DebugMessageCallback(IntPtr callback)
        {
            _DebugMessageCallback(callback, default);
        }
        public static int CreateProgram()
        {
            return _CreateProgram();
        }
        public static void AttachShader(int program, int shader)
        {
            _AttachShader(program, shader);
        }
        public static void LinkProgram(int program)
        {
            _LinkProgram(program);
        }
        #endregion
    }
}
