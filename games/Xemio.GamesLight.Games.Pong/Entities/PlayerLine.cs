using System;
using System.Diagnostics;
using System.Drawing;
using Xemio.GameLight.Entities;
using Xemio.GameLight.Extensions;
using Xemio.GamesLight.Games.Pong.Entities.Balls;

namespace Xemio.GamesLight.Games.Pong.Entities
{
    public enum PlayerLineSize
    {
        Small,
        Normal,
        Big,
        Large,
    }

    public class PlayerLine : Entity, IBallCollider, IBallBlocker
    {
        private readonly SolidBrush _brush;

        private PlayerLineSize _lineSize;

        public PlayerLine()
        {
            this._brush = new SolidBrush(Color.Gray);

            this.LineSize = PlayerLineSize.Normal;
            this.Location = new PointF(this.GraphicsDevice.Width / 2 - this.Size.Width / 2, this.GraphicsDevice.Height - 70);
        }

        public override void Tick(double elapsed)
        {
            var locationX = this.InputManager.Mouse.MousePosition.X -  this.Size.Width / 2;
            this.Location = new PointF(locationX, this.Location.Y);
        }

        public override void Render()
        {
            this.GraphicsDevice.FillRectangle(this._brush, this.Rectangle);
            Block.DrawBorder(this.GraphicsDevice, this.Rectangle);
        }

        public PlayerLineSize LineSize
        {
            get { return this._lineSize; }
            private set
            {
                this._lineSize = value;

                switch (this._lineSize)
                {
                    case PlayerLineSize.Small:
                        this.Size = new SizeF(150, 20);
                        break;
                    case PlayerLineSize.Normal:
                        this.Size = new SizeF(200, 20);
                        break;
                    case PlayerLineSize.Big:
                        this.Size = new SizeF(250, 20);
                        break;
                    case PlayerLineSize.Large:
                        this.Size = new SizeF(300, 20);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void IncreaseSize()
        {
            this.ChangeSize(1);
        }

        public void ReduceSize()
        {
            this.ChangeSize(-1);
        }

        private void ChangeSize(int change)
        {
            var nextSize = (PlayerLineSize)(this.LineSize + change);

            if (Enum.IsDefined(typeof(PlayerLineSize), nextSize))
                this.LineSize = nextSize;
        }

        public void OnCollide(Ball ball)
        {
            ball.Speed = (int)(ball.Speed * 1.05);

            Debug.WriteLine($"Ballspeed: {ball.Speed}");

            this.UpdateDirectionOfBall(ball);
        }

        private void UpdateDirectionOfBall(Ball ball)
        {
            var ballCenter = ball.Location.X + ball.Size.Width / 2;
            var lineCenter = this.Location.X + this.Size.Width / 2;

            var ballRightOfLine = ballCenter > lineCenter;

            var distance = ballRightOfLine
                ? ballCenter - lineCenter
                : lineCenter - ballCenter;

            var fullPossibleDistance = this.Size.Width / 2;

            var percentage = distance / fullPossibleDistance;
            var angle = 45 * percentage + 90;

            var angleInRadians = this.AngleToRadians(angle);

            var x = Math.Cos(angleInRadians);
            var y = -Math.Sin(angleInRadians);

            if (ballRightOfLine)
            {
                x = -x;
            }

            ball.Direction = new PointF((float)x, (float)y);

            Debug.WriteLine($"Distance = {distance}");
            Debug.WriteLine($"Angle = {angle}");
            Debug.WriteLine($"X = {x}, Y = {y}");
        }

        private double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}