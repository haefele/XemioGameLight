using System.Drawing;
using System.Windows.Forms;
using Xemio.GameLight.Game;

namespace Xemio.GameLight.Tests.Bums
{
    public class TestScene : Scene
    {
        private PointF _lineStartPoint;
        private PointF _lineEndPoint;
        private Pen _linePen;

        private Font _font;
        private string _message;
        private SolidBrush _messageBrush;
        private PointF _messageLocation;
        private double _elapsed;
        private bool _upwards;

        public override void LoadContent()
        {
            base.LoadContent();

            this._lineStartPoint = new PointF(0, 0);
            this._lineEndPoint = new PointF(0, this.GraphicsDevice.Height);
            this._linePen = new Pen(Color.Green);

            this._font = new Font("Arial", 40);
            this._message = "Hallo Welt";
            this._messageBrush = new SolidBrush(Color.White);
        }

        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);

            this._elapsed = elapsed;

            float newX = this._lineStartPoint.X + 100 * (float)elapsed;
            if (newX > this.GraphicsDevice.Width)
                newX = 0;

            this._lineStartPoint = new PointF(newX, this._lineStartPoint.Y);
            this._lineEndPoint = new PointF(newX, this._lineEndPoint.Y);

            SizeF messageSize = this.GraphicsDevice.MeasureString(this._message, this._font);
            this._messageLocation = new PointF(this.GraphicsDevice.Width / 2f - messageSize.Width / 2, this.GraphicsDevice.Height / 2f - messageSize.Height / 2);
            
            this._upwards = this.InputManager.Keyboard.GotPressed(Keys.W);
        }

        public override void Render()
        {
            base.Render();
            
            this.GraphicsDevice.DrawLine(this._linePen, this._lineStartPoint, this._lineEndPoint);

            this.GraphicsDevice.DrawString(this._elapsed.ToString(), this._font, this._messageBrush, new PointF(0, 0));
            this.GraphicsDevice.DrawString(this._message, this._font, this._messageBrush, this._messageLocation);

            if (this._upwards)
            {
                this.GraphicsDevice.DrawRectangle(this._linePen, new RectangleF(10, 10, 50, 50));
            }
        }
    }
}