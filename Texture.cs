using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace GlBindings
{
    public class Texture : BaseBindable<Texture>
    {
        private readonly int identifier;
        public override void Bind()
        {
            CurrentlyBound = this;
            Gl.BindTexture(0x0DE1, identifier);

        }
        public Texture()
        {
            identifier = Gl.GenTextures();
        }
        public byte[] GetTextureRGBData(Bitmap image)
        {
            byte[] texData = new byte[image.Width * image.Height * 3];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color currentPixelColor = image.GetPixel(x, y);
                    texData[(3 * ((image.Width * x) + y)) + 0] = currentPixelColor.R;
                    texData[(3 * ((image.Width * x) + y)) + 1] = currentPixelColor.G;
                    texData[(3 * ((image.Width * x) + y)) + 2] = currentPixelColor.B;
                }
            }
            return texData;
        }
        public byte[] GetTextureRGBData(string image)
        {
            using Bitmap bp = (Bitmap)Image.FromFile(image);
            return GetTextureRGBData(bp);
        }
        private Bitmap GetBitmapFromFile(string imageName)
        {
            return (Bitmap)Image.FromFile(imageName);
        }
        private bool TryGetBitmapFromFile(string imageName, out Bitmap bp)
        {
            if (File.Exists(imageName))
            {
                bp = (Bitmap)Image.FromFile(imageName);
                return bp != null;
            }
            bp = null;
            return false;
        }
        public void SetTextureData(int width, int height, in byte[] data)
        {
            Bind();
            IntPtr dataPointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * data.Length);
            Marshal.Copy(data, 0, dataPointer, data.Length);
            Gl.TexImage2D(0x0DE1, 0, 0x1907, width, height, 0, 0x1907, 0x1401, dataPointer);
            Marshal.FreeHGlobal(dataPointer);
        }
        public void SetTextureData(Bitmap bp)
        {
            SetTextureData(bp.Width, bp.Height, GetTextureRGBData(bp));
        }
        public void SetTextureData(string image)
        {
            using Bitmap bp = GetBitmapFromFile(image);
            SetTextureData(bp);
        }
        public void GenerateMipmap()
        {
            Bind();
            Gl.GenerateMipmap(0x0DE1);
        }
        public void SetActiveTexture(int textureId)
        {
            Gl.ActiveTexture(textureId);
            Bind();
        }

    }
}
