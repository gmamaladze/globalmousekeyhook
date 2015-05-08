using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace MouseKeyboardActivityMonitor.WinApi
{
    internal class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public HookProcedureHandle() 
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return HookNativeMethods.UnhookWindowsHookEx(handle)!=0;
        }
    }
}
