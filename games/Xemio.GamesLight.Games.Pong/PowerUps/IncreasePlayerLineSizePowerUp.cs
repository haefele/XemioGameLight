using System;
using System.Drawing;
using System.Linq;
using Xemio.GameLight;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Level;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public class IncreasePlayerLineSizePowerUp : PowerUp, IGivePoints
    {
        public override bool IsPositive => true;
        public override Image Icon { get; } = XGL.Get<IIconProvider>().GetIncreasePlayerLineSizePowerUpIcon();
        public override TimeSpan AvailableFor { get; } = TimeSpan.FromSeconds(10);
        
        public override void ActivatePowerUp(Ball ball)
        {
            var playerLine = this.Environment.Entities.OfType<PlayerLine>().FirstOrDefault();

            if (playerLine == null)
                return;

            playerLine.IncreaseSize();
        }

        public int Points => 10;
        public override string Text => "Jetzt hast du 'nen Langen!";
    }
}