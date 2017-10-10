using System.Drawing;

namespace Xemio.GamesLight.Games.Pong
{
    public static class PongBrushes
    {
        public static Brush Text { get; } = new SolidBrush(Color.Black);
        public static Brush OverlayText { get; } = new SolidBrush(Color.White);
        public static Brush GrayOverlay { get; } = new SolidBrush(Color.FromArgb(100, Color.Black));
        public static Brush Ball { get; } = new SolidBrush(Color.Red);
        public static Brush PositivePowerUp { get; } = new SolidBrush(Color.FromArgb(100, Color.Green));
        public static Brush NegativePowerUp { get; } = new SolidBrush(Color.FromArgb(100, Color.Red));
    }
}