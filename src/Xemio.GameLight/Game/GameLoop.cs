using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly List<IGameLoopSubscriber> _subscribers;

        private CancellationTokenSource _tokenSource;
        private Task _task;

        public int FramesPerSecond { get; }
        public bool IsStarted => this._task != null;

        public GameLoop(int framesPerSecond)
        {
            this._subscribers = new List<IGameLoopSubscriber>();
            this.FramesPerSecond = framesPerSecond;
        }

        public void Start()
        {
            if (this.IsStarted)
                return;

            this._tokenSource = new CancellationTokenSource();
            this._task = Task.Factory.StartNew(this.Loop, this._tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            if (this.IsStarted == false)
                return;

            this._tokenSource.Cancel();
            this._task.Wait();

            this._tokenSource = null;
            this._task = null;
        }
        
        private void Loop()
        {
            TimeSpan previousElapsed = TimeSpan.Zero;
            var watch = Stopwatch.StartNew();

            while (this._tokenSource.IsCancellationRequested == false)
            {
                SpinWait.SpinUntil(() => this.NextTickRequested(watch, previousElapsed));

                var elapsed = watch.Elapsed;
                var elapsedSinceLastTick = elapsed - previousElapsed;

                foreach (var subscriber in this._subscribers)
                {
                    subscriber.Tick(elapsedSinceLastTick.TotalSeconds);
                }

                foreach (var subscriber in this._subscribers)
                {
                    subscriber.Render();
                }

                previousElapsed = elapsed;
            }
        }

        private bool NextTickRequested(Stopwatch watch, TimeSpan previousElapsed)
        {
            double millisecondsPerFrame = 1000.0 / this.FramesPerSecond * 0.985; //Tolerance
            return (watch.Elapsed - previousElapsed).TotalMilliseconds > millisecondsPerFrame;
        }
        
        public void Subscribe(IGameLoopSubscriber subscriber)
        {
            this._subscribers.Add(subscriber);
        }
    }
}