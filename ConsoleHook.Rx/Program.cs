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
                {"2. Detect key combination (chord)", ChordSample},
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

        private static void ChordSample(AutoResetEvent quit)
        {
            var chords = new []
            {
                new Chord {Trigger = Keys.H, Modifiers = new [] {Keys.Alt, Keys.Shift}},
                new Chord {Trigger = Keys.Q, Modifiers = new [] {Keys.Control}}
            };

            Hook
                .GlobalEvents()
                .ChordObservable(chords)
                .Select(chord=>chord.ToString())
                .ForEachAsync(name =>
                {
                    if (name == "Control+Q") quit.Set();
                    Console.WriteLine(name);
                });

            Console.WriteLine("Press Ctrl+Q to quit.");
            Console.WriteLine("Monitoring folowing sequences:");
            foreach (var name in chords)
            {
                Console.WriteLine("\t" + name);
            }

        }

        private static void SequenceSample(AutoResetEvent quit)
        {
            var map = new Dictionary<KeySequence, string>
            {
                {KeySequence.Of(Keys.LControlKey.Down(), Keys.Q.Down()), "Ctrl+Q"},
                {KeySequence.Of(Keys.LMenu.Down(), Keys.LShiftKey.Down(), Keys.H.Down()), "Alt+Shift+H"},
                {KeySequence.Of(Keys.RShiftKey.Down(), Keys.Z.Down(), Keys.Z.Up(), Keys.Z.Down()), "Shift+Z,Z"}
            };


            Hook
                .GlobalEvents()
                .UpDownObservable()
                .KeySequenceMapped(map)
                .ForEachAsync(name =>
                {
                    if (name == "Ctrl+Q") quit.Set();
                    Console.WriteLine(name);
                });

            Console.WriteLine("Press Ctrl+Q to quit.");
            Console.WriteLine("Monitoring folowing sequences:");
            foreach (var name in map.Values)
            {
                Console.WriteLine("\t" + name);
            }
        }
    }
}
