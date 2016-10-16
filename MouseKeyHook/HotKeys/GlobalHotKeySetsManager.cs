using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gma.System.MouseKeyHook.HotKeys
{
    class GlobalHotKeySetsManager : HotKeySetsManager
    {
        protected override HotKeySetsListener GeyHotKeySetsListener(HotKeySetCollection collection)
        {
            return new GlobalHotKeySetsListener(collection);
        }
    }
}