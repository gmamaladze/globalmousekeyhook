using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor
{
    internal class GlobalKeyListener : KeyListener
    {
        public GlobalKeyListener()
            : base(HookHelper.HookGlobalKeyboard)
        {
        }

        protected override KeyPressEventArgsExt GetPressEventArgs(CallbackData data)
        {
            return KeyPressEventArgsExt.FromRawDataGlobal(data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
        {
            return KeyEventArgsExt.FromRawDataGlobal(data);
        }
    }
}