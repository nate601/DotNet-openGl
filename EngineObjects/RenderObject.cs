using System;
using System.Numerics;
using GlBindings;

namespace openGlTest.EngineObjects
{
    public abstract class RenderObject
    {
        public Transform transform;
        public abstract void Render(Camera camera, float[,] projectionMatrix);

    }
    public class Sprite : RenderObject
    {
        public Texture tex;
        public BufferSet buffers;
        public ShaderProgram shader;

        public Sprite(Texture tex, ShaderProgram shader)
        {
            this.tex = tex;
            this.shader = shader;
            transform = new Transform();

            BufferSet.BufferAttribute[] bufferAttributes = new BufferSet.BufferAttribute[2];
            bufferAttributes[0] = new BufferSet.BufferAttribute("Vertex Position", DataType.GL_FLOAT, 3);
            bufferAttributes[1] = new BufferSet.BufferAttribute("Texture Mapping", DataType.GL_FLOAT, 2);

            buffers.InitializeBuffers(Primatives.Quad.vertices, Primatives.Quad.indices, DrawType.GL_STATIC_DRAW, bufferAttributes);
        }

        public override void Render(Camera camera = null, float[,] projectionMatrix = null)
        {
            if (camera is null)
            {
                if (Camera.mainCamera != null)
                {
                    camera = Camera.mainCamera;
                }
                else
                {
                    throw new Exception("No camera set!");
                }
            }

            if (projectionMatrix is null)
            {
                projectionMatrix = MatrixProjections.GetOrthoProjection(640f/480f, 1, 0, 5);
            }

            shader.Bind();
            tex.SetActiveTexture(0);
            buffers.vao.Bind();
            shader.SetUniform("model", transform.GetModelMatrix() );
            shader.SetUniform("view", camera.transform.GetModelMatrix());
            shader.SetUniform("projection", projectionMatrix);
            Gl.DrawElements(0x004, 6, 0x1405, 0);
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
        public Vector3 position;
        public float[,] GetModelMatrix()
        {
            return MatrixProjections.Translation(position);
        }
        public Transform()
        {
            position = new Vector3(0, 0, 0);
        }
    }
    public class Camera
    {
        public static Camera mainCamera;
        public Transform transform;
        public Camera()
        {
            if (mainCamera == null)
            {
                mainCamera = this;
            }
            transform = new Transform();
        }
    }
    public class CameraControls
    {
        public Camera cam;
        public float movementSpeed;
        public float multiplier = 1;

        public CameraControls(Camera cam, float movementSpeed)
        {
            this.cam = cam;
            this.movementSpeed = movementSpeed;
            Program.Update += Update;
        }

        public void Update(float deltaTime)
        {
            var newMovement = new Vector3(0, 0, 0);
            if (InputManager.GetKeyDown(340))  // LEFT SHIFT
            {
                multiplier = 2.8f;
            }
            else
            {
                multiplier = 1.8f;
            }
            if (InputManager.GetKeyDown(87)) // W
            {
                newMovement -= new Vector3(0, movementSpeed * deltaTime * multiplier, 0);
            }
            if (InputManager.GetKeyDown(65)) // A
            {
                newMovement += new Vector3(movementSpeed * deltaTime * multiplier, 0, 0);
            }
            if (InputManager.GetKeyDown(68)) // D
            {
                newMovement -= new Vector3(movementSpeed * deltaTime * multiplier, 0, 0);
            }
            if (InputManager.GetKeyDown(83)) // S
            {
                newMovement += new Vector3(0, movementSpeed * deltaTime * multiplier, 0);
            }
            if (InputManager.GetKeyDown(81)) // Q
            {
                newMovement += new Vector3(0, 0, movementSpeed * deltaTime * multiplier);
            }
            if (InputManager.GetKeyDown(90)) // Z
            {
                newMovement -= new Vector3(0, 0, movementSpeed * deltaTime * multiplier);
            }
            cam.transform.position = cam.transform.position + newMovement;

        }
    }
}
