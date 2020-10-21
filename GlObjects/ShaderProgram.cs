namespace GlBindings
{
    public class ShaderProgram : BaseBindable<ShaderProgram>
    {
        internal uint programIdentifier;

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
            if (LastLinkingSuccessful())
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
        public void SetUniform(string locationName, Vector2D val)
        {
            Gl.SetUniform(GetUniformLocation(locationName), new float[] { val.x, val.y, 0 });
        }
        public void SetUniform(string locationName, Vector3D val)
        {
            Gl.SetUniform(GetUniformLocation(locationName), new float[] { val.x, val.y, val.z });
        }
        public void SetUniform(string locationName, float[,] val)
        {
            float[] flattenedArray = new float[val.GetLength(1) * val.GetLength(0)];
            for (int i = 0; i < val.GetLength(1); i++)
            {
                for (int j = 0; j < val.GetLength(0); j++)
                {
                    flattenedArray[(val.GetLength(0) * j) + i] = val[j, i];
                }
            }
            Gl.SetUniform(GetUniformLocation(locationName), flattenedArray, true);
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
        public override void Bind()
        {
            CurrentlyBound = this;
            Gl.UseProgram(programIdentifier);
        }
    }
}
