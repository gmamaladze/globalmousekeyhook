using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static Gma.System.MouseKeyHook.WinApi.KeyboardNativeMethods;

namespace Gma.System.MouseKeyHook
{
    internal static class Shared
    {        
        public static Stack<Keys> FromString(string str)
        {
            IEnumerable<Keys> keys =
                str
                    .Split('+')
                    .Select(p =>
                    {
                        if (p.Length == 1 && TryGetKeyFromChar(p.First(), out Keys key))
                            return key;
                        else
                            return (Keys)Enum.Parse(typeof(Keys), p, false);

                    });
            return new Stack<Keys>(keys);
        }
    }
}
