// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor.NewApi;

namespace MouseKeyboardActivityMonitor
{
    internal abstract class EventFacade : IKeyboardMouseEvents
    {
        private static WeakReference _keyListenerCache = new WeakReference(null);
        private static WeakReference _mouseListenerCache = new WeakReference(null);

        public event KeyEventHandler KeyDown
        {
            add { GetKeyListener().KeyDown += value; }
            remove { GetKeyListener().KeyDown -= value; }
        }

        public event KeyPressEventHandler KeyPress
        {
            add { GetKeyListener().KeyPress += value; }
            remove { GetKeyListener().KeyPress -= value; }
        }

        public event KeyEventHandler KeyUp
        {
            add { GetKeyListener().KeyUp += value; }
            remove { GetKeyListener().KeyUp -= value; }
        }

        public event MouseEventHandler MouseMove
        {
            add { GetMouseListener().MouseMove += value; }
            remove { GetMouseListener().MouseMove -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add { GetMouseListener().MouseMoveExt += value; }
            remove { GetMouseListener().MouseMoveExt -= value; }
        }

        public event MouseEventHandler MouseClick
        {
            add { GetMouseListener().MouseClick += value; }
            remove { GetMouseListener().MouseClick -= value; }
        }

        public event MouseEventHandler MouseDown
        {
            add { GetMouseListener().MouseDown += value; }
            remove { GetMouseListener().MouseDown -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDownExt
        {
            add { GetMouseListener().MouseDownExt += value; }
            remove { GetMouseListener().MouseDownExt -= value; }
        }

        public event MouseEventHandler MouseUp
        {
            add { GetMouseListener().MouseUp += value; }
            remove { GetMouseListener().MouseUp -= value; }
        }

        public event MouseEventHandler MouseWheel
        {
            add { GetMouseListener().MouseWheel += value; }
            remove { GetMouseListener().MouseWheel -= value; }
        }

        public event MouseEventHandler MouseDoubleClick
        {
            add { GetMouseListener().MouseDoubleClick += value; }
            remove { GetMouseListener().MouseDoubleClick -= value; }
        }

        private KeyListener GetKeyListener()
        {
            var target = _keyListenerCache.Target as KeyListener;
            if (target != null) return target;
            target = CreateKeyListener();
            _keyListenerCache = new WeakReference(target);
            return target;
        }

        private MouseListener GetMouseListener()
        {
            var target = _mouseListenerCache.Target as MouseListener;
            if (target != null) return target;
            target = CreateMouseListener();
            _mouseListenerCache = new WeakReference(target);
            return target;
        }

        protected abstract MouseListener CreateMouseListener();
        protected abstract KeyListener CreateKeyListener();
    }
}