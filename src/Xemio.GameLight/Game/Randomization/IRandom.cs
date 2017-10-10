using Xemio.GameLight.Components;

namespace Xemio.GameLight.Game.Randomization
{
    public interface IRandom : IComponent
    {
        float Next(int min, int max);
    }
}