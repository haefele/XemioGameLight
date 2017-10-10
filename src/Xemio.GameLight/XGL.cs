using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game;
using Xemio.GameLight.Game.Randomization;
using Xemio.GameLight.Input;
using Xemio.GameLight.Rendering;
using Xemio.GameLight.Threading;

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

        public static void CreateWinFormsWindow(Action<XGLConfiguration> configurationAction)
        {
            var configuration = new XGLConfiguration();
            configurationAction(configuration);
            CreateWinFormsWindow(configuration);
        }
        public static void CreateWinFormsWindow(XGLConfiguration configuration)
        {
            var form = new Form
            {
                ClientSize = configuration.BackBuffer,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Text = configuration.Title,
                ShowIcon = false,
                KeyPreview = true,
            };
            var panel = new Panel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top| AnchorStyles.Right| AnchorStyles.Bottom,
                Margin = new Padding(0),
                Width = configuration.BackBuffer.Width,
                Height = configuration.BackBuffer.Height
            };
            form.Controls.Add(panel);
            form.Activated += (s, e) =>
            {
                panel.Focus();
            };
            
            configuration.Control = panel;

            Run(configuration);

            Cursor.Hide();

            Application.Run(form);
        }

        public static void EmbedInControl(Action<XGLConfiguration> configurationAction)
        {
            var configuration = new XGLConfiguration();
            configurationAction(configuration);
            EmbedInControl(configuration);
        }
        public static void EmbedInControl(XGLConfiguration configuration)
        {
            Run(configuration);
        }

        private static void Run(XGLConfiguration configuration)
        {
            if (IsConfigured)
                return;

            configuration.Validate();

            var threadInvoker = new ThreadInvoker(configuration.Control);
            var gameLoop = new GameLoop(configuration.FramesPerSecond, threadInvoker);
            var inputManager = new InputManager(configuration.Control, configuration.BackBuffer);
            var renderManager = new RenderManager(configuration.Control, configuration.BackBuffer, configuration.DefaultColor);
            var sceneManager = new SceneManager();

            gameLoop.Subscribe(sceneManager);
            gameLoop.Subscribe(renderManager);
            gameLoop.Subscribe(inputManager);

            _components = new List<IComponent>(configuration.Components)
            {
                gameLoop,
                renderManager,
                renderManager.GraphicsDevice,
                sceneManager,
                threadInvoker,
                inputManager,
                new LocalRandom(),
            };

            sceneManager.CurrentScene = configuration.StartScene;
            
            IsConfigured = true;

            gameLoop.Start();
        }
    }
}
