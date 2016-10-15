// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Linq;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal class HotKeySetsManager : IHotkeyManager, IDisposable
    {
        private readonly HotKeySetCollection m_Collection = new HotKeySetCollection();
        private HotKeySetsListener m_KeyListener;

        internal HotKeySetsManager(bool isGlobal)
        {
            m_KeyListener = isGlobal ? 
                new HotKeySetsListener(HookHelper.HookGlobalKeyboard, m_Collection) 
                : new HotKeySetsListener(HookHelper.HookAppKeyboard, m_Collection);
        }

        public bool AddHotKeySet(HotKeySet hotKeySet)
        {
            if ((hotKeySet == null)
                || m_Collection.Any(hks => hks.HotKeys.SequenceEqual(hotKeySet.HotKeys)))
                return false;
            m_Collection.Add(hotKeySet);
            return true;
        }

        public void RemoveHotKeySet(HotKeySet hotKeySet)
        {
            m_Collection.Remove(hotKeySet);
        }

        public void Dispose()
        {
            m_KeyListener.Dispose();
        }
    }
}