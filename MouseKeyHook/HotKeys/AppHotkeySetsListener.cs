using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gma.System.MouseKeyHook.Implementation;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal class AppHotKeySetsListener : HotKeySetsListener
    {
        public AppHotKeySetsListener(HotKeySetCollection collection)
            : base(HookHelper.HookAppKeyboard, collection)
        {
        }

        protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data)
        {
            return KeyPressEventArgsExt.FromRawDataApp(data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
        {
            return KeyEventArgsExt.FromRawDataApp(data);
        }
    }
}