using System.Runtime.InteropServices;

namespace GlBindings
{
    public class ElementBufferObject : BaseBindable<ElementBufferObject>
    {
        private readonly uint identifier;

        public static explicit operator uint(ElementBufferObject ebo)
        {
            return ebo.identifier;
        }
        public ElementBufferObject()
        {
            identifier = Gl.GenBuffers();
        }
        public override void Bind()
        {
            Gl.BindBuffer(0x8893, identifier);
            CurrentlyBound = this;
        }
        public void BufferData(int[] indices, DrawType drawType)
        {
            if (!IsCurrentlyBound())
            {
                Bind();
            }

            Gl.BufferData(0x8893, indices.Length * Marshal.SizeOf(new int()), indices, (int)drawType);
        }
    }
}

