using System.Collections.Generic;
using GlBindings;

namespace openGlTest.EngineObjects
{
    public static class Renderer
    {
        public static List<ShaderProgram> shaderPrograms = new List<ShaderProgram>();
        public static List<Sprite> subscribeToRender = new List<Sprite>();
        public static void EndScene()
        {

        }
        public static void Render(Sprite renderObject, Camera camera)
        {
            var shaderProgram = renderObject.shader;
            shaderProgram.Bind();
            renderObject.tex.SetActiveTexture(0);
            renderObject.buffers.vao.Bind();
            /* Console.WriteLine(renderObject.transform.GetModelMatrix().ToStringPretty()); */
            /* Console.WriteLine(camera.transform.GetModelMatrix().ToStringPretty()); */
            shaderProgram.SetUniform("model", renderObject.transform.GetModelMatrix());
            shaderProgram.SetUniform("view", camera.transform.GetModelMatrix());
            shaderProgram.SetUniform("projection", MatrixProjections.GetPerspectiveProjection(45, 640, 480, 0.1f, 100));
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
