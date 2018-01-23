using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ConsoleHook
{
    class Program
    {
        private static void Main(string[] args)
        {
            var quit = new AutoResetEvent(false);

            var selector = new Dictionary<string, Action<AutoResetEvent>>
            {
                {"1. Log keys", LogKeys.Do},
                {"2. Detect key combinations", DetectCombinations.Do},
                {"3. Detect key sequences", DetectSequences.Do},
                {"Q. Quit", Exit}
            };

            Action<AutoResetEvent> action = null;

            while (action == null)
            {
                Console.WriteLine("Please select one of these:");
                foreach (var selectorKey in selector.Keys)
                    Console.WriteLine(selectorKey);
                var ch = Console.ReadKey(true).KeyChar;
                action = selector
                    .Where(p => p.Key.StartsWith(ch.ToString()))
                    .Select(p => p.Value).FirstOrDefault();
            }
            Console.WriteLine("--------------------------------------------------");
            action(quit);

            while (!quit.WaitOne(10))
                Application.DoEvents();
            ;
        }

        private static void Exit(AutoResetEvent quit)
        {
            quit.Set();
        }
    }

    internal class DetectSequences
    {
        public static void Do(AutoResetEvent quit)
        {
            var map = new Dictionary<Sequence, Action>
            {
                {Sequence.FromString("Control+Z,B")  , Console.WriteLine},
                {Sequence.FromString("Control+Z,Z")  , Console.WriteLine},
               {Sequence.FromString("Escape,Escape,Escape") , () => quit.Set()},
            };

            Console.WriteLine("Detecting following combinations:");
            foreach (var sequence in map.Keys)
            {
                Console.WriteLine("\t{0}", sequence);
            }
            Console.WriteLine("Press 3 x ESC (three times) to exit.");

            //Actual loop
            Hook.GlobalEvents().OnSequence(map);
        }

    }
}

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

    internal class LogKeys
    {
        public static void Do(AutoResetEvent quit)
        {
            Hook.GlobalEvents().KeyPress += (sender, e) =>
            {
                Console.Write(e.KeyChar);
                if (e.KeyChar == 'q') quit.Set();
            };
        }
    }
