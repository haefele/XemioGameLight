using System;
using System.Drawing;
using Xemio.GameLight;
using Xemio.GameLight.Rendering;

namespace Xemio.GamesLight.Games.Pong.Elements
{
    public class TextWithIcon
    {
        public PointF Location { get; set; }
        public Image Icon { get; set; }
        public string Text { get; set; }
        public bool Visible { get; set; } = true;

        public GraphicsDevice GraphicsDevice => XGL.Get<GraphicsDevice>();
        

        public void Render()
        {
            if (this.Visible == false)
                return;

            if (this.Icon != null)
                this.GraphicsDevice.DrawImage(this.Icon, new RectangleF(this.Location, new SizeF(this.Icon.Width, this.Icon.Height)));

            if (string.IsNullOrWhiteSpace(this.Text) == false)
                this.GraphicsDevice.DrawString(this.Text, PongFonts.Normal, PongBrushes.Text, new PointF(this.Location.X + 30, this.Location.Y));
        }
    }

    public class TemporaryText
    {
        private string _text;
        private TimeSpan _duration;
        private double _elapsed;
        
        public GraphicsDevice GraphicsDevice => XGL.Get<GraphicsDevice>();

        public void ShowText(string text, TimeSpan duration)
        {
            this._elapsed = 0;
            this._text = text;
            this._duration = duration;
        }

        public void Tick(double elapsed)
        {
            this._elapsed += elapsed;

            if (this._elapsed > this._duration.TotalSeconds)
            {
                this._text = null;
                this._duration = TimeSpan.Zero;
            }
        }

        public void Render()
        {
            if (string.IsNullOrWhiteSpace(this._text))
                return;

            var size = this.GraphicsDevice.MeasureString(this._text, PongFonts.Normal);
            var location = this.GraphicsDevice.GetScreenCenterFor(size);

            this.GraphicsDevice.DrawString(this._text, PongFonts.Normal, PongBrushes.Text, location);
        }
    }
}