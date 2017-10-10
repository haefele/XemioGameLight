using System;
using System.Drawing;
using System.Linq;
using Xemio.GameLight;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Level;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public class PointsPowerUp : PowerUp, IGivePoints
    {
        public override TimeSpan AvailableFor { get; } = TimeSpan.FromSeconds(10);
        public override bool IsPositive { get; } = true;
        public override Image Icon { get; } = XGL.Get<IIconProvider>().PointsPowerUpIcon();
        
        public override void ActivatePowerUp(Ball ball)
        {
        }

        public int Points => 1000;
        public override string Text => "Caaaaash!";
    }
}