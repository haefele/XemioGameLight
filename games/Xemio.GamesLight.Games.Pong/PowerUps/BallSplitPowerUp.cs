using System;
using System.Drawing;
using Xemio.GameLight;
using Xemio.GamesLight.Games.Pong.Entities;
using Xemio.GamesLight.Games.Pong.Level;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public class BallSplitPowerUp : PowerUp
    {
        public override TimeSpan AvailableFor { get; } = TimeSpan.FromSeconds(10);
        public override bool IsPositive { get; } = true;
        public override Image Icon { get; } = XGL.Get<IIconProvider>().GetBallSplitPowerUpIcon();

        public override void ActivatePowerUp(Ball ball)
        {
            this.Environment.Add(new Ball
            {
                Speed = ball.Speed,
                Location = ball.Location,
                Direction = new PointF(0, -1)
            });
        }

        public override string Text => "Multitasking!";
    }
}