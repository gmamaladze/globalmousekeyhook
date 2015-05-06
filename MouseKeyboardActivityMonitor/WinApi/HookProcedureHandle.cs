using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace MouseKeyboardActivityMonitor.WinApi
{
    /// <summary>
    /// TODO
    /// </summary>
    public class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// TODO
        /// </summary>
        public HookProcedureHandle() 
            : base(true)
        {
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            return HookNativeMethods.UnhookWindowsHookEx(this.DangerousGetHandle())!=0;
        }
    }
}
