using System;
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
            this.BackBuffer.InterpolationMode = InterpolationMode.Default;
            this.BackBuffer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            this.BackBuffer.SmoothingMode = SmoothingMode.AntiAlias;
            this.BackBuffer.CompositingQuality = CompositingQuality.AssumeLinear;

            this.Clear();
        }

        public RectangleF GetScreenRectangle()
        {
            return new RectangleF(0, 0, this.Width, this.Height);
        }

        public PointF GetScreenCenter()
        {
            return new PointF(this.Width / 2, this.Height / 2);
        }

        public PointF GetScreenCenterFor(SizeF size)
        {
            return this.GetScreenCenter() - new SizeF(size.Width / 2, size.Height / 2);
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

        public void DrawImage(Image image, RectangleF rect, RectangleF sourceRect)
        {
            this.BackBuffer.DrawImage(image, rect, sourceRect, GraphicsUnit.Pixel);
        }

        public void Clear()
        {
            this.BackBuffer.Clear(this.DefaultColor);
        }
    }
}