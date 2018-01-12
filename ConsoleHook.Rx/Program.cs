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
                {"1. Detect key sequence", DetectSequences.Do},
                {"2. Detect key combinationss", DetectCombinations.Do},
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

        private static void Exit(AutoResetEvent quit)
        {
            quit.Set();
        }
    }
}
