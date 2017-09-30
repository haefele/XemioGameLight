using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Xemio.GameLight.Components;

namespace Xemio.GameLight.Rendering
{
    public class GraphicsDevice : IComponent
    {
        public GraphicsDevice(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.Bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            this.BackBuffer = Graphics.FromImage(this.Bitmap);
            this.BackBuffer.InterpolationMode = InterpolationMode.NearestNeighbor;
            this.BackBuffer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            this.BackBuffer.SmoothingMode = SmoothingMode.HighSpeed;
            this.BackBuffer.CompositingQuality = CompositingQuality.AssumeLinear;

            this.Clear();
        }
        
        public int Width { get; }
        public int Height { get; }

        public Bitmap Bitmap { get; }
        public Graphics BackBuffer { get; }

        public void Clear()
        {
            this.BackBuffer.Clear(Color.Transparent);
        }
    }
}