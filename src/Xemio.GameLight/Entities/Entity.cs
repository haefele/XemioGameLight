using System;
using System.Collections;
using System.Drawing;
using Xemio.GameLight.Input;
using Xemio.GameLight.Rendering;

namespace Xemio.GameLight.Entities
{
    public abstract class Entity
    {
        public EntityEnvironment Environment { get; set; }

        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public RectangleF Rectangle => new RectangleF(this.Location, this.Size);

        public GraphicsDevice GraphicsDevice => XGL.Get<GraphicsDevice>();
        public InputManager InputManager => XGL.Get<InputManager>();

        public virtual void Tick(double elapsed)
        {
        }

        public virtual void Render()
        {
        }
    }
}