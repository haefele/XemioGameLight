using Xemio.GameLight.Rendering;

namespace Xemio.GameLight.Game
{
    public abstract class Scene
    {
        public GraphicsDevice GraphicsDevice => XGL.Get<GraphicsDevice>();

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