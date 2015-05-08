using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor
{
    internal class AppMouseListener : MouseListener
    {
        public AppMouseListener() 
            : base(HookHelper.HookAppMouse)
        {
        }

        protected override MouseEventExtArgs GetEventArgs(CallbackData data)
        {
            return MouseEventExtArgs.FromRawDataApp(data);
        }
    }
}