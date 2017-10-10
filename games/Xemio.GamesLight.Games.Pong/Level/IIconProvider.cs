using System.Drawing;
using System.IO;
using Xemio.GameLight.Components;

namespace Xemio.GamesLight.Games.Pong.Level
{
    public interface IIconProvider : IComponent
    {
        Image GetPointsIcon();
        Image GetBallCountIcon();
        Image GetBallSpeedIcon();
        Image GetIncreasePlayerLineSizePowerUpIcon();
        Image GetReducePlayerLineSizePowerUpIcon();
        Image PointsPowerUpIcon();
        Image GetBallSplitPowerUpIcon();
        Image GetSpawnBlockPowerUpIcon();
    }

    public class FileBasedIconProvider : IIconProvider
    {
        private readonly string _folderPath;

        public FileBasedIconProvider(string folderPath)
        {
            this._folderPath = folderPath;
        }

        public Image GetPointsIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "points.png"));
        }

        public Image GetBallCountIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "ballcount.png"));
        }

        public Image GetBallSpeedIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "ballspeed.png"));
        }

        public Image GetIncreasePlayerLineSizePowerUpIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "increaseplayerlinesizepowerup.png"));
        }

        public Image GetReducePlayerLineSizePowerUpIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "reduceplayerlinesizepowerup.png"));
        }

        public Image PointsPowerUpIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "pointspowerup.png"));
        }

        public Image GetBallSplitPowerUpIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "ballsplitpowerup.png"));
        }

        public Image GetSpawnBlockPowerUpIcon()
        {
            return new Bitmap(Path.Combine(this._folderPath, "spawnblockpowerup.png"));
        }
    }
}