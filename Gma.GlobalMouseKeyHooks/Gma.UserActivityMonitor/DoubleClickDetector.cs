using System;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// This class listens to <see cref="MouseHookListener.MouseClick"/> events.
    /// Detects two sequential cliks of the same mouse button during the time span defined for double click
    /// in system and fires <see cref="MouseHookListener.MouseDoubleClick"/> event.
    /// </summary>
    internal class DoubleClickDetector : IDisposable
    {
        private Timer m_DoubleClickTimer;
        private MouseButtons m_PrevClickedButton;

        /// <summary>
        /// Initializes a new instance of <see cref="DoubleClickDetector"/>
        /// </summary>
        public DoubleClickDetector()
        {
            //We create a timer to monitor interval between two clicks
            m_DoubleClickTimer = new Timer
            {
                //This interval will be set to the value we retrive from windows. This is a windows setting from contro planel.
                Interval = Mouse.GetDoubleClickTime(),
                //We do not start timer yet. It will be start when the click occures.
                Enabled = false
            };
            //We define the callback function for the timer
            m_DoubleClickTimer.Tick += DoubleClickTimeElapsed;
        }

        private void DoubleClickTimeElapsed(object sender, EventArgs e)
        {
            // Timed out after one second. No 2nd click.
            m_DoubleClickTimer.Enabled = false;
            m_PrevClickedButton = MouseButtons.None;
        }


        /// <summary>
        /// This method is designed to monitor mouse clicks in order to fire a double click event if interval between 
        /// clicks was short enough.
        /// </summary>
        /// <param name = "e">Contains event data.</param>
        internal MouseEventExtArgs OnMouseDown(MouseEventExtArgs e)
        {
            //If the secon click happened on the same button
            if (e.Button.Equals(m_PrevClickedButton))
            {
                e = new MouseEventExtArgs(e.Button, 2, e.Point, e.Delta, e.IsMouseKeyDown, e.IsMouseKeyUp);
                //Stop timer
                m_DoubleClickTimer.Enabled = false;
                m_PrevClickedButton = MouseButtons.None;
            }
            else
            {
                //If it was the firts click start the timer
                m_DoubleClickTimer.Enabled = true;
                m_PrevClickedButton = e.Button;
            }
            return e;
        }

        /// <summary>
        /// Stops and disposes the timer, releases delegates.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (m_DoubleClickTimer != null)
            {
                m_DoubleClickTimer.Stop();
                m_DoubleClickTimer.Dispose();
                m_DoubleClickTimer = null;
            }
        }
    }
}
