namespace GlBindings
{
    public class ShaderProgram
    {
        internal uint programIdentifier;

        public void AttachShader(Shader shader)
        {
            Gl.AttachShader(programIdentifier, shader.shaderIdentifier);
        }
        public void Link()
        {
            Gl.LinkProgram(programIdentifier);
        }
        public ShaderProgram()
        {
            programIdentifier = Gl.CreateProgram();
        }
    }
}
