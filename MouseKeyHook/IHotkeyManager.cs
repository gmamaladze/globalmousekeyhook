using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gma.System.MouseKeyHook.HotKeys;

namespace Gma.System.MouseKeyHook
{
    /// <summary>
    ///     Provides hotkey managing
    /// </summary>
    public interface IHotkeyManager
    {
        /// <summary>
        ///     Adds a HotKeySet to the HotSetKetCollection. 
        /// </summary>
        /// <param name="hotKeySet">hotkey</param>
        /// <returns></returns>
        bool AddHotKeySet(HotKeySet hotKeySet);
        void RemoveHotKeySet(HotKeySet hotKeySet);
    }
}
