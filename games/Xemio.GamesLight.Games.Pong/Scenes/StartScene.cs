using System.Windows.Forms;
using Xemio.GameLight;
using Xemio.GameLight.Game;

namespace Xemio.GamesLight.Games.Pong.Scenes
{
    public class StartScene : Scene
    {
        public override void Tick(double elapsed)
        {
            base.Tick(elapsed);

            if (this.InputManager.Keyboard.GotPressed(Keys.Space))
            {
                XGL.Get<SceneManager>().CurrentScene = new PongScene();
            }
        }

        public override void Render()
        {
            base.Render();

            var text = $"Drücke die Leertaste um das Spiel zu starten!";

            var size = this.GraphicsDevice.MeasureString(text, PongFonts.Normal);
            var position = this.GraphicsDevice.GetScreenCenterFor(size);
            this.GraphicsDevice.DrawString(text, PongFonts.Normal, PongBrushes.Text, position);
        }
    }
}