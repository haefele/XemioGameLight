using System.Windows.Forms;

namespace Xemio.GameLight.Input
{
    public struct KeyboardState
    {
        public KeyboardState(Keys key, bool isPressed)
        {
            this.Key = key;
            this.IsPressed = isPressed;
        }

        public Keys Key { get; }
        public bool IsPressed { get; }
    }
}