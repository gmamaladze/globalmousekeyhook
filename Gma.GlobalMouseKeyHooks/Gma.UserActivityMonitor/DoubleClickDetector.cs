using System;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// This class listens to <see cref="MouseHookListener.MouseClick"/> events.
    /// Detects two sequential cliks of the same mouse button during the time span defined for double click
    /// in system and fires <see cref="MouseDoubleClick"/> event.
    /// </summary>
    public class DoubleClickDetector : IDisposable
    {
        private readonly MouseHookListener m_ClickEventSource;
        private MouseEventHandler m_MouseDoubleClick;
        private Timer m_DoubleClickTimer;
        private MouseButtons m_PrevClickedButton;

        /// <summary>
        /// Initializes a new instance of <see cref="DoubleClickDetector"/>
        /// </summary>
        /// <param name="clickEventSource"></param>
        public DoubleClickDetector(MouseHookListener clickEventSource)
        {
            if (clickEventSource == null)
            {
                throw new ArgumentNullException("clickEventSource");
            }
            m_ClickEventSource = clickEventSource;
        }

        /// <summary>
        ///   Occurs when a double clicked was performed by the mouse.
        /// </summary>
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                if (m_MouseDoubleClick == null)
                {
                    m_ClickEventSource.MouseUp += OnMouseUp;
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
                m_MouseDoubleClick += value;
            }
            remove
            {
                if (m_MouseDoubleClick != null)
                {
                    m_MouseDoubleClick -= value;
                    if (m_MouseDoubleClick == null)
                    {
                        m_ClickEventSource.MouseUp -= OnMouseUp;
                        //Dispose the timer
                        m_DoubleClickTimer.Tick -= DoubleClickTimeElapsed;
                        m_DoubleClickTimer = null;
                    }
                }
            }
        }

        private void DoubleClickTimeElapsed(object sender, EventArgs e)
        {
            //Timer is alapsed and no second click ocuured
            m_DoubleClickTimer.Enabled = false;
            m_PrevClickedButton = MouseButtons.None;
        }


        /// <summary>
        ///   This method is designed to monitor mouse clicks in order to fire a double click event if interval between 
        ///   clicks was short enaugh.
        /// </summary>
        /// <param name = "sender">Is always null</param>
        /// <param name = "e">Some information about click heppened.</param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            //This should not heppen
            if (e.Clicks < 1)
            {
                return;
            }
            //If the secon click heppened on the same button
            if (e.Button.Equals(m_PrevClickedButton))
            {
                if (m_MouseDoubleClick != null)
                {
                    //Fire double click
                    m_MouseDoubleClick.Invoke(null, e);
                }
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
        }

        /// <summary>
        /// Stops and siposes the timer, releases delegates.
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
