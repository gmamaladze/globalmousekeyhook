using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MouseKeyboardActivityMonitor;

namespace FormComponentExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        #region Mouse Handlers

        private void inputEventsProvider_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                e.Handled = true;
                txtOutput.AppendText("Suppressed.\n");
            }
            else
            {
                txtOutput.AppendText(string.Format("MouseDown:\t\t{0}\n", e.Button));
            }
            txtOutput.ScrollToCaret();
        }

        private void inputEventsProvider_MouseClickExt(object sender, MouseEventExtArgs e)
        {
            txtOutput.AppendText(string.Format("MouseClick:\t\t{0}\n", e.Button));
            txtOutput.ScrollToCaret();
        }

        private void inputEventsProvider_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtOutput.AppendText(string.Format("MouseDoubleClick:\t\t\t{0}\n", e.Button));
            txtOutput.ScrollToCaret();
        }
               

        private void inputEventsProvider_MouseUp(object sender, MouseEventArgs e)
        {
            txtOutput.AppendText(string.Format("MouseUp:\t\t{0}\n", e.Button));
            txtOutput.ScrollToCaret();
        }

        private void inputEventsProvider_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            labelPosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void inputEventsProvider_MouseWheel(object sender, MouseEventArgs e)
        {
            labelDelta.Text = e.Delta.ToString();
        }

        #endregion
        
        #region Keyboard Handlers

        private void inputEventsProvider_KeyDown(object sender, KeyEventArgs e)
        {
            txtOutput.AppendText(string.Format("KeyDown:\t{0}\n", e.KeyData));
            txtOutput.ScrollToCaret();
        }

        private void inputEventsProvider_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtOutput.AppendText(string.Format("KeyPress:\t\t{0}\n", e.KeyChar));
            txtOutput.ScrollToCaret();
        }

        private void inputEventsProvider_KeyUp(object sender, KeyEventArgs e)
        {
            txtOutput.AppendText(string.Format("KeyUp:\t\t{0}\n", e.KeyData));
            txtOutput.ScrollToCaret();
        }

        #endregion

    }
}
