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
        private readonly HotKeySetCollection m_Collection = new HotKeySetCollection();
        private readonly HotKeySetsListener m_KeyListener;

        internal HotKeySetsManager()
        {
            m_KeyListener = GeyHotKeySetsListener(m_Collection);
        }

        public void AddHotKeySet(HotKeySet hotKeySet)
        {
            if (hotKeySet == null)
                return;
            m_Collection.Add(hotKeySet);
        }

        public void RemoveHotKeySet(HotKeySet hotKeySet)
        {
            m_Collection.Remove(hotKeySet);
        }

        public IEnumerable<HotKeySet> FindHotKeySetsWhere(Func<HotKeySet, bool> predicate)
        {
            return m_Collection.Where(predicate).ToArray();
        }

        public void Dispose()
        {
            m_KeyListener.Dispose();
        }

        protected abstract HotKeySetsListener GeyHotKeySetsListener(HotKeySetCollection collection);
    }
}