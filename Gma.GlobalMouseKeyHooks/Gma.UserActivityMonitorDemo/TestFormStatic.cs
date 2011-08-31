using System;
using System.Windows.Forms;
using Gma.UserActivityMonitor;

namespace Gma.UserActivityMonitorDemo
{
    public partial class TestFormStatic : Form
    {
        public TestFormStatic()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //##################################################################
        #region Check boxes to set or remove particular event handlers.

        private void checkBoxOnMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseMove.Checked)
            {
                MouseHookManager.MouseMove += HookManager_MouseMove;
            }
            else
            {
                MouseHookManager.MouseMove -= HookManager_MouseMove;
            }
        }

        private void checkBoxOnMouseClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseClick.Checked)
            {
                MouseHookManager.MouseClick += HookManager_MouseClick;
            }
            else
            {
                MouseHookManager.MouseClick -= HookManager_MouseClick;
            }
        }

        private void checkBoxOnMouseUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseUp.Checked)
            {
                MouseHookManager.MouseUp += HookManager_MouseUp;
            }
            else
            {
                MouseHookManager.MouseUp -= HookManager_MouseUp;
            }
        }

        private void checkBoxOnMouseDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseDown.Checked)
            {
                MouseHookManager.MouseDown += HookManager_MouseDown;
            }
            else
            {
                MouseHookManager.MouseDown -= HookManager_MouseDown;
            }
        }

        private void checkBoxMouseDoubleClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseDoubleClick.Checked)
            {
                MouseHookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
            }
            else
            {
                MouseHookManager.MouseDoubleClick -= HookManager_MouseDoubleClick;
            }
        }

        private void checkBoxMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseWheel.Checked)
            {
                MouseHookManager.MouseWheel += HookManager_MouseWheel;
            }
            else
            {
                MouseHookManager.MouseWheel -= HookManager_MouseWheel;
            }
        }

        private void checkBoxKeyDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyDown.Checked)
            {
                KeyboardHookManager.KeyDown += HookManager_KeyDown;
            }
            else
            {
                KeyboardHookManager.KeyDown -= HookManager_KeyDown;
            }
        }


        private void checkBoxKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyUp.Checked)
            {
                KeyboardHookManager.KeyUp += HookManager_KeyUp;
            }
            else
            {
                KeyboardHookManager.KeyUp -= HookManager_KeyUp;
            }
        }

        private void checkBoxKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyPress.Checked)
            {
                KeyboardHookManager.KeyPress += HookManager_KeyPress;
            }
            else
            {
                KeyboardHookManager.KeyPress -= HookManager_KeyPress;
            }
        }

        #endregion

        //##################################################################
        #region Event handlers of particular events. They will be activated when an appropriate checkbox is checked.

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxLog.AppendText(string.Format("KeyDown - {0}\n", e.KeyCode));
            textBoxLog.ScrollToCaret();
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxLog.AppendText(string.Format("KeyUp - {0}\n", e.KeyCode));
            textBoxLog.ScrollToCaret();
        }


        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxLog.AppendText(string.Format("KeyPress - {0}\n", e.KeyChar));
            textBoxLog.ScrollToCaret();
        } 


        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxLog.AppendText(string.Format("MouseClick - {0}\n", e.Button));
            textBoxLog.ScrollToCaret();
        }


        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxLog.AppendText(string.Format("MouseUp - {0}\n", e.Button));
            textBoxLog.ScrollToCaret();
        }


        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxLog.AppendText(string.Format("MouseDown - {0}\n", e.Button));
            textBoxLog.ScrollToCaret();
        }


        private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxLog.AppendText(string.Format("MouseDoubleClick - {0}\n", e.Button));
            textBoxLog.ScrollToCaret();
        }


        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }

        #endregion
    }
}