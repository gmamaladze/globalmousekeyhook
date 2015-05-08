// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation;

namespace Gma.System.MouseKeyHook.Demo
{
    public partial class TestFormHookListeners : Form
    {
        private readonly IKeyboardMouseEvents m_Events;

        public TestFormHookListeners()
        {
            InitializeComponent();
            m_Events = Hook.GlobalEvents();
        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            Log("Suppressed.\n");
            e.Handled = true;
        }

        //##################################################################

        #region Check boxes to set or remove particular event handlers.

        private void checkBoxOnMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseMove.Checked)
            {
                m_Events.MouseMove += HookManager_MouseMove;
            }
            else
            {
                m_Events.MouseMove -= HookManager_MouseMove;
            }
        }

        private void checkBoxOnMouseUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseUp.Checked)
            {
                m_Events.MouseUp += HookManager_MouseUp;
            }
            else
            {
                m_Events.MouseUp -= HookManager_MouseUp;
            }
        }

        private void checkBoxOnMouseDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseDown.Checked)
            {
                m_Events.MouseDown += HookManager_MouseDown;
            }
            else
            {
                m_Events.MouseDown -= HookManager_MouseDown;
            }
        }

        private void checkBoxMouseDoubleClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseDoubleClick.Checked)
            {
                m_Events.MouseDoubleClick += HookManager_MouseDoubleClick;
            }
            else
            {
                m_Events.MouseDoubleClick -= HookManager_MouseDoubleClick;
            }
        }

        private void checkBoxMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseWheel.Checked)
            {
                m_Events.MouseWheel += HookManager_MouseWheel;
            }
            else
            {
                m_Events.MouseWheel -= HookManager_MouseWheel;
            }
        }

        private void checkBoxSuppressMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSuppressMouse.Checked)
            {
                m_Events.MouseDownExt += HookManager_Supress;
            }
            else
            {
                m_Events.MouseDownExt -= HookManager_Supress;
            }
        }

        private void checkBoxKeyDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyDown.Checked)
            {
                m_Events.KeyDown += HookManager_KeyDown;
            }
            else
            {
                m_Events.KeyDown -= HookManager_KeyDown;
            }
        }


        private void checkBoxKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyUp.Checked)
            {
                m_Events.KeyUp += HookManager_KeyUp;
            }
            else
            {
                m_Events.KeyUp -= HookManager_KeyUp;
            }
        }

        private void checkBoxKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyPress.Checked)
            {
                m_Events.KeyPress += HookManager_KeyPress;
            }
            else
            {
                m_Events.KeyPress -= HookManager_KeyPress;
            }
        }

        #endregion

        //##################################################################

        #region Event handlers of particular events. They will be activated when an appropriate check box is checked.

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyDown \t\t {0}\n", e.KeyCode));
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
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

        private void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseClick \t\t {0}\n", e.Button));
        }

        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseUp \t\t {0}\n", e.Button));
        }


        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseDown \t\t {0}\n", e.Button));
        }


        private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseDoubleClick \t\t {0}\n", e.Button));
        }


        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }

        private void Log(string text)
        {
            textBoxLog.AppendText(text);
            textBoxLog.ScrollToCaret();
        }

        #endregion
    }
}