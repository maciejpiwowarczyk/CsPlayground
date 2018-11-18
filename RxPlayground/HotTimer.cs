using System;
using System.Reactive.Linq;
using System.Timers;

namespace RxPlayground
{
    class HotTimer
    {
        class HotTimerEventArgs : EventArgs
        {
            public string EventId { get; private set; }
            public HotTimerEventArgs(string eventId)
            {
                EventId = eventId;
            }
        }

        private int eventNumber = 0;
        private event EventHandler<HotTimerEventArgs> OnElapsedEvent;
        private readonly Timer timer;

        public HotTimer(TimeSpan interval)
        {
            this.timer = new Timer(interval.TotalMilliseconds);
            this.timer.Elapsed += OnElapsed;
        }

        HotTimerEventArgs GetNextHotTimerEventArgs()
        {
            var eventId = $"hottimer-{eventNumber}-{Guid.NewGuid()}";
            return new HotTimerEventArgs(eventId);
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            eventNumber++;
            if (eventNumber > 10)
            {
                timer.Enabled = false;
                return;
            }
            var eventArgs = GetNextHotTimerEventArgs();
            Printer.Print($"Publishing event: {eventArgs.EventId}");
            OnElapsedEvent(this, eventArgs);
        }

        public IObservable<string> GetObservable()
        {
            var obs = Observable.FromEventPattern<HotTimerEventArgs>(
                h => OnElapsedEvent += h,
                h => OnElapsedEvent -= h
            ).Select(ev => ev.EventArgs.EventId);
            timer.Enabled = true;
            return obs;
        }
    }
}