// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Threading;
using Gma.System.MouseKeyHook;

namespace ConsoleHook
{
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
}