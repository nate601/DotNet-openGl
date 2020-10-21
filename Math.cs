using System.Numerics;

namespace GlBindings
{
    public struct Vector2D
    {
        public readonly float x, y;
        public static implicit operator Vector2(Vector2D vec)
        {
            return new Vector2(vec.x, vec.y);
        }
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2 ToNumerics()
        {
            return new Vector2(x, y);
        }
    }
    public struct Vector3D
    {
        public readonly float x, y, z;
        public static implicit operator Vector3(Vector3D vec)
        {
            return new Vector3(vec.x, vec.y, vec.z);
        }
        public Vector3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3 ToNumerics()
        {
            return new Vector3(x, y, z);
        }
    }
    public static class MathExtensions
    {
    }
}
