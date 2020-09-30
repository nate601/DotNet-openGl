namespace GlBindings
{
    public class VertexArrayObject
    {
        public uint identifier;
        public static VertexArrayObject currentlyBound;
        public bool IsCurrentlyBound => currentlyBound == this;

        public VertexArrayObject()
        {
            identifier = Gl.GenVertexArrays();
        }
        public void EnableVertexAttribArray(uint index)
        {
            Gl.EnableVertexAttribArray(index);
        }
        public void Bind()
        {
            currentlyBound = this;
            Gl.BindVertexArray(identifier);
        }
        public void VertexAttribPointer(uint index, int size, DataType type, bool normalized, int stride)
        {
            if (!IsCurrentlyBound)
            {
                Bind();
            }
            Gl.VertexAttribPointer(index, size, (int)type, normalized, stride, default);
        }

    }

    public enum DataType
    {
        GL_FLOAT = 0x1406
    }

    public enum DrawType
    {
        GL_STREAM_DRAW = 0x88E0,
        GL_STATIC_DRAW = 0x88E4,
        GL_DYNAMIC_DRAW = 0x88E8
    }
    public enum BufferType
    {
        GL_ARRAY_BUFFER = 0x8892
    }
}
