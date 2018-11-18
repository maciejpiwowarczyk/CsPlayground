using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace RxPlayground
{
    static class ProcessingQueue
    {
        public static void RunExample()
        {
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
        }

        private static int receiveShortHandled = 0;
        static void ReceiveShort(string evtDesc)
        {
            Printer.Print($"Got {evtDesc}");
            receiveShortHandled++;
        }

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