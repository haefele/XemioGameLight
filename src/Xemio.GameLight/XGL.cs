using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game;
using Xemio.GameLight.Rendering;

namespace Xemio.GameLight
{
    public static class XGL
    {
        private static IList<IComponent> _components;

        public static T Get<T>() where T : IComponent
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public static bool IsConfigured { get; private set; }

        public static void Initialize(Action<XGLConfiguration> configurationAction)
        {
            var configuration = new XGLConfiguration();
            configurationAction(configuration);
            Initialize(configuration);
        }

        public static void Initialize(XGLConfiguration configuration)
        {
            if (IsConfigured)
                return;
            
            configuration.Validate();

            var gameLoop = new GameLoop(configuration.FramesPerSecond);
            var renderManager = new RenderManager(configuration.Control, configuration.BackBuffer);
            var sceneManager = new SceneManager();
            sceneManager.CurrentScene = configuration.StartScene;

            gameLoop.Subscribe(sceneManager);
            gameLoop.Subscribe(renderManager);

            _components = new List<IComponent>
            {
                gameLoop,
                renderManager,
                renderManager.GraphicsDevice,
                sceneManager
            };

            gameLoop.Run();

            IsConfigured = true;
        }
    }

    public class XGLConfiguration
    {
        public int FramesPerSecond { get; set; }
        public Size BackBuffer { get; set; }
        public Control Control { get; set; }
        public Scene StartScene { get; set; }

        public void Validate()
        {
            if (this.Control == null)
                throw new Exception();

            if (this.StartScene == null)
                throw new Exception();
        }
    }
}
