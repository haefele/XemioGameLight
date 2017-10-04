using System;
using System.Drawing;
using System.Windows.Forms;
using Xemio.GameLight.Components;
using Xemio.GameLight.Game;

namespace Xemio.GameLight.Input
{
    public class InputManager : IComponent, IGameLoopSubscriber
    {
        public InputManager(Control control, Size backBufferSize)
        {
            this.Keyboard = new KeyboardAdapter(control);
            this.Mouse = new MouseAdapter(control, backBufferSize);
        }

        public KeyboardAdapter Keyboard { get; }
        public MouseAdapter Mouse { get; }

        public void Tick(double elapsed)
        {
            this.Keyboard.PushCurrentToPrevious();
            this.Mouse.PushCurrentToPrevious();
        }

        public void Render()
        {
        }
    }
}