namespace GlBindings
{
    public class ShaderProgram
    {
        internal uint programIdentifier;
        public static ShaderProgram ActiveProgram { get; private set; }

        public void AttachShader(Shader shader)
        {
            Gl.AttachShader(programIdentifier, shader.shaderIdentifier);
        }
        public void AttachShader(params Shader[] shaders)
        {
            foreach (Shader shader in shaders)
            {
                AttachShader(shader);
            }
        }
        public void Link()
        {
            Gl.LinkProgram(programIdentifier);
        }
        public ShaderProgram()
        {
            programIdentifier = Gl.CreateProgram();
        }
        public void Bind()
        {
            ActiveProgram = this;
            Gl.UseProgram(programIdentifier);
        }
    }
}
