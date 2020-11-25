using GlBindings;

namespace openGlTest.EngineObjects
{
    public class RenderObject
    {
        public Transform transform;
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
            if(mainCamera is null)
            {
                mainCamera = this;
            }
        }
    }
    public static class Renderer
    {
        
    }
}
