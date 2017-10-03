using System.Drawing;
using System.Windows.Forms;

namespace Xemio.GameLight.Input
{
    public class MouseAdapter
    {
        private readonly Control _control;
        private readonly Size _backBufferSize;

        public MouseAdapter(Control control, Size backBufferSize)
        {
            this._control = control;
            this._backBufferSize = backBufferSize;

            control.MouseMove += this.ControlOnMouseMove;
        }

        public PointF MousePosition { get; private set; }

        private void ControlOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var xRatio = this._control.Width / (float)this._backBufferSize.Width;
            var yRatio = this._control.Height / (float)this._backBufferSize.Height;

            this.MousePosition = new PointF(mouseEventArgs.X / xRatio, mouseEventArgs.Y / yRatio);
        }
    }
}