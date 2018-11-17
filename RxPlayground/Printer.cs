using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RxPlayground
{
    static class Printer
    {
        public static void Print(string message, [CallerMemberName] string memberName = "")
        {
            var dt = DateTime.Now.ToString("HH:mm:ss,fff");
            Console.WriteLine($"{dt} [{Thread.CurrentThread.ManagedThreadId}] {memberName}: " + message);
        }
    }
}