using System.Drawing;
using System.Windows.Forms;
using Xemio.GameLight;
using Xemio.GameLight.Game;

namespace Xemio.GamesLight.Games.Pong.Scenes
{
    public class WinScene : Scene
    {
        private readonly int _points;

        public WinScene(int points)
        {
            this._points = points;
        }

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

            this.RenderPointLine();
            this.RenderRestartLine();
        }

        private void RenderPointLine()
        {
            var text = $"Du hast gesiegt und {this._points} Punkte erreicht!";

            var size = this.GraphicsDevice.MeasureString(text, PongFonts.Normal);
            var position = this.GraphicsDevice.GetScreenCenterFor(size);
            this.GraphicsDevice.DrawString(text, PongFonts.Normal, PongBrushes.Text, position);
        }

        private void RenderRestartLine()
        {
            var text = $"Drücke die Leertaste um ein neues Spiel zu starten!";

            var size = this.GraphicsDevice.MeasureString(text, PongFonts.Small);
            var position = this.GraphicsDevice.GetScreenCenterFor(size) + new SizeF(0, 40);
            this.GraphicsDevice.DrawString(text, PongFonts.Small, PongBrushes.Text, position);
        }
    }
}