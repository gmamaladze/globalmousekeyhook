using System;

namespace MouseKeyboardActivityMonitor.WinApi
{
    public class HookEventArgs : EventArgs
    {
        private readonly IntPtr m_LParam;
        private readonly int m_WParam;
        private bool m_Cancel;

        public HookEventArgs(int wParam, IntPtr lParam)
        {
            m_WParam = wParam;
            m_LParam = lParam;
            m_Cancel = false;
        }

        public int WParam
        {
            get { return m_WParam; }
        }

        public IntPtr LParam
        {
            get { return m_LParam; }
        }

        public bool Cancel
        {
            get { return m_Cancel; }
            set { m_Cancel = value; }
        }
    }
}