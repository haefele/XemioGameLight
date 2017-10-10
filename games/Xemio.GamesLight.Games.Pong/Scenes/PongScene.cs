using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLight;
using Xemio.GameLight.Entities;
using Xemio.GameLight.Extensions;
using Xemio.GameLight.Game;
using Xemio.GamesLight.Games.Pong.Elements;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Level;
using Xemio.GamesLight.Games.Pong.PowerUps;

namespace Xemio.GamesLight.Games.Pong.Scenes
{
    public class PongScene : Scene
    {
        private IBlockGenerator _blockGenerator;
        private IIconProvider _iconProvider;
        private PowerUpManager _powerUpManager;

        private readonly EntityEnvironment _environment;

        private TextWithIcon _points;
        private TextWithIcon _ballCount;
        private TextWithIcon _ballSpeed;
        private TemporaryText _tempText;

        public int BallCount { get; set; }
        public int Points { get; set; }
        public bool IsPaused { get; set; }

        public PongScene()
        {
            this._environment = new EntityEnvironment();
            this._environment.EntityAdded += this.EnvironmentOnEntityAdded;
            this._environment.EntityRemoved += this.EnvironmentOnEntityRemoved;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            this._blockGenerator = XGL.Get<IBlockGenerator>();
            this._iconProvider = XGL.Get<IIconProvider>();
            this._powerUpManager = XGL.Get<PowerUpManager>();
            
            this._points = new TextWithIcon
            {
                Icon = this._iconProvider.GetPointsIcon(),
                Text = "Points",
                Location = new PointF(10f, this.GraphicsDevice.Height - 42)
            };
            this._ballCount = new TextWithIcon
            {
                Icon = this._iconProvider.GetBallCountIcon(),
                Text = "Balls",
                Location = new PointF(120, this.GraphicsDevice.Height - 42)
            };
            this._ballSpeed = new TextWithIcon
            {
                Icon = this._iconProvider.GetBallSpeedIcon(),
                Text = "Speed",
                Location = new PointF(200, this.GraphicsDevice.Height - 42)
            };
            this._tempText = new TemporaryText();

            this._environment.Add(new Ball());
            this._environment.Add(new PlayerLine());

            var areaToFillWithBlocks = new RectangleF(0, 0, this.GraphicsDevice.Width, this.GraphicsDevice.Height / 3f);
            foreach (var block in this._blockGenerator.GenerateBlocks(areaToFillWithBlocks))
            {
                this._environment.Add(block);
            }
        }

        public override void Tick(double elapsed)
        {
            var togglePause = this.InputManager.Keyboard.GotPressed(Keys.Escape) || this.InputManager.Keyboard.GotPressed(Keys.Space);
            if (togglePause && this.BallCount > 0)
            {
                this.IsPaused = !this.IsPaused;
            }

            if (this.IsPaused == false)
            {
                this._environment.Tick(elapsed);

                var powerUp = this._powerUpManager.SpawnPowerUp(elapsed);
                if (powerUp != null)
                {
                    this._environment.Add(powerUp);
                }
            }

            this._points.Text = this.Points.ToString();
            this._ballCount.Text = this.BallCount.ToString();
            
            var allBalls = this._environment.Entities.OfType<Ball>().ToList();
            this._ballSpeed.Visible = allBalls.Any();
            if (allBalls.Any())
            {
                this._ballSpeed.Text = allBalls.Max(f => f.Speed).ToString();
            }

            this._tempText.Tick(elapsed);
        }

        public override void Render()
        {
            this.RenderTempText();
            this.RenderGame();
            this.RenderPoints();
            this.RenderBallCount();
            this.RenderBallSpeed();
            this.RenderPause();
            this.RenderMousePosition();
        }

        private void RenderMousePosition()
        {
            var mousePosition = new RectangleF(this.InputManager.Mouse.MousePosition, new SizeF(5, 5));
            this.GraphicsDevice.FillEllipse(PongBrushes.OverlayText, mousePosition);
        }

        private void RenderGame()
        {
            this._environment.Render();
        }

        private void RenderPoints()
        {
            this._points.Render();
        }

        private void RenderBallCount()
        {
            this._ballCount.Render();
        }

        private void RenderBallSpeed()
        {
            this._ballSpeed.Render();
        }
        
        private void RenderPause()
        {
            if (this.IsPaused)
            {
                this.GraphicsDevice.FillRectangle(PongBrushes.GrayOverlay, this.GraphicsDevice.GetScreenRectangle());
                
                var textSize = this.GraphicsDevice.MeasureString(PongTexts.Pause, PongFonts.Normal);
                var pauseLocation = this.GraphicsDevice.GetScreenCenterFor(textSize);

                this.GraphicsDevice.DrawString(PongTexts.Pause, PongFonts.Normal, PongBrushes.OverlayText, pauseLocation);
            }
        }

        private void RenderTempText()
        {
            this._tempText.Render();
        }

        private void EnvironmentOnEntityAdded(object sender, EntityAddedEventArgs entityAddedEventArgs)
        {
            if (entityAddedEventArgs.Entity is Ball)
            {
                this.BallCount++;
            }
        }

        private void EnvironmentOnEntityRemoved(object sender, EntityRemovedEventArgs entityRemovedEventArgs)
        {
            if (entityRemovedEventArgs.Entity is Ball)
            {
                this.BallCount--;
            }

            if (entityRemovedEventArgs.Entity is IGivePoints givePoints)
            {
                bool addPoints = givePoints is PowerUp == false || ((PowerUp)givePoints).WasActivated;

                if (addPoints)
                    this.Points += givePoints.Points;
            }

            if (entityRemovedEventArgs.Entity is PowerUp powerUp && powerUp.WasActivated)
            {
                this._tempText.ShowText(powerUp.Text, TimeSpan.FromSeconds(3));
            }

            if (this._environment.Entities.Any(f => f is Block) == false)
            {
                XGL.Get<SceneManager>().CurrentScene = new WinScene(this.Points);
            }

            if (this.BallCount == 0)
            {
                XGL.Get<SceneManager>().CurrentScene = new LoseScene(this.Points);
            }
        }
    }
}