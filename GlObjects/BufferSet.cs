using System.Runtime.InteropServices;

namespace GlBindings
{
    public struct BufferSet
    {
        public VertexBufferObject vbo;
        public VertexArrayObject vao;
        public ElementBufferObject ebo;
        public void InitializeBuffers(float[] vertices, int[] indices, DrawType dtype, BufferAttribute[] attributes)
        {
            vbo = new VertexBufferObject(BufferType.GL_ARRAY_BUFFER);
            vao = new VertexArrayObject();
            ebo = new ElementBufferObject();
            vao.Bind();
            vbo.Bind();
            vbo.BufferData(vertices, dtype);
            ebo.Bind();
            ebo.BufferData(indices, dtype);
            foreach (var attribute in attributes)
            {
                vao.AddAttribute(attribute.numberOfElements, attribute.type, false, Marshal.SizeOf((typeof(float))) * 5);
            }
        }
        public struct BufferAttribute
        {
            public int numberOfElements;
            public DataType type;
            public string name;

            public BufferAttribute(string name, DataType type, int numberOfElements)
            {
                this.type = type;
                this.numberOfElements = numberOfElements;
                this.name = name;
            }
        }
    }
}
