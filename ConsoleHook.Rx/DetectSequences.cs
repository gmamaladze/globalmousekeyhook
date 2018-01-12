using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using MouseKeyHook.Rx;

namespace ConsoleHook.Rx
{
    internal class DetectSequences
    {
        public static void Do(AutoResetEvent quit)
        {
            var map = new []
            {
                new Sequence<Keys>(Keys.Control, Keys.Q),
                new Sequence<Keys>(Keys.Alt, Keys.Shift),
            };

            Hook
                .GlobalEvents()
                .KeyDownObservable()
                .Sequences(2,3)
                //.Matching(map)
                .ForEachAsync(sequence =>
                {
                    //if (sequence.Id == "Control+Q") quit.Set();
                    Console.WriteLine(sequence);
                });

            Console.WriteLine("Press Control+Q to quit.");
            Console.WriteLine("Monitoring folowing sequences:");
            foreach (var name in map)
            {
                Console.WriteLine("\t" + name);
            }
        }
    }
}