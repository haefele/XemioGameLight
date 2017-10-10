using System;
using System.Diagnostics;
using System.Drawing;
using Xemio.GameLight.Entities;
using Xemio.GameLight.Extensions;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Entities.Balls;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public abstract class PowerUp : Entity, IBallCollider
    {
        private double _elapsed;

        public abstract TimeSpan AvailableFor { get; }
        public abstract bool IsPositive { get; }
        public abstract Image Icon { get; }

        public PowerUp()
        {
            this.Size = new SizeF(30, 30);
        }

        public void OnCollide(Ball ball)
        {
            this.WasActivated = true;
            this.ActivatePowerUp(ball);
            this.Environment.Remove(this);
        }

        public abstract void ActivatePowerUp(Ball ball);

        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);

            this._elapsed += elapsed;

            if (this._elapsed > this.AvailableFor.TotalSeconds)
            {
                this.Environment.Remove(this);
            }
        }

        public override void Render()
        {
            base.Render();

            var brush = this.IsPositive 
                ? PongBrushes.PositivePowerUp 
                : PongBrushes.NegativePowerUp;
            var pen = this.IsPositive
                ? PongPens.PositivePowerUp
                : PongPens.NegativePowerUp;
            
            this.GraphicsDevice.FillEllipse(brush, this.Rectangle);
            this.GraphicsDevice.DrawImage(this.Icon, this.Rectangle.IndentBy(2));

            var anglePercentage = this._elapsed / this.AvailableFor.TotalSeconds;

            var startAngle = 0;
            var sweepAngle = 360 - (float) anglePercentage * 360;

            this.GraphicsDevice.DrawArc(pen, this.Rectangle, startAngle, Math.Max(1, sweepAngle));
        }

        public bool WasActivated { get; private set; }

        public abstract string Text { get; }
    }
}