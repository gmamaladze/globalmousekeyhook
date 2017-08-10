﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using Gma.System.MouseKeyHook.Implementation;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal abstract class HotKeySetsListener : KeyListener
    {
        internal HotKeySetsListener(Subscribe subscribe) : base(subscribe)
        {
            Collection = new HotKeySetCollection();
        }

        internal HotKeySetCollection Collection { get; private set; }

        protected override bool Callback(CallbackData data)
        {
            var eDownUp = GetDownUpEventArgs(data);
            Collection.OnKey(eDownUp);
            return !eDownUp.Handled;
        }
    }
}