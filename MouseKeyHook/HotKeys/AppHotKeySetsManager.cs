// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

namespace Gma.System.MouseKeyHook.HotKeys
{
    class AppHotKeySetsManager : HotKeySetsManager
    {
        protected override HotKeySetsListener GeyHotKeySetsListener(HotKeySetCollection collection)
        {
            return new AppHotKeySetsListener(collection);
        }
    }
}