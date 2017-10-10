using System;
using System.Collections.Generic;
using System.Drawing;
using Xemio.GameLight;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game.Randomization;
using Xemio.GameLight.Rendering;

namespace Xemio.GamesLight.Games.Pong.PowerUps
{
    public class PowerUpManager : IComponent
    {
        private double _elapsedSincePossibleSpawn;

        public int Chance { get; set; } = 20; // 100
        public double TimeUntilPossibleSpawn { get; set; } = 2;

        private readonly List<Type> _powerUpTypes = new List<Type>
        {
            typeof(BallSplitPowerUp),
            typeof(IncreasePlayerLineSizePowerUp),
            typeof(PointsPowerUp),
            typeof(ReducePlayerLineSizePowerUp),
            typeof(SpawnBlockPowerUp),
        };
        
        public PowerUp SpawnPowerUp(double elapsed)
        {
            this._elapsedSincePossibleSpawn += elapsed;

            if (this._elapsedSincePossibleSpawn < this.TimeUntilPossibleSpawn)
                return null;

            this._elapsedSincePossibleSpawn = 0;

            var random = XGL.Get<IRandom>();

            if (random.Next(0, this.Chance) >= 5)
                return null;

            var graphicsDevice = XGL.Get<GraphicsDevice>();

            var x = random.Next(0, graphicsDevice.Width);
            var y = random.Next(0, graphicsDevice.Height);

            var powerUp = this.CreateRandomPowerUp(random);
            powerUp.Location = new PointF(x, y);

            return powerUp;
        }

        private PowerUp CreateRandomPowerUp(IRandom random)
        {
            var powerUpIndex = (int)random.Next(0, this._powerUpTypes.Count);
            var type = this._powerUpTypes[powerUpIndex];

            return (PowerUp)Activator.CreateInstance(type);
        }
    }
}