using System;

namespace MouseKeyboardActivityMonitor.WinApi
{
    internal struct CallbackData
    {
        private readonly int m_WParam;
        private readonly IntPtr m_LParam;

        public CallbackData(int wParam, IntPtr lParam)
        {
            m_WParam = wParam;
            m_LParam = lParam;
        }

        public int WParam
        {
            get { return m_WParam; }
        }

        public IntPtr LParam
        {
            get { return m_LParam; }
        }
    }
}