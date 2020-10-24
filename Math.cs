using System.Numerics;
using System.Text;

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
    public static class MatrixProjections
    {
        public static float[,] GetOrthoProjection(float width, float height, float zNear, float zFar)
        {
            return Matrix4x4.CreateOrthographic(width, height, zNear, zFar).ExtractToFloatArray();
        }
        public static float[,] Transform(float[,] matArray, Vector3D vec)
        {
            Matrix4x4 mat = matArray.FillMatrix4x4();
            Matrix4x4 trans = Matrix4x4.CreateTranslation(vec.x, vec.y, vec.z);
            return Matrix4x4.Multiply(mat, trans).ExtractToFloatArray();
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
        public static string ToStringPretty(this float[,] arr)
        {
            StringBuilder sb = new StringBuilder();
            int maximumWidth = 0;
            foreach (float entry in arr)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (maximumWidth < entry.ToString().Length)
                    {
                        maximumWidth = entry.ToString().Length;
                    }
                }
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    _ = arr[j, i] >= 0 ? sb.Append(' ').Append(arr[j, i]) : sb.Append(arr[j, i]);
                    for (int x = 0; x < maximumWidth - arr[j, i].ToString().Length; x++)
                    {
                        _ = sb.Append(' ');
                    }
                    _ = arr[j,i] < 0 ? sb.Append(' ') : null;
                }
                _ = sb.Append("\n");
            }
            return sb.ToString();
        }
        public static Matrix4x4 FillMatrix4x4(this float[,] arr)
        {
            return new Matrix4x4
            {
                M11 = arr[0, 0],
                M12 = arr[0, 1],
                M13 = arr[0, 2],
                M14 = arr[0, 3],


                M21 = arr[1, 0],
                M22 = arr[1, 1],
                M23 = arr[1, 2],
                M24 = arr[1, 3],


                M31 = arr[2, 0],
                M32 = arr[2, 1],
                M33 = arr[2, 2],
                M34 = arr[2, 3],


                M41 = arr[3, 0],
                M42 = arr[3, 1],
                M43 = arr[3, 2],
                M44 = arr[3, 3]
            };
        }
    }
}
