using System;
using System.Reactive.Linq;
using System.Threading;

namespace RxPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var ts1 = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000))
                .Select(i => $"ts1-{i}-{Guid.NewGuid()}");
            var ts2 = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1500))
                .Select(i => $"ts2-{i}");

            var evts = ts1;//.Merge(ts2);
            evts.Publish(); //hot
    
            evts.Subscribe(ReceiveShort);
            evts.Subscribe(ReceiveLong);

            Console.WriteLine("Press any key...");
            Console.Read();
        }

        static void ReceiveShort(string evtDesc)
        {
            Printer.Print($"Got {evtDesc}");
        }

        static void ReceiveLong(string evtDesc)
        {
            Printer.Print($"Starting {evtDesc}");
            Thread.Sleep(TimeSpan.FromMilliseconds(2000));
            Printer.Print($"Finished {evtDesc}");
        }
    }
}