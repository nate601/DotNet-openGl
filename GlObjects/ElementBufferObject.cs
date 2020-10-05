using System.Runtime.InteropServices;

namespace GlBindings
{
    public class ElementBufferObject
    {
        private readonly uint identifier;
        public static ElementBufferObject currentlyBound;

        public static explicit operator uint(ElementBufferObject ebo)
        {
            return ebo.identifier;
        }
        public ElementBufferObject()
        {
            identifier = Gl.GenBuffers();
        }
        public void Bind()
        {
            Gl.BindBuffer(0x8893, identifier);
            currentlyBound = this;
        }
        public bool IsCurrentlyBound()
        {
            return currentlyBound == this;
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

