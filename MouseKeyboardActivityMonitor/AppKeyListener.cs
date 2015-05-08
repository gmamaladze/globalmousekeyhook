using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor
{
    internal class AppKeyListener : KeyListener
    {
        public AppKeyListener() 
            : base(HookHelper.HookAppKeyboard)
        {
        }

        protected override KeyPressEventArgsExt GetPressEventArgs(CallbackData data)
        {
            return KeyPressEventArgsExt.FromRawDataApp(data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
        {
            return KeyEventArgsExt.FromRawDataApp(data);
        }
    }
}