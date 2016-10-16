using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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