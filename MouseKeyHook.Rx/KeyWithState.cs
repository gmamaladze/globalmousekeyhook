// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation;

namespace MouseKeyHook.Rx
{
    public class KeyWithState : Tuple<Keys, KeyboardState>
    {
        public KeyWithState(Keys key, KeyboardState state)
            : base(key, state)
        {
        }

        public Keys KeyCode => Item1;
        public KeyboardState State => Item2;

        public bool Matches(Trigger trigger)
        {
            return
                KeyCode == trigger.TriggerKey &&
                State.AreAllDown(trigger.AdditionalKeys);
        }
    }
}