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
        private static Form _form;
        private static IList<IComponent> _components;

        public static T Get<T>() where T : IComponent
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public static bool IsConfigured => _form != null;

        public static void Run(Action<XGLConfiguration> configurationAction)
        {
            var configuration = new XGLConfiguration();
            configurationAction(configuration);
            Run(configuration);
        }

        public static void Run(XGLConfiguration configuration)
        {
            if (IsConfigured)
                return;
            
            configuration.Validate();

            _form = new Form
            {
                Width = configuration.BackBuffer.Width,
                Height = configuration.BackBuffer.Height,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Text = configuration.Title,
                ShowIcon = false
            };

            var gameLoop = new GameLoop(configuration.FramesPerSecond);
            var renderManager = new RenderManager(_form, configuration.BackBuffer, configuration.DefaultColor);
            var sceneManager = new SceneManager();

            gameLoop.Subscribe(sceneManager);
            gameLoop.Subscribe(renderManager);

            _components = new List<IComponent>
            {
                gameLoop,
                renderManager,
                renderManager.GraphicsDevice,
                sceneManager
            };

            sceneManager.CurrentScene = configuration.StartScene;

            gameLoop.Start();

            Application.Run(_form);
        }
    }

    public class XGLConfiguration
    {
        public int FramesPerSecond { get; set; }
        public Size BackBuffer { get; set; }
        public Scene StartScene { get; set; }
        public Color DefaultColor { get; set; }
        public string Title { get; set; }

        public void Validate()
        {
            if (this.FramesPerSecond <= 0)
                throw new Exception();
            if (this.StartScene == null)
                throw new Exception();
        }
    }
}
