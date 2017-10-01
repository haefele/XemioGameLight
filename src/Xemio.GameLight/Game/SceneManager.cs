using Xemio.GameLight.Components;

namespace Xemio.GameLight.Game
{
    public class SceneManager : IComponent, IGameLoopSubscriber
    {
        private Scene _currentScene;

        public Scene CurrentScene
        {
            get { return this._currentScene; }
            set
            {
                if (this._currentScene == value)
                    return;

                this._currentScene?.OnLeave();
                this._currentScene = value;
                this._currentScene?.LoadContentIfNeeded();
                this._currentScene?.OnEnter();
            }
        }

        public void Tick(double elapsed)
        {
            this.CurrentScene?.Tick(elapsed);
        }

        public void Render()
        {
            this.CurrentScene?.Render();
        }
    }
}