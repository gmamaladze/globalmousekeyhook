using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;
using MouseKeyHook;
using System.Reactive.Linq;
using System.Threading;
using MouseKeyHook.Rx;
using System.Windows.Forms;

namespace ConsoleHook.Rx
{
    class Program
    {
        static void Main(string[] args)
        {
            var quit = new AutoResetEvent(false);

            var selector = new Dictionary<string, Action<AutoResetEvent>>()
            {
                {"1. Detect key sequence", SequenceSample},
                {"2. Detect key combinationss", CombinationSample},
                {"Q. Quit", Exit}
            };

            Action<AutoResetEvent> action = null;

            while (action == null)
            {
                Console.WriteLine("Please select one of these:");
                foreach (var selectorKey in selector.Keys)
                {
                    Console.WriteLine(selectorKey);
                }
                var ch = Console.ReadKey(true).KeyChar;
                action = selector
                    .Where(p => p.Key.StartsWith(ch.ToString()))
                    .Select(p=>p.Value).FirstOrDefault();
            }
            Console.WriteLine("--------------------------------------------------");
            action(quit);

            while (!quit.WaitOne(100))
            {
                Application.DoEvents();
            };
        }

        private static void CombinationSample(AutoResetEvent quit)
        {
            var quitTrigger = Trigger.FromString("Control+Q");
            var triggers = new[]
            {
                quitTrigger,
                Trigger.On(Keys.H).Alt().Shift(),
                Trigger.On(Keys.E).And(Keys.Q).And(Keys.W)
            };


            Hook
                .GlobalEvents()
                .KeyDownObservable()
                .Matching(triggers)
                .ForEachAsync(trigger =>
                {
                    if (trigger==quitTrigger) quit.Set();
                    Console.WriteLine(trigger);
                });

            Console.WriteLine("Press Control+Q to quit.");
            Console.WriteLine("Monitoring folowing combinations:");
            foreach (var name in triggers)
            {
                Console.WriteLine("\t" + name);
            }
        }

        private static void Exit(AutoResetEvent quit)
        {
            quit.Set();
        }

        private static void SequenceSample(AutoResetEvent quit)
        {
            var map = new []
            {
                new KeySequence("Control+Q", Keys.Q.Control().Down()),
                new KeySequence("Alt+Shift+H", Keys.H.Alt().Shift().Down()),
                new KeySequence("Shift+Z,Z", Keys.Z.Shift().Down(), Keys.Z.Shift().Up(), Keys.Z.Shift().Down()),
                new KeySequence("abc", Keys.A.Down(), Keys.B.Down(), Keys.C.Down()), 
            };

            Hook
                .GlobalEvents()
                .UpDownEvents()
                .Match(map)
                .ForEachAsync(sequence =>
                {
                    if (sequence.Id == "Control+Q") quit.Set();
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
