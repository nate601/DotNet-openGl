using System.Collections.Generic;
using GlBindings;

namespace openGlTest.EngineObjects
{
    public class RenderObject
    {
        public Transform transform;
    }
    public class Sprite : RenderObject
    {
        public Texture tex;
        public BufferSet buffers;
        public ShaderProgram shader;

        public Sprite(string spriteName)
        {
            tex = new Texture();
            tex.GetTextureRGBData(spriteName);

            BufferSet bufferSet = new BufferSet();

            BufferSet.BufferAttribute[] bufferAttributes = new BufferSet.BufferAttribute[2];
            bufferAttributes[0] = new BufferSet.BufferAttribute("Vertex Position", DataType.GL_FLOAT, 3);
            bufferAttributes[1] = new BufferSet.BufferAttribute("Texture Mapping", DataType.GL_FLOAT, 2);

            bufferSet.InitializeBuffers(Primatives.Quad.vertices, Primatives.Quad.indices, DrawType.GL_STATIC_DRAW, bufferAttributes);
        }

    }
    public class Primatives
    {
        public record Primative(float[] vertices, int[] indices);
        public static Primative Quad = new Primative(
            new float[]{
            //position location1   texture location2
             0.5f,  0.5f, 0.0f,  1.0f, 1.0f, // top    right
             0.5f, -0.5f, 0.0f,  1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f,  0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f,  0.0f, 1.0f  // top    left
            },
            new int[]{
             0, 1, 3,
             1, 2, 3
            }
            );
    }

    public class Transform
    {
        public Vector3D position;
    }
    public class Camera
    {
        public static Camera mainCamera;
        public Transform transform;
        public Camera()
        {
            if (mainCamera is null)
            {
                mainCamera = this;
            }
        }
    }
    public static class Renderer
    {
        public static List<ShaderProgram> shaderPrograms = new List<ShaderProgram>();
        public static void EndScene()
        {

        }
        public static void Render(int shaderProgramIndex = 0)
        {
            var shaderProgram = shaderPrograms[shaderProgramIndex];

        }
    }
    public static class ResourceManager
    {

    }

}
