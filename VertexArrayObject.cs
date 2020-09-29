using System.Runtime.InteropServices;

namespace GlBindings
{
    public class VertexArrayObject
    {

    }
    public class VertexBufferObject
    {
        private readonly BufferType bufType;
        public uint identifier;

        public VertexBufferObject(BufferType bufType)
        {
            identifier = Gl.GenBuffers();
            this.bufType = bufType;
        }
        public void Bind()
        {
            Gl.BindBuffer((int)bufType, identifier);
        }
        public void BufferData(float[] data, DrawType dType)
        {
            Gl.BufferData((int)bufType, Marshal.SizeOf(data), data, (int)dType);

        }
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
