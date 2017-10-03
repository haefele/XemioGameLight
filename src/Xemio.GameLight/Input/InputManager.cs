using System;
using System.Windows.Forms;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game;

namespace Xemio.GameLight.Input
{
    public class InputManager : IComponent, IGameLoopSubscriber
    {
        public InputManager(Control control)
        {
            this.Keyboard = new KeyboardAdapter(control);
            this.Mouse = new MouseAdapter(control);
        }

        public KeyboardAdapter Keyboard { get; }
        public MouseAdapter Mouse { get; }

        public void Tick(double elapsed)
        {
            this.Keyboard.PushCurrentToPrevious();
        }

        public void Render()
        {
        }
    }

    public class MouseAdapter
    {
        public MouseAdapter(Control control)
        {
        }
    }
}