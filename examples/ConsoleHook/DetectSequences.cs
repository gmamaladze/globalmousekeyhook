using System;
using System.Collections.Generic;
using System.Threading;
using Gma.System.MouseKeyHook;

namespace ConsoleHook
{
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