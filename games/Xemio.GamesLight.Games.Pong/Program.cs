using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xemio.GameLight;
using Xemio.GamesLight.Games.Pong.Level;
using Xemio.GamesLight.Games.Pong.PowerUps;
using Xemio.GamesLight.Games.Pong.Scenes;

namespace Xemio.GamesLight.Games.Pong
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XGL.CreateWinFormsWindow(f =>
            {
                var backgroundImageProvider = new RandomImageProvider(".\\Resources\\BackgroundImages\\");
                var imageProvider = new BackgroundImageProvider(backgroundImageProvider);
                var regularBlockGenerator = new RegularBlockGenerator(imageProvider);
                var iconProvider = new FileBasedIconProvider(".\\Resources\\Icons\\");
                var powerUpManager = new PowerUpManager();

                f.Components.Add(powerUpManager);
                f.Components.Add(iconProvider);
                f.Components.Add(imageProvider);
                f.Components.Add(regularBlockGenerator);

                f.Title = "Pong";
                f.StartScene = new StartScene();
            });
        }
    }
}
