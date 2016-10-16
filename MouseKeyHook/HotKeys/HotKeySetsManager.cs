// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal abstract class HotKeySetsManager : IHotKeyManager
    {
        private HotKeySetsListener m_KeyListener;

        public void AddHotKeySet(HotKeySet hotKeySet)
        {
            if (hotKeySet == null)
                return;
            GetListener().Collection.Add(hotKeySet);
        }

        public void RemoveHotKeySet(HotKeySet hotKeySet)
        {
            GetListener().Collection.Remove(hotKeySet);
        }

        public IEnumerable<HotKeySet> FindHotKeySetsWhere(Func<HotKeySet, bool> predicate)
        {
            return GetListener().Collection.Where(predicate).ToArray();
        }

        public void Dispose()
        {
            m_KeyListener.Dispose();
        }

        private HotKeySetsListener GetListener()
        {
            if(m_KeyListener != null)
                return m_KeyListener;
            m_KeyListener = GeyHotKeySetsListener();
            return m_KeyListener;
        }

        protected abstract HotKeySetsListener GeyHotKeySetsListener();
    }
}