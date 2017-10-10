using System;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using Xemio.GameLight.Components;

namespace Xemio.GamesLight.Games.Pong.Level
{
    public interface IBackgroundImageProvider : IComponent
    {
        Image GetBackgroundImage();
    }

    public class RandomImageProvider : IBackgroundImageProvider
    {
        private readonly string _folderPath;

        public RandomImageProvider(string folderPath)
        {
            this._folderPath = folderPath;
        }

        public Image GetBackgroundImage()
        {
            try
            {
                var fileNames = Directory.GetFiles(this._folderPath);

                if (fileNames.Length == 0)
                    return null;

                var random = new Random();
                var index = random.Next(fileNames.Length);

                return new Bitmap(fileNames[index]);
            }
            catch
            {
                return null;
            }
        }
    }

    public class BackgroundImageProvider : IBackgroundImageProvider
    {
        private readonly IBackgroundImageProvider _fallback;

        public BackgroundImageProvider(IBackgroundImageProvider fallback)
        {
            this._fallback = fallback;
        }

        public Image GetBackgroundImage()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false))
                {
                    var currentWallpaper = key?.GetValue("Wallpaper") as string ?? string.Empty;

                    if (File.Exists(currentWallpaper))
                    {
                        return new Bitmap(currentWallpaper);
                    }

                    return this._fallback.GetBackgroundImage();
                }
            }
            catch
            {
                return this._fallback.GetBackgroundImage();
            }
        }
    }
}