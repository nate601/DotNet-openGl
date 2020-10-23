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
        public static float[,] ExtractToFloatArray(this Matrix4x4 mat)
        {
            float[,] retVal = new float[4, 4];
            retVal[0, 0] = mat.M11;
            retVal[0, 1] = mat.M12;
            retVal[0, 2] = mat.M13;
            retVal[0, 3] = mat.M14;

            retVal[1, 0] = mat.M21;
            retVal[1, 1] = mat.M22;
            retVal[1, 2] = mat.M23;
            retVal[1, 3] = mat.M24;

            retVal[2, 0] = mat.M31;
            retVal[2, 1] = mat.M32;
            retVal[2, 2] = mat.M33;
            retVal[2, 3] = mat.M34;

            retVal[3, 0] = mat.M41;
            retVal[3, 1] = mat.M42;
            retVal[3, 2] = mat.M43;
            retVal[3, 3] = mat.M44;

            return retVal;
        }
    }
}
