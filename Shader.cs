using System.IO;

namespace GlBindings
{
    public abstract class ShaderBase
    {
        internal uint shaderIdentifier;
        public abstract void Compile();
        public abstract void Init();
        public abstract string ShaderSource { set; }

        public void SetShaderSource(string shaderSource)
        {
            ShaderSource = shaderSource;
        }
        public void LoadShaderSourceFromFile(string filePath)
        {
            string v = File.ReadAllText(filePath);
            ShaderSource = v;
        }
    }


    public class VertexShader : ShaderBase
    {
        private const int ShaderType = 0x8B31;

        public override string ShaderSource
        {
            set => Gl.ShaderSource(shaderIdentifier, value);
        }

        public override void Compile()
        {
            Gl.CompileShader(shaderIdentifier);
        }

        public override void Init()
        {
            shaderIdentifier = Gl.CreateShader(ShaderType);
        }
    }

}
