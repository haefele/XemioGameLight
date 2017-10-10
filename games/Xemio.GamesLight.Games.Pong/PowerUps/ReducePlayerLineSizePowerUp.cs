using System;
using System.Drawing;
using System.Linq;
using Xemio.GameLight;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Level;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public class ReducePlayerLineSizePowerUp : PowerUp, IGivePoints
    {
        public override TimeSpan AvailableFor { get; } = TimeSpan.FromSeconds(10);
        public override bool IsPositive { get; } = false;
        public override Image Icon { get; } = XGL.Get<IIconProvider>().GetReducePlayerLineSizePowerUpIcon();

        public override void ActivatePowerUp(Ball ball)
        {
            var playerLine = this.Environment.Entities.OfType<PlayerLine>().FirstOrDefault();

            if (playerLine == null)
                return;

            playerLine.ReduceSize();
        }

        public int Points => 50;
        public override string Text => "Ups, der ist ja putzig!";
    }
}