using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MouseKeyboardActivityMonitor.WinApi
{
    internal static class HookIds
    {
        /// <summary>
        /// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure. 
        /// </summary>
        internal const int WH_MOUSE = 7;

        /// <summary>
        /// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure. 
        /// </summary>
        internal const int WH_KEYBOARD = 2;

        /// <summary>
        /// Windows NT/2000/XP/Vista/7: Installs a hook procedure that monitors low-level mouse input events.
        /// </summary>
        internal const int WH_MOUSE_LL = 14;

        /// <summary>
        /// Windows NT/2000/XP/Vista/7: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        internal const int WH_KEYBOARD_LL = 13;

    }
}
