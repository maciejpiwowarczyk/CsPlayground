using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Timers;

namespace RxPlayground
{
    class HotTimer
    {        
        private int eventNumber = 0;
        private readonly Subject<string> subject = new Subject<string>();        
        private readonly Timer timer; 

        public HotTimer(TimeSpan interval)
        {            
            this.timer = new Timer(interval.TotalMilliseconds);
            this.timer.Elapsed += OnElapsed;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {            
            eventNumber++;
            if (eventNumber > 10)
            {
                subject.OnCompleted();
                timer.Enabled = false;
                return;
            }
            var eventId = $"hottimer-{eventNumber}-{Guid.NewGuid()}";            
            Printer.Print($"Publishing event: {eventId}");
            subject.OnNext(eventId);            
        }

        public IObservable<string> GetObservable()
        {
            timer.Enabled = true;
            return subject.AsObservable();
        }
    }
}