using Xemio.GameLight.Input;
using Xemio.GameLight.Rendering;

namespace Xemio.GameLight.Game
{
    public abstract class Scene
    {
        public GraphicsDevice GraphicsDevice => XGL.Get<GraphicsDevice>();
        public InputManager InputManager => XGL.Get<InputManager>();

        public bool IsContentLoaded { get; private set; }

        internal void LoadContentIfNeeded()
        {
            if (this.IsContentLoaded)
                return;

            this.LoadContent();

            this.IsContentLoaded = true;
        }

        public virtual void LoadContent()
        {
            
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnLeave()
        {
            
        }

        public virtual void Tick(double elapsed)
        {
            
        }

        public virtual void Render()
        {
            
        }
    }
}