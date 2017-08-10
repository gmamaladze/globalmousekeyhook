﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.HotKeys;
using Gma.System.MouseKeyHook.Implementation;

namespace Demo
{
    public partial class Main : Form
    {
        private IKeyboardMouseEvents m_Events;
        private IHotKeyManager _mHotKeyManager;

        public Main()
        {
            InitializeComponent();
            radioGlobal.Checked = true;
            SubscribeGlobal();
            FormClosing += Main_Closing;

           

        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeApplication()
        {
            Unsubscribe();
            Subscribe(Hook.AppEvents());
            SetupHotKeyManager(false);
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
            SetupHotKeyManager(true);
        }

        private void SetupHotKeyManager(bool isGlobal)
        {
            _mHotKeyManager = isGlobal ? Hook.GlobalHotkeyManager() : Hook.AppHotkeyManager();
            var hks = new HotKeySet(new[] { Keys.End, Keys.LControlKey, Keys.ControlKey });
            hks.RegisterExclusiveOrKey(new[] { Keys.LControlKey, Keys.ControlKey });
            hks.OnHotKeysDownOnce += (sender, args) =>
            {
                Log(string.Format("HotKeySet \t\t \"{0}\" -  {1}\n", hks.Name, string.Join("+", hks.HotKeys)));
            };
            labelHotkey.Text = string.Format("HotKey: {0}", string.Join(" + ", hks.HotKeys));
            _mHotKeyManager.AddHotKeySet(hks);
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            m_Events.MouseDragStarted += OnMouseDragStarted;
            m_Events.MouseDragFinished += OnMouseDragFinished;

            if (checkBoxSupressMouseWheel.Checked)
                m_Events.MouseWheelExt += HookManager_MouseWheelExt;
            else
                m_Events.MouseWheel += HookManager_MouseWheel;

            if (checkBoxSuppressMouse.Checked)
                m_Events.MouseDownExt += HookManager_Supress;
            else
                m_Events.MouseDown += OnMouseDown;
            
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.MouseUp -= OnMouseUp;
            m_Events.MouseClick -= OnMouseClick;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;

            m_Events.MouseMove -= HookManager_MouseMove;

            m_Events.MouseDragStarted -= OnMouseDragStarted;
            m_Events.MouseDragFinished -= OnMouseDragFinished;

            if (checkBoxSupressMouseWheel.Checked)
                m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
            else
                m_Events.MouseWheel -= HookManager_MouseWheel;

            if (checkBoxSuppressMouse.Checked)
                m_Events.MouseDownExt -= HookManager_Supress;
            else
                m_Events.MouseDown -= OnMouseDown;

            m_Events.Dispose();
            m_Events = null;

            _mHotKeyManager.Dispose();
            _mHotKeyManager = null;
        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                Log(string.Format("MouseDown \t\t {0}\n", e.Button));
                return;
            }

            Log(string.Format("MouseDown \t\t {0} Suppressed\n", e.Button));
            e.Handled = true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyDown  \t\t {0}\n", e.KeyCode));
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyUp  \t\t {0}\n", e.KeyCode));
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log(string.Format("KeyPress \t\t {0}\n", e.KeyChar));
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseDown \t\t {0}\n", e.Button));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseUp \t\t {0}\n", e.Button));
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseDoubleClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
            Log("MouseDragStarted\n");
        }

        private void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            Log("MouseDragFinished\n");
        }

        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }
        
        private void HookManager_MouseWheelExt(object sender, MouseEventExtArgs e)
        {
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
            Log("Mouse Wheel Move Suppressed.\n");
            e.Handled = true;
        }

        private void Log(string text)
        {
            if (IsDisposed) return;
            textBoxLog.AppendText(text);
            textBoxLog.ScrollToCaret();
        }

        private void radioApplication_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked) SubscribeApplication();
        }

        private void radioGlobal_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked) SubscribeGlobal();
        }

        private void radioNone_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked) Unsubscribe();
        }

        private void checkBoxSupressMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Events == null) return;

            if (((CheckBox) sender).Checked)
            {
                m_Events.MouseWheel -= HookManager_MouseWheel;
                m_Events.MouseWheelExt += HookManager_MouseWheelExt;
            }
            else
            {
                m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
                m_Events.MouseWheel += HookManager_MouseWheel;
            }
        }

        private void checkBoxSuppressMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Events == null) return;

            if (((CheckBox)sender).Checked)
            {
                m_Events.MouseDown -= OnMouseDown;
                m_Events.MouseDownExt += HookManager_Supress;
            }
            else
            {
                m_Events.MouseDownExt -= HookManager_Supress;
                m_Events.MouseDown += OnMouseDown;
            }
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
        }
    }
}