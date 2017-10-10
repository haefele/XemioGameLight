using System.Drawing;
using Xemio.GamesLight.Games.Pong.Entities;

namespace Xemio.GamesLight.Games.Pong
{
    public static class PongPens
    {
        public static Pen BallOutline { get; } = new Pen(Color.DarkRed, 4);
        public static Pen BlockLightOutline { get; } = new Pen(Color.FromArgb(100, Color.White), Block.BorderThickness);
        public static Pen BlockDarkOutline { get; } = new Pen(Color.FromArgb(100, Color.Black), Block.BorderThickness);
        public static Pen PositivePowerUp { get; } = new Pen(Color.Green, 2);
        public static Pen NegativePowerUp { get; } = new Pen(Color.Red, 2);
    }
}