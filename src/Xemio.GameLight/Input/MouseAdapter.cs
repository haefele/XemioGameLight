using System;
using System.Drawing;
using System.Windows.Forms;

namespace Xemio.GameLight.Input
{
    public class MouseAdapter
    {
        private readonly HistoryDictionary<MouseButtons, MouseKeyState> _mouseKeyStates;
        private readonly Control _control;
        private readonly Size _backBufferSize;

        public PointF MousePosition { get; private set; }

        public MouseAdapter(Control control, Size backBufferSize)
        {
            this._mouseKeyStates = new HistoryDictionary<MouseButtons, MouseKeyState>();
            this._control = control;
            this._backBufferSize = backBufferSize;

            control.MouseMove += this.ControlOnMouseMove;
            control.MouseDown += this.ControlOnMouseDown;
            control.MouseUp += this.ControlOnMouseUp;
        }

        #region Current State
        public bool IsPressed(MouseButtons keys)
        {
            return this.GetCurrent(keys).IsPressed;
        }
        public bool IsReleased(MouseButtons key)
        {
            return this.GetCurrent(key).IsPressed == false;
        }
        #endregion

        #region Previous State
        public bool WasPressed(MouseButtons key)
        {
            return this.GetPrevious(key).IsPressed;
        }
        public bool WasReleased(MouseButtons key)
        {
            return this.GetPrevious(key).IsPressed == false;
        }
        #endregion

        #region State Changes
        public bool GotPressed(MouseButtons key)
        {
            return this.WasReleased(key) && this.IsPressed(key);
        }
        public bool GotReleased(MouseButtons key)
        {
            return this.WasPressed(key) && this.IsReleased(key);
        }
        #endregion

        public MouseKeyState GetPrevious(MouseButtons key)
        {
            if (this._mouseKeyStates.TryGetPrevious(key, out var state))
                return state;

            return new MouseKeyState(key, false);
        }

        public MouseKeyState GetCurrent(MouseButtons key)
        {
            if (this._mouseKeyStates.TryGetCurrent(key, out var state))
                return state;

            return new MouseKeyState(key, false);
        }
        
        private void ControlOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var xRatio = this._control.Width / (float)this._backBufferSize.Width;
            var yRatio = this._control.Height / (float)this._backBufferSize.Height;

            this.MousePosition = new PointF(mouseEventArgs.X / xRatio, mouseEventArgs.Y / yRatio);
        }

        private void ControlOnMouseDown(object sender, MouseEventArgs mouseEventArgs)
        {
            void CheckForButtonAndSetState(MouseButtons button)
            {
                if (mouseEventArgs.Button.HasFlag(button))
                {
                    this._mouseKeyStates.SetCurrent(button, new MouseKeyState(button, true));
                }
            }

            CheckForButtonAndSetState(MouseButtons.Left);
            CheckForButtonAndSetState(MouseButtons.Right);
            CheckForButtonAndSetState(MouseButtons.Middle);
            CheckForButtonAndSetState(MouseButtons.XButton1);
            CheckForButtonAndSetState(MouseButtons.XButton2);
        }

        private void ControlOnMouseUp(object sender, MouseEventArgs mouseEventArgs)
        {
            void CheckForButtonAndSetState(MouseButtons button)
            {
                if (mouseEventArgs.Button.HasFlag(button))
                {
                    this._mouseKeyStates.SetCurrent(button, new MouseKeyState(button, false));
                }
            }

            CheckForButtonAndSetState(MouseButtons.Left);
            CheckForButtonAndSetState(MouseButtons.Right);
            CheckForButtonAndSetState(MouseButtons.Middle);
            CheckForButtonAndSetState(MouseButtons.XButton1);
            CheckForButtonAndSetState(MouseButtons.XButton2);
        }
        
        internal void PushCurrentToPrevious()
        {
            this._mouseKeyStates.PushCurrentToPrevious();
        }
    }
}