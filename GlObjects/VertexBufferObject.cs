using System.Runtime.InteropServices;

namespace GlBindings
{
    public class VertexBufferObject : BaseBindable<VertexBufferObject>
    {
        private readonly BufferType bufType;
        private readonly uint identifier;
        public static explicit operator uint(VertexBufferObject vbo)
        {
            return vbo.identifier;
        }

        public VertexBufferObject(BufferType bufType)
        {
            identifier = Gl.GenBuffers();
            this.bufType = bufType;
        }
        public override void Bind()
        {
            Gl.BindBuffer((int)bufType, identifier);
        }
        public void BufferData(float[] data, DrawType dType)
        {
            Gl.BufferData((int)bufType, Marshal.SizeOf(typeof(float)) * data.Length, data, (int)dType);
        }
    }
}
