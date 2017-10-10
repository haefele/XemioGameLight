using System.Drawing;

namespace Xemio.GameLight.Extensions
{
    public static class RectangleExtensions
    {
        public static RectangleF IndentBy(this RectangleF self, float indentBy)
        {
            return new RectangleF(self.X + indentBy, self.Y + indentBy, self.Width - indentBy * 2, self.Height - indentBy * 2);
        }
    }
}