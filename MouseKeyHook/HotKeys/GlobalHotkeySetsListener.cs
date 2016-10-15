using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gma.System.MouseKeyHook.Implementation;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal class GlobalHotkeySetsListener : HotKeySetsListener
    {
        public GlobalHotkeySetsListener(HotKeySetCollection collection) : base(HookHelper.HookGlobalKeyboard, collection)
        {
        }
        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
        {
            return KeyEventArgsExt.FromRawDataGlobal(data);
        }
    }
}
