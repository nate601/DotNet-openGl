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
        private int GetUniformLocation(string uniformName)
        {
            return Gl.GetUniformLocation((int)this, uniformName);
        }
        public void SetUniform(string location, int val)
        {
            Gl.SetUniform(GetUniformLocation(location), val);
        }
        public void SetUniform(string location, bool val)
        {
            Gl.SetUniform(GetUniformLocation(location), val);
        }
        public void SetUniform(string location, float val)
        {
            Gl.SetUniform(GetUniformLocation(location), val);
        }
        public static explicit operator int(ShaderProgram pgm)
        {
            return (int)pgm.programIdentifier;
        }

        public ShaderProgram()
        {
            programIdentifier = Gl.CreateProgram();
        }
        public ShaderProgram(params Shader[] shaders)
        {
            programIdentifier = Gl.CreateProgram();
            AttachShader(shaders);
        }
        public void Bind()
        {
            ActiveProgram = this;
            Gl.UseProgram(programIdentifier);
        }
    }
}
