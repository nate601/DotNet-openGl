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
        public bool TryLink(out string infoLog)
        {
            Link();
            if(LastLinkingSuccessful())
            {
                infoLog = "";
                return true;
            }
            infoLog = GetInfoLog();
            return false;
        }
        private bool LastLinkingSuccessful()
        {
            const int GL_LINK_STATUS = 0x8B82;
            return Gl.GetProgram(this, GL_LINK_STATUS) == 1;
        }
        private string GetInfoLog()
        {
            return Gl.GetProgramInfoLog(this);
        }
        private int GetUniformLocation(string uniformName)
        {
            return Gl.GetUniformLocation(this, uniformName);
        }
        public void SetUniform(string locationName, int val)
        {
            Gl.SetUniform(GetUniformLocation(locationName), val);
        }
        public void SetUniform(string locationName, bool val)
        {
            Gl.SetUniform(GetUniformLocation(locationName), val);
        }
        public void SetUniform(string locationName, float val)
        {
            Gl.SetUniform(GetUniformLocation(locationName), val);
        }
        public static implicit operator int(ShaderProgram pgm)
        {
            return (int)pgm.programIdentifier;
        }
        public static implicit operator uint(ShaderProgram pgm)
        {
            return pgm.programIdentifier;
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
