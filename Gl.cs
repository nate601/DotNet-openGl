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
            _Clear = GetGlMethod<glClear>();
            _UseProgram = GetGlMethod<glUseProgram>();
            _GetProgram = GetGlMethod<glGetProgramiv>();
            _ClearColor = GetGlMethod<glClearColor>();
            _GetShaderSource = GetGlMethod<glGetShaderSource>();
            _SetViewport = GetGlMethod<glViewport>();
            _DrawElements = GetGlMethod<glDrawElements>();
            _GetAttribLocation = GetGlMethod<glGetAttribLocation>();
            _GetUniformLocation = GetGlMethod<glGetUniformLocation>();
            _SetUniformFloat = GetGlMethod<glUniform1f>();
            _SetUniformInt = GetGlMethod<glUniform1i>();
            _GetProgramInfoLog = GetGlMethod<glGetProgramInfoLog>();
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
            //Console.WriteLine($"Attempting to find function {procName} for delegate {typeof(T).Name}");
            IntPtr pointer = Glfw.GetGlFunctionAddress(procName);
            if (pointer == IntPtr.Zero)
            {
                Console.WriteLine($"FAILURE: Unable to find function {procName} for delegate {typeof(T).Name}");
                return default;
            }
            Console.WriteLine($"{procName,-30} : {pointer.ToString("X"),12}");
            //Console.WriteLine($"SUCCESS: Pointer {procName} for delegate {typeof(T).Name} is {pointer.ToString("X")}");
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
        private static glClear _Clear;
        private static glUseProgram _UseProgram;
        private static glGetProgramiv _GetProgram;
        private static glClearColor _ClearColor;
        private static glGetShaderSource _GetShaderSource;
        private static glViewport _SetViewport;
        private static glDrawElements _DrawElements;
        private static glGetAttribLocation _GetAttribLocation;
        private static glGetUniformLocation _GetUniformLocation;
        private static glUniform1f _SetUniformFloat;
        private static glUniform1i _SetUniformInt;
        private static glGetProgramInfoLog _GetProgramInfoLog;
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
        private delegate void glGenBuffers(int size, ref uint buffers);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glBindBuffer(int type, uint bufferIndex);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glBufferData(int type, int size, IntPtr data, int usage);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGenVertexArrays(int size, ref uint arrays);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glBindVertexArray(uint array);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glEnableVertexAttribArray(uint index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glVertexAttribPointer(uint index, int size, int type, bool normalized, int stride, IntPtr pointer);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate uint glCreateShader(int shaderType);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glShaderSource(uint shader, int count, string[] content, int[] lengths);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glCompileShader(uint shader);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetShaderiv(uint shader, int parameterName, out int results);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetShaderInfoLog(uint shaderIndex, int maxLength, out int size, StringBuilder infoLog);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void GlErrorCallbackDelegate(int source, int type, int id, int severeity, int length, string message, IntPtr userParams);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glDebugMessageCallback(IntPtr callback, IntPtr userParam);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate uint glCreateProgram();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glAttachShader(uint program, uint shader);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glLinkProgram(uint program);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glClear(int bitField);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glUseProgram(uint program);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetProgramiv(uint program, int pnmae, out int result);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glClearColor(float red, float green, float blue, float alpha);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetShaderSource(uint shader, int bufferSize, out int length, StringBuilder source);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glViewport(int x, int y, int width, int height);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glDrawElements(int mode, int count, int type, int indices);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate int glGetAttribLocation(int programIndex, string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate int glGetUniformLocation(int programIndex, string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glUniform1i(int location, int uniformValue);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glUniform1f(int location, float uniformValue);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void glGetProgramInfoLog(uint program, int maxLength, out int length, StringBuilder infoLog);
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
        public static uint GenBuffers(int size = 1)
        {
            uint buffers = 0;
            _GenBuffers(size, ref buffers);
            //Console.WriteLine(buffers);
            return buffers;
        }
        public static void BindBuffer(int type, uint bufferIndex)
        {
            _BindBuffer(type, bufferIndex);
        }
        public static void BufferData<T>(int type, int size, T[] data, int usage) where T : IComparable
        {
            IntPtr ptr = GetPointerToArray(data, out Action deallocate);
            BufferData(type, size, ptr, usage);
            deallocate();
        }
        private static IntPtr GetPointerToArray<T>(T[] array, out Action deallocatePtr)
        {

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(array[0].GetType()) * array.Length);
            deallocatePtr = () => Marshal.FreeHGlobal(ptr);
            switch (array)
            {
                case float[] f:
                    Marshal.Copy(f, 0, ptr, array.Length);
                    break;
                case int[] i:
                    Marshal.Copy(i, 0, ptr, array.Length);
                    break;
                default:
                    throw new Exception("Type not handled");
            }
            return ptr;

        }

        public static void BufferData(int type, int size, IntPtr pointerToData, int usage)
        {
            _BufferData(type, size, pointerToData, usage);
        }
        public static uint GenVertexArrays(int size = 1)
        {
            uint array = 255;
            _GenVertexArrays(size, ref array);
            return array;
        }
        public static void BindVertexArray(uint array)
        {
            _BindVertexArray(array);
        }
        public static void EnableVertexAttribArray(uint index)
        {
            _EnableVertexAttribArray(index);
        }
        public static void VertexAttribPointer(uint index, int size, int type, bool normalized, int stride, IntPtr pointer)
        {
            _VertexAttribPointer(index, size, type, normalized, stride, pointer);
        }
        public static uint CreateShader(int shaderType)
        {
            return _CreateShader(shaderType);
        }
        public static void ShaderSource(uint shaderIndex, string[] contents)
        {
            _ShaderSource(shaderIndex, contents.Length, contents, contents.Select(x => x.Length).ToArray());
        }
        public static void ShaderSource(uint shaderIndex, string content)
        {
            ShaderSource(shaderIndex, new string[] { content });
        }
        public static void CompileShader(uint shaderIndex)
        {
            _CompileShader(shaderIndex);
        }
        public static int GetShader(uint shaderIndex, int param)
        {
            //int result = -1;
            _GetShader(shaderIndex, param, out int result);
            return result;
        }
        public static string GetShaderInfoLog(uint shaderIndex, int length)
        {
            StringBuilder result = new StringBuilder(length);
            _GetShaderInfoLog(shaderIndex, length, out int _, result);
            return result.ToString();
        }
        public static string GetShaderInfoLog(uint shaderIndex)
        {
            const int GL_INFO_LOG_LENGTH = 0x8B84;
            int length = GetShader(shaderIndex, GL_INFO_LOG_LENGTH);
            return GetShaderInfoLog(shaderIndex, length);
        }
        public static void DebugMessageCallback(IntPtr callback)
        {
            _DebugMessageCallback(callback, default);
        }
        public static void DebugMessageCallback<T>(T callback)
        {
            DebugMessageCallback(Marshal.GetFunctionPointerForDelegate(callback));
        }
        public static uint CreateProgram()
        {
            return _CreateProgram();
        }
        public static void AttachShader(uint program, uint shader)
        {
            _AttachShader(program, shader);
        }
        public static void LinkProgram(uint program)
        {
            _LinkProgram(program);
        }
        public static void Clear(int bitField)
        {
            _Clear(bitField);
        }
        public static void UseProgram(uint program)
        {
            _UseProgram(program);
        }
        public static int GetProgram(uint program, int param)
        {
            _GetProgram(program, param, out int val);
            return val;
        }
        public static void ClearColor(float red, float green, float blue, float alpha)
        {
            _ClearColor(red, green, blue, alpha);
        }
        public static string GetShaderSource(uint shader)
        {
            StringBuilder result = new StringBuilder(128);
            _GetShaderSource(shader, 128, out int _, result);
            return result.ToString();
        }
        public static void SetViewport(int x, int y, int width, int height)
        {
            _SetViewport(x, y, width, height);
        }
        public static void DrawElements(int mode, int size, int type, int indices)
        {
            _DrawElements(mode, size, type, indices);
        }
        public static int GetAttribLocation(int programIndex, string location)
        {
            return _GetAttribLocation(programIndex, location);
        }
        public static int GetUniformLocation(int programIndex, string location)
        {
            return _GetUniformLocation(programIndex, location);
        }
        public static void SetUniform(int uniformLocation, int uniformValue)
        {
            _SetUniformInt(uniformLocation, uniformValue);
        }
        public static void SetUniform(int uniformLocation, bool uniformValue)
        {
            SetUniform(uniformLocation, uniformValue ? 1 : 0);
        }
        public static void SetUniform(int uniformLocation, float uniformValue)
        {
            _SetUniformFloat(uniformLocation, uniformValue);
        }
        public static string GetProgramInfoLog(uint program, int length)
        {
            StringBuilder result = new StringBuilder(length);
            _GetProgramInfoLog(program, length, out int _, result);
            return result.ToString();
        }
        public static string GetProgramInfoLog(uint program)
        {
            const int GL_INFO_LOG_LENGTH = 0x8B84;
            int length = GetProgram(program, GL_INFO_LOG_LENGTH);
            return GetProgramInfoLog(program, length);
        }
        #endregion
    }
}
