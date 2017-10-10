using System.Collections.Generic;
using System.Drawing;
using System.Security.Principal;
using Xemio.GameLight.Components;
using Xemio.GamesLight.Games.Pong.Entities;

namespace Xemio.GamesLight.Games.Pong.Level
{
    public interface IBlockGenerator : IComponent
    {
        IList<Block> GenerateBlocks(RectangleF area);
    }
}