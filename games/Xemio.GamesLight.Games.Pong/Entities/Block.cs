using System;
using System.Drawing;
using Xemio.GameLight;
using Xemio.GameLight.Entities;
using Xemio.GameLight.Extensions;
using Xemio.GameLight.Game.Randomization;
using Xemio.GameLight.Rendering;
using Xemio.GamesLight.Games.Pong.Entities.Balls;

namespace Xemio.GamesLight.Games.Pong.Entities
{
    public class Block : Entity, IBallCollider, IBallBlocker, IGivePoints
    {
        private readonly Image _backgroundImageCutOut;

        public const int BorderThickness = 3;

        public Block(PointF location, SizeF size, Image backgroundImage)
        {
            this.Location = location;
            this.Size = size;
            
            var random = XGL.Get<IRandom>();

            if (backgroundImage.Width < this.Size.Width || backgroundImage.Height < this.Size.Height)
                throw new Exception("Background image too small.");

            float x = random.Next(0, backgroundImage.Width - (int)this.Size.Width);
            float y = random.Next(0, backgroundImage.Height - (int)this.Size.Height);

            var backgroundImagePart = new RectangleF(x, y, this.Size.Width, this.Size.Height);

            this._backgroundImageCutOut = backgroundImage.CutOut(backgroundImagePart);
        }

        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);
        }

        public override void Render()
        {
            var drawingRect = this.Rectangle.IndentBy(1);

            this.GraphicsDevice.DrawImage(this._backgroundImageCutOut, drawingRect);
            DrawBorder(this.GraphicsDevice, drawingRect);
        }

        public static void DrawBorder(GraphicsDevice graphicsDevice, RectangleF drawingRect)
        {
            var borderRect = drawingRect.IndentBy(BorderThickness / 2f);

            //Oben
            graphicsDevice.DrawLine(PongPens.BlockLightOutline, new PointF(drawingRect.Left, borderRect.Top), new PointF(drawingRect.Right, borderRect.Top));
            //Links
            graphicsDevice.DrawLine(PongPens.BlockLightOutline, new PointF(borderRect.Left, drawingRect.Top), new PointF(borderRect.Left, drawingRect.Bottom));
            //Rechts
            graphicsDevice.DrawLine(PongPens.BlockDarkOutline, new PointF(borderRect.Right, drawingRect.Top), new PointF(borderRect.Right, drawingRect.Bottom));
            //Unten
            graphicsDevice.DrawLine(PongPens.BlockDarkOutline, new PointF(drawingRect.Left, borderRect.Bottom), new PointF(drawingRect.Right, borderRect.Bottom));
        }

        public void OnCollide(Ball ball)
        {
            this.Environment.Remove(this);
        }

        public int Points => 100;
    }
}