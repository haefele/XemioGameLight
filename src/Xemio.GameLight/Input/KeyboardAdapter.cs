using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;

namespace Xemio.GameLight.Input
{
    public class KeyboardAdapter
    {
        private readonly ConcurrentDictionary<Keys, KeyboardState> _currentKeyboardStates;
        private readonly ConcurrentDictionary<Keys, KeyboardState> _previousKeyboardStates;
        private readonly ReaderWriterLockSlim _pushCurrentToPreviousLock;

        public KeyboardAdapter(Control control)
        {
            this._currentKeyboardStates = new ConcurrentDictionary<Keys, KeyboardState>();
            this._previousKeyboardStates = new ConcurrentDictionary<Keys, KeyboardState>();
            this._pushCurrentToPreviousLock = new ReaderWriterLockSlim();

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
            this._pushCurrentToPreviousLock.EnterReadLock();

            try
            {
                if (this._previousKeyboardStates.TryGetValue(key, out var state))
                    return state;

                return new KeyboardState(key, false);
            }
            finally
            {
                this._pushCurrentToPreviousLock.ExitReadLock();
            }
        }

        public KeyboardState GetCurrent(Keys key)
        {
            this._pushCurrentToPreviousLock.EnterReadLock();

            try
            {
                if (this._currentKeyboardStates.TryGetValue(key, out var state))
                    return state;

                return new KeyboardState(key, false);
            }
            finally
            {
                this._pushCurrentToPreviousLock.ExitReadLock();
            }
        }

        private void ControlOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            var state = new KeyboardState(keyEventArgs.KeyCode, false);
            this._currentKeyboardStates.AddOrUpdate(state.Key, state, (_, __) => state);
        }

        private void ControlOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            var state = new KeyboardState(keyEventArgs.KeyCode, true);
            this._currentKeyboardStates.AddOrUpdate(state.Key, state, (_, __) => state);
        }

        internal void PushCurrentToPrevious()
        {
            this._pushCurrentToPreviousLock.EnterWriteLock();
            try
            {
                this._previousKeyboardStates.Clear();
                foreach (var pair in this._currentKeyboardStates)
                {
                    this._previousKeyboardStates[pair.Key] = pair.Value;
                }
            }
            finally
            {
                this._pushCurrentToPreviousLock.ExitWriteLock();
            }
        }
    }
}