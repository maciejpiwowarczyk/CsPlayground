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

            ProcessingQueue.RunExample();
            
            Console.WriteLine("Press any key...");
            Console.Read();
        }
    }
}