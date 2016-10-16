// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Linq;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.HotKeys;

namespace Demo
{
    internal class Sample
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private IHotkeyManager m_HotkeyManager;
        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;

            // Just as with normal hooks, we instantiate the IHotKeyManager
            // from the Hook class
            m_HotkeyManager = Hook.GlobalHotkeyManager();
            var newHotkey = new HotKeySet(new[] {Keys.End, Keys.LControlKey, Keys.ControlKey})
            {
                Name = "Ctrl+End hook",
            };
            m_HotkeyManager.AddHotKeySet(newHotkey);
            newHotkey.OnHotKeysDownOnce += CtrlEndOnHotKeysDownOnce;
        }

        private void CtrlEndOnHotKeysDownOnce(object sender, HotKeyArgs hotKeyArgs)
        {
            MessageBox.Show("HotKey fired!");
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }
    }
}