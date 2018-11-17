using System;
using System.Reactive.Linq;

namespace RxPlayground 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            var xs = Observable.Range(1, 10);
            xs.Subscribe(x => Console.WriteLine("Received {0}", x),
                () => Console.WriteLine("Done"));
        }
    }
}