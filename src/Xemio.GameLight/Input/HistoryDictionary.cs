using System.Collections.Concurrent;
using System.Threading;

namespace Xemio.GameLight.Input
{
    public class HistoryDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> _current;
        private readonly ConcurrentDictionary<TKey, TValue> _previous;
        private readonly ReaderWriterLockSlim _pushCurrentToPreviousLock;

        public HistoryDictionary()
        {
            this._current = new ConcurrentDictionary<TKey, TValue>();
            this._previous = new ConcurrentDictionary<TKey, TValue>();
            this._pushCurrentToPreviousLock = new ReaderWriterLockSlim();
        }

        public bool TryGetPrevious(TKey key, out TValue value)
        {
            this._pushCurrentToPreviousLock.EnterReadLock();

            try
            {
                return this._previous.TryGetValue(key, out value);
            }
            finally
            {
                this._pushCurrentToPreviousLock.ExitReadLock();
            }
        }

        public bool TryGetCurrent(TKey key, out TValue value)
        {
            this._pushCurrentToPreviousLock.EnterReadLock();

            try
            {
                return this._current.TryGetValue(key, out value);
            }
            finally
            {
                this._pushCurrentToPreviousLock.ExitReadLock();
            }
        }

        public void SetCurrent(TKey key, TValue value)
        {
            this._current.AddOrUpdate(key, value, (_, __) => value);
        }

        public void PushCurrentToPrevious()
        {
            this._pushCurrentToPreviousLock.EnterWriteLock();
            try
            {
                this._previous.Clear();
                foreach (var pair in this._current)
                {
                    this._previous[pair.Key] = pair.Value;
                }
            }
            finally
            {
                this._pushCurrentToPreviousLock.ExitWriteLock();
            }
        }
    }
}