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

            var hotTimer = new HotTimer(TimeSpan.FromMilliseconds(1000));
            // var evts = ts1 //.Merge(ts2);
            //     .Publish(); //connectable
            var evts = hotTimer.GetObservable();

            //evts.Subscribe(ReceiveShort);
            //TODO: tutaj jakas magia, ktora sprawi, ze publikowanie bedzie czekalo
            //na zakonczenie ReceiveLong i zachowywalo kolejnosc
            evts.Subscribe(ReceiveLong, () => {
                Printer.Print($"Completed, handled: {receiveLongHandled} events");
            });
            //TODO: wspomniana wyzej magia nie powinna wplywac na to
            //obecnie i tak to nie dziala poprawnie
            evts.Subscribe(ReceiveShort);

            Console.WriteLine("Press any key...");
            Console.Read();
        }

        static void ReceiveShort(string evtDesc)
        {
            Printer.Print($"Got {evtDesc}");
        }

        private static object receiveLongLock = new object();
        private static int receiveLongHandled = 0;
        static void ReceiveLong(string evtDesc)
        {
            //TODO: nie chcialbym locka - chcialbym miec to zalatwione przez rx i nie miec popsutej kolejnosci
            lock(receiveLongLock)
            {
                Printer.Print($"Starting {evtDesc}");
                Thread.Sleep(TimeSpan.FromMilliseconds(2000));
                Printer.Print($"Finished {evtDesc}");
                receiveLongHandled++;
                Printer.Print($"Handled {receiveLongHandled}");
            }
        }
    }
}