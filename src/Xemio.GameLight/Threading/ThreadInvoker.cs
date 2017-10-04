using System;
using System.Windows.Forms;
using Xemio.GameLight.Components;

namespace Xemio.GameLight.Threading
{
    public class ThreadInvoker : IComponent
    {
        private readonly Control _control;

        public ThreadInvoker(Control control)
        {
            this._control = control;
        }

        public void Execute(Action action)
        {
            if (this._control.InvokeRequired)
            {
                try
                {
                    this._control.Invoke(action);
                }
                catch (ObjectDisposedException)
                {
                }
            }
            
            action();
        }
    }
}