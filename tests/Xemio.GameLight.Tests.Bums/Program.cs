using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xemio.GameLight.Game;

namespace Xemio.GameLight.Tests.Bums
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form
            {
                Width = 1280,
                Height = 720,
                FormBorderStyle = FormBorderStyle.FixedSingle
            };

            XGL.Initialize(f =>
            {
                f.Control = form;
                f.BackBuffer = new Size(1280, 720);
                f.FramesPerSecond = 60;
                f.StartScene = new TestScene();
            });

            Application.Run(form);
        }
    }

    public class TestScene : Scene
    {
        private int _x = 0;

        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);

            this._x++;

            if (this._x > this.GraphicsDevice.Width)
                this._x = 0;
        }

        public override void Render()
        {
            base.Render();

            this.GraphicsDevice.BackBuffer.DrawLine(new Pen(Color.Green), this._x, 0, this._x, this.GraphicsDevice.Height);
        }
    }
}
