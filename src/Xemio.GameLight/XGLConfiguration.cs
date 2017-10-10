using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game;

namespace Xemio.GameLight
{
    public class XGLConfiguration
    {
        public int FramesPerSecond { get; set; }
        public Size BackBuffer { get; set; }
        public Scene StartScene { get; set; }
        public Color DefaultColor { get; set; }
        public string Title { get; set; }
        public Control Control { get; set; }
        public List<IComponent> Components { get; }

        public XGLConfiguration()
        {
            this.Components = new List<IComponent>();
            this.FramesPerSecond = 60;
            this.BackBuffer = new Size(1280, 720);
            this.DefaultColor = Color.CornflowerBlue;
        }

        public void Validate()
        {
            if (this.Control == null)
                throw new Exception();

            if (this.FramesPerSecond <= 0)
                throw new Exception();

            if (this.StartScene == null)
                throw new Exception();
        }
    }
}