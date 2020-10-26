using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace GlBindings
{
    internal static class Gl
    {
#pragma warning disable // Disable warnings because this function is only called dynamically at runtime
        private static T GetGlMethod<T>(string procName)
#pragma warning enable
        {
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
        public static void LoadDelegates()
        {
            foreach (Type delegateType in typeof(Gl).GetNestedTypes(BindingFlags.NonPublic).Where(x => x.BaseType == typeof(MulticastDelegate)))
            {
                /* Console.WriteLine("Delegate type name: " + delegateType.Name); */
                foreach (FieldInfo internalFunction in typeof(Gl).GetFields(BindingFlags.NonPublic | BindingFlags.Static).Where(x => x.FieldType == delegateType))
                {
                    /* Console.WriteLine("Internal function name: " + internalFunction.Name); */
                    object delegateForPtr = typeof(Gl).GetMethod("GetGlMethod", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(delegateType).Invoke(null, new[] { delegateType.Name });
                    internalFunction.SetValue(null, delegateForPtr);
                }

            }
        }

        #region internalFunctions
#pragma warning disable  //Disable warnings because the functions are assigned dynamically at runtime
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
        private static glGenTextures _GenTextures;
        private static glTexImage2D _TexImage2D;
        private static glBindTexture _BindTexture;
        private static glGenerateMipmap _GenerateMipmap;
        private static glActiveTexture _ActiveTexture;
        private static glUniform3fv _SetUniformFloatVector3;
        private static glUniformMatrix4fv _SetUniformMatrix4fv;
        private static glDeleteShader _DeleteShader;
#pragma warning enable
        #endregion internalFunctions

        #region Delegates
        private delegate void glDrawArrays(int mode, int first, int count);
        private delegate IntPtr glGetString(int gl_reference);
        private delegate void glEnable(int cap);
        private delegate void glDepthFunc(int depthFunctionMethod);
        private delegate void glGenBuffers(int size, ref uint buffers);
        private delegate void glBindBuffer(int type, uint bufferIndex);
        private delegate void glBufferData(int type, int size, IntPtr data, int usage);
        private delegate void glGenVertexArrays(int size, ref uint arrays);
        private delegate void glBindVertexArray(uint array);
        private delegate void glEnableVertexAttribArray(uint index);
        private delegate void glVertexAttribPointer(uint index, int size, int type, bool normalized, int stride, IntPtr pointer);
        private delegate uint glCreateShader(int shaderType);
        private delegate void glShaderSource(uint shader, int count, string[] content, int[] lengths);
        private delegate void glCompileShader(uint shader);
        private delegate void glGetShaderiv(uint shader, int parameterName, out int results);
        private delegate void glGetShaderInfoLog(uint shaderIndex, int maxLength, out int size, StringBuilder infoLog);
        public delegate void GlErrorCallbackDelegate(int source, int type, int id, int severeity, int length, string message, IntPtr userParams);
        private delegate void glDebugMessageCallback(IntPtr callback, IntPtr userParam);
        private delegate uint glCreateProgram();
        private delegate void glAttachShader(uint program, uint shader);
        private delegate void glLinkProgram(uint program);
        private delegate void glClear(int bitField);
        private delegate void glUseProgram(uint program);
        private delegate void glGetProgramiv(uint program, int pnmae, out int result);
        private delegate void glClearColor(float red, float green, float blue, float alpha);
        private delegate void glGetShaderSource(uint shader, int bufferSize, out int length, StringBuilder source);
        private delegate void glViewport(int x, int y, int width, int height);
        private delegate void glDrawElements(int mode, int count, int type, int indices);
        private delegate int glGetAttribLocation(int programIndex, string name);
        private delegate int glGetUniformLocation(int programIndex, string name);
        private delegate void glUniform1i(int location, int uniformValue);
        private delegate void glUniform1f(int location, float uniformValue);
        private delegate void glUniform3fv(int location, int count, float[] val);
        private delegate void glGetProgramInfoLog(uint program, int maxLength, out int length, StringBuilder infoLog);
        private delegate void glGenTextures(int count, out int textureIndex);
        private delegate void glTexImage2D(int targetType, int levelOfDetail, int internalFormat, int width, int height, int border, int format, int type, IntPtr pointerToData);
        private delegate void glBindTexture(int target, int texture);
        private delegate void glGenerateMipmap(int target);
        private delegate void glActiveTexture(int texture);
        private delegate void glUniformMatrix4fv(int location, int count, bool transpose, float[] val);
        private delegate void glDeleteShader(uint shader);
        #endregion Delegates


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
        public static void SetUniform(int uniformLocation, float[] uniformValue, bool matrix = false)
        {
            if (matrix)
            {
                SetUniformMatrix(uniformLocation, uniformValue);
                return;
            }
            switch (uniformValue.Length)
            {
                case 3:
                    _SetUniformFloatVector3(uniformLocation, 1, uniformValue);
                    break;
                default:
                    throw new Exception($"Uniform vector of length {uniformValue.Length} not implemented.");
                    break;
            }
        }
        private static void SetUniformMatrix(int uniformLocation, float[] uniformValue)
        {
            switch (uniformValue.Length)
            {
                case 4*4:
                    _SetUniformMatrix4fv(uniformLocation, 1, false, uniformValue);
                    break;
                default:
                    throw new Exception($"Uniform matrix of length {uniformValue.Length} not implemented.");
                    break;
            }

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
        public static int GenTextures()
        {
            _GenTextures(1, out int textureIndex);
            return textureIndex;
        }
        public static void TexImage2D(int targetType, int levelOfDetail, int internalFormat, int width, int height, int border, int format, int type, IntPtr pointerToData)
        {
            _TexImage2D(targetType, levelOfDetail, internalFormat, width, height, border, format, type, pointerToData);
        }
        public static void BindTexture(int target, int texture)
        {
            _BindTexture(target, texture);
        }
        public static void GenerateMipmap(int target)
        {
            _GenerateMipmap(target);
        }
        public static void ActiveTexture(int textureNumber)
        {
            const int GL_TEXTURE0 = 0x84C0;
            _ActiveTexture(GL_TEXTURE0 + textureNumber);
        }
        public static void DeleteShader(uint shader)
        {
            _DeleteShader(shader);
        }
        #endregion ExternalFunctions
    }
}
