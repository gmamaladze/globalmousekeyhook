// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;

namespace Gma.System.MouseKeyHook.WinApi
{
    internal struct CallbackData
    {
        private readonly IntPtr m_LParam;
        private readonly int m_WParam;

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