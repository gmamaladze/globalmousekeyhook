using System;
using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor
{

    internal abstract class BaseListener : IDisposable
    {
        protected HookResult Handle { get; set; }

        protected BaseListener(Subscribe subscribe)
        {
            Handle = subscribe(Callback);
        }

        protected abstract bool Callback(CallbackData data);

        public void Dispose()
        {
            Handle.Dispose();
        }
    }
}