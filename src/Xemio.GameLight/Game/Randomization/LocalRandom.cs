using System;

namespace Xemio.GameLight.Game.Randomization
{
    public class LocalRandom : IRandom
    {
        private readonly Random _random;

        public LocalRandom()
        {
            this._random = new Random();
        }

        public float Next(int min, int max)
        {
            lock (this._random)
            {
                return this._random.Next(min, max);
            }
        }
    }
}