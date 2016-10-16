using System.Collections.Generic;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.HotKeys
{
    internal class GlobalHotKeySetsListener : HotKeySetsListener
    {
        public GlobalHotKeySetsListener(HotKeySetCollection collection)
            : base(HookHelper.HookGlobalKeyboard, collection)
        {
        }

        protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data)
        {
            return KeyPressEventArgsExt.FromRawDataGlobal(data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
        {
            return KeyEventArgsExt.FromRawDataGlobal(data);
        }
    }
}