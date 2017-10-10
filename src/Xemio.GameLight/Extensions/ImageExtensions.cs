using System.Drawing;

namespace Xemio.GameLight.Extensions
{
    public static class ImageExtensions
    {
        public static Image CutOut(this Image self, RectangleF area)
        {
            var result = new Bitmap((int)area.Width, (int)area.Height);
            var graphics = Graphics.FromImage(result);

            graphics.DrawImage(self, 0, 0, area, GraphicsUnit.Pixel);

            return result;
        }
    }
}