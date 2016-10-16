﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gma.System.MouseKeyHook.HotKeys;

namespace Gma.System.MouseKeyHook
{
    /// <summary>
    ///     Provides hotkey managing
    /// </summary>
    public interface IHotkeyManager : IDisposable
    {
        /// <summary>
        ///     Add a HotKeySet to the HotSetKetCollection.  
        /// </summary>
        /// <param name="hotKeySet">HotKeySet to add</param>
        void AddHotKeySet(HotKeySet hotKeySet);
        /// <summary>
        ///     Remove a HotKeySet from the collection.
        /// </summary>
        /// <param name="hotKeySet"></param>
        void RemoveHotKeySet(HotKeySet hotKeySet);

        /// <summary>
        ///     Filters HotKeySets based on the predicate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>Filtered HotKeySet IEnumerable</returns>
        IEnumerable<HotKeySet> FindHotKeySetsWhere(Func<HotKeySet, bool> predicate);
    }
}