using System;
using System.Drawing;
using System.Linq;
using Xemio.GameLight;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Level;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public class SpawnBlockPowerUp : PowerUp
    {
        public override TimeSpan AvailableFor { get; } = TimeSpan.FromSeconds(10);
        public override bool IsPositive { get; } = true;
        public override Image Icon { get; } = XGL.Get<IIconProvider>().GetSpawnBlockPowerUpIcon();

        public override void ActivatePowerUp(Ball ball)
        {
            var playerLine = this.Environment.Entities.OfType<PlayerLine>().FirstOrDefault();

            if (playerLine == null)
                return;

            var backgroundImage = XGL.Get<IBackgroundImageProvider>().GetBackgroundImage();
            this.Environment.Add(new Block(playerLine.Location, playerLine.Size, backgroundImage));
        }

        public override string Text => "I'm helping!";
    }
}