using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace RxPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            // var ts1 = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000))
            //     .Select(i => $"ts1-{i}-{Guid.NewGuid()}");
            // var ts2 = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1500))
            //     .Select(i => $"ts2-{i}");

            // var evts = ts1 //.Merge(ts2);
            //     .Publish(); //connectable

            var hotTimer = new HotTimer(TimeSpan.FromMilliseconds(1000));
            var evts = hotTimer.GetObservable();

            evts
                .ObserveOn(NewThreadScheduler.Default) //dedykowany watek dla obslugi tego observera
                .Subscribe(ReceiveLong, () =>
                {
                    Printer.Print($"Completed-ReceiveLong, handled: {receiveLongHandled} events");
                });

            evts
                .Subscribe(ReceiveShort, () =>
                {
                    Printer.Print($"Completed-ReceiveShort, handled: {receiveShortHandled} events");
                });

            Console.WriteLine("Press any key...");
            Console.Read();
        }

        private static int receiveShortHandled = 0;
        static void ReceiveShort(string evtDesc)
        {
            Printer.Print($"Got {evtDesc}");
            receiveShortHandled++;
        }

        private static object receiveLongLock = new object();
        private static int receiveLongHandled = 0;
        static void ReceiveLong(string evtDesc)
        {
            Printer.Print($"Starting {evtDesc}");
            Thread.Sleep(TimeSpan.FromMilliseconds(2000));
            Printer.Print($"Finished {evtDesc}");
            receiveLongHandled++;
        }
    }
}