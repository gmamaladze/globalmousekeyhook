// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor
{
    internal abstract class KeyListener : BaseListener, IKeyboardEvents
    {
        protected KeyListener(Subscribe subscribe) 
            : base(subscribe)
        {
        }

        public event KeyEventHandler KeyDown;

        public void InvokeKeyDown(KeyEventArgsExt e)
        {
            var handler = KeyDown;
            if (handler == null || e.Handled || !e.IsKeyDown)
            {
                return;
            }
            handler(this, e);
        }

        public event KeyPressEventHandler KeyPress;

        public void InvokeKeyPress(KeyPressEventArgsExt e)
        {
            var handler = KeyPress;
            if (handler == null || e.Handled || e.IsNonChar)
            {
                return;
            }
            handler(this, e);
        }

        public event KeyEventHandler KeyUp;

        public void InvokeKeyUp(KeyEventArgsExt e)
        {
            var handler = KeyUp;
            if (handler == null || e.Handled || !e.IsKeyUp)
            {
                return;
            }
            handler(this, e);
        }

        protected override bool Callback(CallbackData data)
        {
            var eDownUp = GetDownUpEventArgs(data);
            var ePress = GetPressEventArgs(data);

            InvokeKeyDown(eDownUp);
            InvokeKeyPress(ePress);
            InvokeKeyUp(eDownUp);

            return !eDownUp.Handled;
        }

        protected abstract KeyPressEventArgsExt GetPressEventArgs(CallbackData data);
        protected abstract KeyEventArgsExt GetDownUpEventArgs(CallbackData data);
    }
}