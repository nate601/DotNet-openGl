using System.Collections.Generic;
using GlBindings;

namespace openGlTest.EngineObjects
{
    public static class Renderer
    {
        public static List<ShaderProgram> shaderPrograms = new List<ShaderProgram>();
        public static List<Sprite> subscribeToRender = new List<Sprite>();
        public static readonly float[,] perspectiveProjection = MatrixProjections.GetPerspectiveProjection(45, 640, 480, 0.1f, 100);
        public static readonly float[,] orthoProjection = MatrixProjections.GetOrthoProjection(16,16,  0, 5);
        public static void EndScene()
        {

        }
        public static void Render(Sprite renderObject, Camera camera)
        {
            renderObject.shader.Bind();
            renderObject.tex.SetActiveTexture(0);
            renderObject.buffers.vao.Bind();
            renderObject.shader.SetUniform("model", renderObject.transform.GetModelMatrix());
            renderObject.shader.SetUniform("view", camera.transform.GetModelMatrix());
            /* renderObject.shader.SetUniform("projection", perspectiveProjection); */
            renderObject.shader.SetUniform("projection", MatrixProjections.GetOrthoProjection(640f / 480f, 1, 0, 5));
            Gl.DrawElements(0x004, 6, 0x1405, 0);
        }
        public static void Update()
        {
            foreach (var entry in subscribeToRender)
            {
                Render(entry, Camera.mainCamera);
            }
        }
    }
}
