namespace GlBindings
{
    public class VertexArrayObject : BaseBindable<VertexArrayObject>
    {
        private readonly uint identifier;

        private uint currentAttributeCount;

        public static explicit operator uint(VertexArrayObject i)
        {
            return i.identifier;
        }

        public VertexArrayObject()
        {
            identifier = Gl.GenVertexArrays();
        }
        public void EnableVertexAttribArray(uint index)
        {
            Gl.EnableVertexAttribArray(index);
        }
        public override void Bind()
        {
            CurrentlyBound = this;
            Gl.BindVertexArray(identifier);
        }
        public void VertexAttribPointer(uint index, int size, DataType type, bool normalized, int stride)
        {
            Bind();
            Gl.VertexAttribPointer(index, size, (int)type, normalized, stride, default);
        }
        public uint AddAttribute(int size, DataType type, bool normalized, int stride)
        {
            Bind();
            VertexAttribPointer(currentAttributeCount, size, type, normalized, stride);
            EnableVertexAttribArray(currentAttributeCount);
            currentAttributeCount++;
            return currentAttributeCount - 1;

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
