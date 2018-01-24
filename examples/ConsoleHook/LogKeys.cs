using System;
using System.Threading;
using Gma.System.MouseKeyHook;

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