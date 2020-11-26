using System.Collections.Generic;
using GlBindings;

namespace openGlTest.EngineObjects
{
    public class RenderObject
    {
        public Transform transform;
    }
    public class Sprite
    {
        public Texture tex;

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
