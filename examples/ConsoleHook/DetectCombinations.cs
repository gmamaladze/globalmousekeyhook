using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ConsoleHook
{
    internal class DetectCombinations
    {
        public static void Do(AutoResetEvent quit)
        {
            var map = new Dictionary<Combination, Action>
            {
                //Specify which key combinations to detect and action - what to do if detected.
                //You can create a key combinations directly from string or ...
                {Combination.FromString("A+B+C") , () => Console.WriteLine(":-)")},
                //... or alternatively you can use builder methods
                {Combination.TriggeredBy(Keys.F).With(Keys.E).With(Keys.D) , () => Console.WriteLine(":-D")},
                {Combination.FromString("Alt+A") , () => Console.WriteLine(":-P")},
                {Combination.FromString("Control+Shift+Z") , () => Console.WriteLine(":-/")},
                {Combination.FromString("Escape") , () => quit.Set()},
            };

            Console.WriteLine("Detecting following combinations:");
            foreach (var combination in map.Keys)
            {
                Console.WriteLine("\t{0}",combination);
            }
            Console.WriteLine("Press ESC to exit.");

            //Actual loop
            Hook.GlobalEvents().OnCombination(map);
        }
    }
}