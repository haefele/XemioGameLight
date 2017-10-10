using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xemio.GamesLight.Games.Pong.Entities;

namespace Xemio.GamesLight.Games.Pong.Level
{
    public class RegularBlockGenerator : IBlockGenerator
    {
        private readonly IBackgroundImageProvider _backgroundImageProvider;

        public RegularBlockGenerator(IBackgroundImageProvider backgroundImageProvider)
        {
            this._backgroundImageProvider = backgroundImageProvider;

            this.Lines = 8;
            this.Columns = 10;
        }

        public int Lines { get; set; }
        public int Columns { get; set; }

        public IList<Block> GenerateBlocks(RectangleF area)
        {
            var backgroundImage = this._backgroundImageProvider.GetBackgroundImage();

            var blocks = new List<RectangleF>();

            var heightPerLine = area.Height / this.Lines;
            var widthPerColumn = area.Width / this.Columns;

            for (int line = 0; line < this.Lines; line++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    blocks.Add(new RectangleF(column * widthPerColumn, line * heightPerLine, widthPerColumn, heightPerLine));
                }
            }
            
            return blocks
                .Select(f => new Block(f.Location, f.Size, backgroundImage))
                .ToList();
        }
    }
}