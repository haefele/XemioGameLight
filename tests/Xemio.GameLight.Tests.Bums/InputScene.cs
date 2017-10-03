using System.Drawing;
using System.Windows.Forms;
using Xemio.GameLight.Game;

namespace Xemio.GameLight.Tests.Bums
{
    public class InputScene : Scene
    {
        private SolidBrush _backgroundBrush;
        private RectangleF _backgroundRect;

        private string _message;
        private Font _font;
        private Brush _brush;
        private PointF _currentLocation;
        
        public override void LoadContent()
        {
            base.LoadContent();

            this._backgroundBrush = new SolidBrush(Color.Green);
            this._backgroundRect = new RectangleF(0, 0, this.GraphicsDevice.Width, this.GraphicsDevice.Height);

            this._message = "Hello";
            this._font = new Font("Arial", 20);
            this._brush = new SolidBrush(Color.White);
        }

        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);

            //PointF direction = new PointF(0, 0);

            //if (this.InputManager.Keyboard.IsPressed(Keys.W))
            //{
            //    direction = new PointF(direction.X, direction.Y - 1);
            //}

            //if (this.InputManager.Keyboard.IsPressed(Keys.A))
            //{
            //    direction = new PointF(direction.X - 1, direction.Y);
            //}

            //if (this.InputManager.Keyboard.IsPressed(Keys.S))
            //{
            //    direction = new PointF(direction.X, direction.Y + 1);
            //}

            //if (this.InputManager.Keyboard.IsPressed(Keys.D))
            //{
            //    direction = new PointF(direction.X + 1, direction.Y);
            //}

            //float speed = 200;

            //var xMovement = (direction.X * (float)elapsed * speed);
            //var yMovement = (direction.Y * (float)elapsed * speed);

            //this._currentLocation = new PointF(this._currentLocation.X + xMovement, this._currentLocation.Y + yMovement);

            this._currentLocation = this.InputManager.Mouse.MousePosition;
        }

        public override void Render()
        {
            this.GraphicsDevice.FillRectangle(this._backgroundBrush, this._backgroundRect);
            this.GraphicsDevice.DrawString(this._message, this._font, this._brush, this._currentLocation);
        }
    }
}