using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Xemio.GameLight.Components;

namespace Xemio.GameLight.Rendering
{
    public class GraphicsDevice : IComponent
    {
        public int Width { get; }
        public int Height { get; }
        public Color DefaultColor { get; }

        internal Bitmap Bitmap { get; }
        internal Graphics BackBuffer { get; }

        public GraphicsDevice(int width, int height, Color defaultColor)
        {
            this.Width = width;
            this.Height = height;
            this.DefaultColor = defaultColor;

            this.Bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            this.BackBuffer = Graphics.FromImage(this.Bitmap);
            this.BackBuffer.InterpolationMode = InterpolationMode.NearestNeighbor;
            this.BackBuffer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            this.BackBuffer.SmoothingMode = SmoothingMode.HighSpeed;
            this.BackBuffer.CompositingQuality = CompositingQuality.AssumeLinear;

            this.Clear();
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            this.BackBuffer.DrawLine(pen, pt1, pt2);
        }

        public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            this.BackBuffer.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        public void DrawRectangle(Pen pen, RectangleF rect)
        {
            this.BackBuffer.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            this.BackBuffer.DrawEllipse(pen, rect);
        }

        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            this.BackBuffer.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        public void FillRectangle(Brush brush, RectangleF rect)
        {
            this.BackBuffer.FillRectangle(brush, rect);
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            this.BackBuffer.FillEllipse(brush, rect);
        }

        public void FillPie(Brush brush, RectangleF rect, float startAngle, float sweepAngle)
        {
            this.BackBuffer.FillPie(brush, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
        }

        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            this.BackBuffer.DrawString(s, font, brush, point);
        }

        public SizeF MeasureString(string text, Font font)
        {
            return this.BackBuffer.MeasureString(text, font);
        }

        public void DrawImage(Image image, RectangleF rect)
        {
            this.BackBuffer.DrawImage(image, rect);
        }

        public void Clear()
        {
            this.BackBuffer.Clear(this.DefaultColor);
        }
    }
}