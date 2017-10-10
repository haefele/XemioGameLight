using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Xemio.GameLight.Entities;
using Xemio.GamesLight.Games.Pong.Entities.Balls;

namespace Xemio.GamesLight.Games.Pong.Entities
{
    public class Ball : Entity
    {
        private int _speed;

        public PointF Direction { get; set; }
        public int Speed
        {
            get { return this._speed; }
            set
            {
                if (value > this.MaxSpeed)
                    value = this.MaxSpeed;

                this._speed = value;
            }
        }
        public int MaxSpeed { get; set; }

        public Ball()
        {
            this.Size = new SizeF(20, 20);
            this.Location = this.GraphicsDevice.GetScreenCenter() - new SizeF(this.Size.Width / 2f, this.Size.Height / 2f);
            this.Direction = new PointF(0.2f, -1);
            this.MaxSpeed = 800;
            this.Speed = 300;
        }

        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);
            
            if (this.Location.X > this.GraphicsDevice.Width - this.Size.Width)
            {
                this.Direction = new PointF(-this.Direction.X, this.Direction.Y);
            }
            if (this.Location.X < 0)
            {
                this.Direction = new PointF(-this.Direction.X, this.Direction.Y);
            }
            if (this.Location.Y < 0)
            {
                this.Direction = new PointF(this.Direction.X, -this.Direction.Y);
            }

            if (this.Location.Y > this.GraphicsDevice.Height)
            {
                this.Environment.Remove(this);
                return;
            }

            var newLocation = new PointF(this.Location.X + this.Direction.X * this.Speed * (float)elapsed, this.Location.Y + this.Direction.Y * this.Speed * (float)elapsed);
            
            foreach (var otherEntity in this.Environment.Entities.Where(f => f != this).ToList())
            {
                var intersection = EntityHelper.WouldIntersectWith(this, newLocation, otherEntity);
                if (intersection != Intersection.None)
                {
                    if (otherEntity is IBallBlocker)
                    {
                        //Ball hits something from the left side
                        if (intersection == Intersection.Left)
                        {
                            this.Direction = new PointF(-1 * this.Direction.X, this.Direction.Y);
                        }
                        //Ball hits something from the right side
                        if (intersection == Intersection.Right)
                        {
                            this.Direction = new PointF(-1 * this.Direction.X, this.Direction.Y);
                        }
                        //Ball hits something from the top
                        if (intersection == Intersection.Top)
                        {
                            this.Direction = new PointF(this.Direction.X, -1 * this.Direction.Y);
                        }
                        //Ball hits something from the bottom
                        if (intersection == Intersection.Bottom)
                        {
                            this.Direction = new PointF(this.Direction.X, -1 * this.Direction.Y);
                        }
                    }

                    if (otherEntity is IBallCollider ballCollider)
                    {
                        ballCollider.OnCollide(this);
                    }
                }
            }

            this.Location = new PointF(this.Location.X + this.Direction.X * this.Speed * (float)elapsed, this.Location.Y + this.Direction.Y * this.Speed * (float)elapsed);
        }

        public override void Render()
        {
            this.GraphicsDevice.FillEllipse(PongBrushes.Ball, this.Rectangle);
            this.GraphicsDevice.DrawEllipse(PongPens.BallOutline, this.Rectangle);
        }
    }
}