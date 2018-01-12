// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using MouseKeyHook.Rx;
using System.Collections.Generic;

namespace ConsoleHook.Rx
{
    internal class DetectSequences
    {
        public static void Do(AutoResetEvent quit)
        {
            var expected = new HashSet<KeySequence>
            {
                new KeySequence(Keys.Control, Keys.Q),
                new KeySequence(Keys.Alt, Keys.Shift)
            };

            Hook
                .GlobalEvents()
                .KeyDownObservable()
                .Sequences(2,3)
                //.Matching(expected)
                .ForEachAsync(sequence =>
                {
                    if (sequence.ToString() == "Control+Q") quit.Set();
                    Console.WriteLine(sequence);
                });

            Console.WriteLine("Press Control+Q to quit.");
            Console.WriteLine("Monitoring folowing sequences:");
            foreach (var name in expected)
                Console.WriteLine("\t" + name);
        }
    }
}