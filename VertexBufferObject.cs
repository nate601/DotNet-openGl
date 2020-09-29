using System.Runtime.InteropServices;

namespace GlBindings
{
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
}
