// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Linq;
using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public class KeySequence : Sequence<Keys>
    {
        public KeySequence(params Keys[] keys)
            : base(keys.Select(k => k.Normalize()).ToArray())
        {
        }
    }
}