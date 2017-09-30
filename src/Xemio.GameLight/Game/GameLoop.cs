using System;
using System.Collections.Generic;
using Xemio.GameLight.Components;
using Timer = System.Threading.Timer;

namespace Xemio.GameLight.Game
{
    public interface IGameLoopSubscriber
    {
        void Tick(double elapsed);
        void Render();
    }

    public class GameLoop : IComponent
    {
        private Timer _timer;
        private List<IGameLoopSubscriber> _subscribers;

        public int FramesPerSecond { get; }

        public GameLoop(int framesPerSecond)
        {
            this._subscribers = new List<IGameLoopSubscriber>();
            this.FramesPerSecond = framesPerSecond;

            this._timer = new Timer(this.Callback, null, TimeSpan.FromMilliseconds(-1), TimeSpan.Zero);
        }

        public void Run()
        {
            this._timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000.0 / this.FramesPerSecond));
        }

        private void Callback(object state)
        {
            foreach (var subscriber in this._subscribers)
            {
                subscriber.Tick(0);
            }

            foreach (var subscriber in this._subscribers)
            {
                subscriber.Render();
            }
        }

        public void Subscribe(IGameLoopSubscriber subscriber)
        {
            this._subscribers.Add(subscriber);
        }
    }
}