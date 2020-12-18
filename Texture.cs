using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace GlBindings
{
    public class Texture : BaseBindable<Texture>
    {
        private const int GL_TEXTURE_2D = 0x0DE1;
        private const int GL_TEXTURE_MIN_FILTER = 0x2801;
        private const int GL_LINEAR = 0x2600;
        private const int GL_TEXTURE_MAG_FILTER = 0x2800;

        private readonly int identifier;
        public readonly string textureName;
        public override void Bind()
        {
            CurrentlyBound = this;
            Gl.BindTexture(GL_TEXTURE_2D, identifier);

        }
        public Texture(string textureName)
        {
            identifier = Gl.GenTextures();
            this.textureName = textureName;
        }
        private byte[] GetTextureRGBData(Bitmap image)
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
        private byte[] GetTextureRGBData(string image)
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
            Gl.TexParameter(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
            Gl.TexParameter(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            Gl.TexImage2D(GL_TEXTURE_2D, 0, 0x1907, width, height, 0, 0x1907, 0x1401, dataPointer);
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
            Gl.GenerateMipmap(GL_TEXTURE_2D);
        }
        public void SetActiveTexture(int textureId)
        {
            Gl.ActiveTexture(textureId);
            Bind();
        }
    }
    public static class TextureManager
    {
        public static List<Texture> textures = new List<Texture>();
        public static Texture GetTexture(string textureName)
        {
            if (textures.Any((x) => x.textureName == textureName))
            {
                return textures.First(x => x.textureName == textureName);
            }
            var newTex = new Texture(textureName);
            try
            {
                newTex.SetTextureData(textureName);
                newTex.GenerateMipmap();
            }
            catch (Exception e)
            {
                throw new Exception($"No texture file found for {textureName}" + e.Message);
            }
            textures.Add(newTex);
            return newTex;
        }
    }
}
