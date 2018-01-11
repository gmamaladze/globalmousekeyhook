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
            var map = new Dictionary<Chord, string>
            {
                { Chord.Of(Keys.LControlKey.Down(), Keys.Q.Down()), "Ctrl+Q"},
                { Chord.Of(Keys.RControlKey.Down(), Keys.Q.Down()), "Ctrl+Q"},
                { Chord.Of(Keys.LMenu.Down(), Keys.LShiftKey.Down(), Keys.H.Down()), "Alt+Shift+H"},
                { Chord.Of(Keys.LShiftKey.Down(), Keys.Z.Down(), Keys.Z.Up(), Keys.Z.Down()), "Shift+Z,Z"},
                { Chord.Of(Keys.RShiftKey.Down(), Keys.Z.Down(), Keys.Z.Up(), Keys.Z.Down()), "Shift+Z,Z"}
            };


            var quit = new AutoResetEvent(false);

            Hook
                .GlobalEvents()
                .UpDownObservable()
                .ChordsMapped(map)
                .ForEachAsync(name =>
                {
                    if (name == "Ctrl+Q") quit.Set();
                    Console.WriteLine(name);
                });

            Console.WriteLine("Press Ctrl+Q to quit.");
            Console.WriteLine("Monitoring folowing key combinations:");
            foreach (var name in map.Values)
            {
                Console.WriteLine("\t"+name);
            }
            while (!quit.WaitOne(100))
            {
                Application.DoEvents();
            };
        }
    }
}
