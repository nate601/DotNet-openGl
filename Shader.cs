using System.IO;

namespace GlBindings
{
    public class Shader
    {
        internal uint shaderIdentifier;

        internal ShaderTypes ShaderType;

        public string ShaderSource
        {
            set => Gl.ShaderSource(shaderIdentifier, value);
        }

        public void Compile()
        {
            Gl.CompileShader(shaderIdentifier);
        }

        public void SetShaderSource(string shaderSource)
        {
            ShaderSource = shaderSource;
        }

        public void LoadShaderSourceFromFile(string filePath)
        {
            string v = File.ReadAllText(filePath);
            ShaderSource = v;
        }

        public Shader(ShaderTypes shaderType)
        {
            ShaderType = shaderType;
            shaderIdentifier = Gl.CreateShader((int)shaderType);
        }

        public enum ShaderTypes
        {
            GL_VERTEX_SHADER    = 0x8B31,
            GL_FRAGMENT_SHADER  = 0x8B30
        }
    }
}
