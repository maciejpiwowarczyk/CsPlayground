using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RxPlayground
{
    static class Printer
    {
        public static void Print(string message, [CallerMemberName] string memberName = "")
        {            
            var threadDesc = Thread.CurrentThread.ManagedThreadId.ToString();
            var syncContext = SynchronizationContext.Current;            
            if (syncContext != null) 
            {
                threadDesc += $" {syncContext}";
            }
            var dt = DateTime.Now.ToString("HH:mm:ss,fff");
            Console.WriteLine($"{dt} [{threadDesc}] {memberName}: " + message);
        }
    }
}