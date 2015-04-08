using System;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor.Demo
{
    public partial class TestFormHookListeners : Form
    {
        private readonly KeyboardHookListener m_KeyboardHookManager;
        private readonly MouseHookListener m_MouseHookManager;

        public TestFormHookListeners()
        {
            InitializeComponent();
            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            m_KeyboardHookManager.Enabled = true;

            m_MouseHookManager = new MouseHookListener(new GlobalHooker());
            m_MouseHookManager.Enabled = true;
        }


        //##################################################################
        #region Check boxes to set or remove particular event handlers.

        private void checkBoxOnMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseMove.Checked)
            {
                m_MouseHookManager.MouseMove += HookManager_MouseMove;
            }
            else
            {
                m_MouseHookManager.MouseMove -= HookManager_MouseMove;
            }
        }

        private void checkBoxOnMouseClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseClick.Checked)
            {
                m_MouseHookManager.MouseClickExt += HookManager_MouseClick;
            }
            else
            {
                checkBoxSuppressMouse.Checked = false;
                m_MouseHookManager.MouseClickExt -= HookManager_MouseClick;
            }
        }

        private void checkBoxOnMouseUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseUp.Checked)
            {
                m_MouseHookManager.MouseUp += HookManager_MouseUp;
            }
            else
            {
                m_MouseHookManager.MouseUp -= HookManager_MouseUp;
            }
        }

        private void checkBoxOnMouseDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseDown.Checked)
            {
                m_MouseHookManager.MouseDown += HookManager_MouseDown;
            }
            else
            {
                m_MouseHookManager.MouseDown -= HookManager_MouseDown;
            }
        }

        private void checkBoxMouseDoubleClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseDoubleClick.Checked)
            {
                m_MouseHookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
            }
            else
            {
                m_MouseHookManager.MouseDoubleClick -= HookManager_MouseDoubleClick;
            }
        }

        private void checkBoxMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseWheel.Checked)
            {
                m_MouseHookManager.MouseWheel += HookManager_MouseWheel;
            }
            else
            {
                m_MouseHookManager.MouseWheel -= HookManager_MouseWheel;
            }
        }

        private void checkBoxSuppressMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSuppressMouse.Checked)
            {
                m_MouseHookManager.MouseDownExt += HookManager_Supress;
            }
            else
            {
                m_MouseHookManager.MouseDownExt -= HookManager_Supress;
            }
        }

        private void checkBoxKeyDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyDown.Checked)
            {
                m_KeyboardHookManager.KeyDown += HookManager_KeyDown;
            }
            else
            {
                m_KeyboardHookManager.KeyDown -= HookManager_KeyDown;
            }
        }


        private void checkBoxKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyUp.Checked)
            {
                m_KeyboardHookManager.KeyUp += HookManager_KeyUp;
            }
            else
            {
                m_KeyboardHookManager.KeyUp -= HookManager_KeyUp;
            }
        }

        private void checkBoxKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyPress.Checked)
            {
                m_KeyboardHookManager.KeyPress += HookManager_KeyPress;
            }
            else
            {
                m_KeyboardHookManager.KeyPress -= HookManager_KeyPress;
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

        private void checkBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            m_MouseHookManager.Enabled = checkBoxEnabled.Checked;
            m_KeyboardHookManager.Enabled = checkBoxEnabled.Checked;
        }

        private void radioHooksType_CheckedChanged(object sender, EventArgs e)
        {
            Hooker hook;

            if (radioApplication.Checked)
            {
                hook = new AppHooker();
            }
            else
            {
                hook = new GlobalHooker();
            }

            m_KeyboardHookManager.Replace(hook);
            m_MouseHookManager.Replace(hook);
        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right) { return;}
            Log("Suppressed.\n");
            e.Handled = true;
        }
    }
}