using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gma.UserActivityMonitor.WinApi
{
    public class GlobalHooker : BaseHooker
    {
        internal override int Subscribe(int hookId, HookCallback hookCallback)
        {
            int hookHandle = SetWindowsHookEx(
                hookId,
                hookCallback,
                Process.GetCurrentProcess().MainModule.BaseAddress,
                0);

            if (hookHandle == 0)
            {
                ThrowLastUnmanagedErrorAsException();
            }

            return hookHandle;
        }

        internal override bool IsGlobal
        {
            get { return true; }
        }

        /// <summary>
        /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
        /// </summary>
        internal const int WH_MOUSE_LL = 14;

        /// <summary>
        /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        internal const int WH_KEYBOARD_LL = 13;
    }
}
