using System.IO;

namespace GlBindings
{
    public class Shader
    {
        internal uint shaderIdentifier;

        public bool isActive;

        internal ShaderTypes ShaderType;

        public string ShaderSource
        {
            set => Gl.ShaderSource(shaderIdentifier, value);
            get => Gl.GetShaderSource(shaderIdentifier);
        }

        public void Compile()
        {
            Gl.CompileShader(shaderIdentifier);
        }
        public bool TryCompile(out string InfoLog)
        {
            Gl.CompileShader(shaderIdentifier);
            InfoLog = HasInfoLog() ? GetInfoLog() : (default);
            return LastCompileSuccessful;
        }
        public bool LastCompileSuccessful
        {
            get
            {
                const int GL_COMPILE_STATUS = 0x8B81;
                return Gl.GetShader(shaderIdentifier, GL_COMPILE_STATUS) == 1;
            }
        }
        public string GetInfoLog()
        {
            if (!HasInfoLog())
            {
                return null;
            }
            int length = Gl.GetShader(shaderIdentifier, 0x8B84);
            return Gl.GetShaderInfoLog(shaderIdentifier, length);
        }
        private bool HasInfoLog()
        {
            return Gl.GetShader(shaderIdentifier, 0x8B84) != 0;
        }
        public void SetShaderSource(string shaderSource)
        {
            ShaderSource = shaderSource;
        }

        public void LoadShaderSourceFromFile(string filePath)
        {
            ShaderSource = File.ReadAllText(filePath);
        }
        public void LoadShaderFromShaderFolder(string shaderName, ShaderTypes shaderType = 0)
        {
            var filePath = $"ProjectFiles/{shaderName}/";
            if(shaderType == 0)
            {
                shaderType = ShaderType;
            }
            switch (shaderType)
            {
                case ShaderTypes.GL_VERTEX_SHADER:
                    filePath += "Vertex_Shader.glsl";
                    break;
                case ShaderTypes.GL_FRAGMENT_SHADER:
                    filePath += "Fragment_Shader.glsl";
                    break;
                default:
                    break;
            }
            ShaderSource = File.ReadAllText(filePath);
        }

        public Shader(ShaderTypes shaderType)
        {
            ShaderType = shaderType;
            shaderIdentifier = Gl.CreateShader((int)shaderType);
            isActive = true;
        }
        public void Delete()
        {
            Gl.DeleteShader(shaderIdentifier);
            isActive = false;
        }

        public enum ShaderTypes
        {
            GL_VERTEX_SHADER = 0x8B31,
            GL_FRAGMENT_SHADER = 0x8B30
        }
    }
}
