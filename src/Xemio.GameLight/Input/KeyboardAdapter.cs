using System.Windows.Forms;

namespace Xemio.GameLight.Input
{
    public class KeyboardAdapter
    {
        private readonly HistoryDictionary<Keys, KeyboardState> _keyboardStates;

        public KeyboardAdapter(Control control)
        {
            this._keyboardStates = new HistoryDictionary<Keys, KeyboardState>();

            control.KeyDown += this.ControlOnKeyDown;
            control.KeyUp += this.ControlOnKeyUp;
        }

        #region Current State
        public bool IsPressed(Keys keys)
        {
            return this.GetCurrent(keys).IsPressed;
        }
        public bool IsReleased(Keys key)
        {
            return this.GetCurrent(key).IsPressed == false;
        }
        #endregion

        #region Previous State
        public bool WasPressed(Keys key)
        {
            return this.GetPrevious(key).IsPressed;
        }
        public bool WasReleased(Keys key)
        {
            return this.GetPrevious(key).IsPressed == false;
        }
        #endregion

        #region State Changes
        public bool GotPressed(Keys key)
        {
            return this.WasReleased(key) && this.IsPressed(key);
        }
        public bool GotReleased(Keys key)
        {
            return this.WasPressed(key) && this.IsReleased(key);
        }
        #endregion

        public KeyboardState GetPrevious(Keys key)
        {
            if (this._keyboardStates.TryGetPrevious(key, out var state))
                return state;

            return new KeyboardState(key, false);
        }

        public KeyboardState GetCurrent(Keys key)
        {
            if (this._keyboardStates.TryGetCurrent(key, out var state))
                return state;

            return new KeyboardState(key, false);
        }

        private void ControlOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            var state = new KeyboardState(keyEventArgs.KeyCode, false);
            this._keyboardStates.SetCurrent(state.Key, state);
        }

        private void ControlOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            var state = new KeyboardState(keyEventArgs.KeyCode, true);
            this._keyboardStates.SetCurrent(state.Key, state);
        }

        internal void PushCurrentToPrevious()
        {
            this._keyboardStates.PushCurrentToPrevious();
        }
    }
}