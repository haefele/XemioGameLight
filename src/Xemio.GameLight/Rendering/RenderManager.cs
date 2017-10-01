using System;
using System.Drawing;
using System.Windows.Forms;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game;

namespace Xemio.GameLight.Rendering
{
    public class RenderManager : IComponent, IGameLoopSubscriber
    {
        private readonly Control _control;

        public RenderManager(Control control, Size backBufferSize, Color defaultColor)
        {
            this._control = control;

            this.GraphicsDevice = new GraphicsDevice(backBufferSize.Width, backBufferSize.Height, defaultColor);
        }

        public GraphicsDevice GraphicsDevice { get; }
        
        public void Tick(double elapsed)
        {
        }

        public void Render()
        {
            Graphics graphics = this._control.CreateGraphics();

            IntPtr hdc = graphics.GetHdc();
            IntPtr dc = Gdi.CreateCompatibleDC(hdc);
            IntPtr buffer = this.GraphicsDevice.Bitmap.GetHbitmap();

            Gdi.SelectObject(dc, buffer);

            Gdi.StretchBlt
            (
                hdc, 0, 0,
                this._control.Width,
                this._control.Height,
                dc,
                0, 0,
                this.GraphicsDevice.Width,
                this.GraphicsDevice.Height,
                Gdi.GdiRasterOperations.SRCCOPY
            );

            Gdi.DeleteObject(buffer);
            Gdi.DeleteObject(dc);

            graphics.ReleaseHdc(hdc);

            this.GraphicsDevice.Clear();
        }
    }
}