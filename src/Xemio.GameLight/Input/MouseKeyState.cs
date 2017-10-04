using System.Windows.Forms;

namespace Xemio.GameLight.Input
{
    public struct MouseKeyState
    {
        public MouseKeyState(MouseButtons button, bool isPressed)
        {
            this.Button = button;
            this.IsPressed = isPressed;
        }

        public MouseButtons Button { get; }
        public bool IsPressed { get; }
    }
}