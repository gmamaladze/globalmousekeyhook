// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal abstract class HotKeySetsListener : BaseListener
    {
        private HotKeySetCollection Collection { get; set; }
        internal HotKeySetsListener(Subscribe subscribe, HotKeySetCollection collection)
            : base(subscribe)
        {
            Collection = collection;
        }

        protected override bool Callback(CallbackData data)
        {
            var eDownUp = GetDownUpEventArgs(data);
            Collection.OnKey(eDownUp);
            return !eDownUp.Handled;
        }

        protected abstract KeyEventArgsExt GetDownUpEventArgs(CallbackData data);
    }
}
