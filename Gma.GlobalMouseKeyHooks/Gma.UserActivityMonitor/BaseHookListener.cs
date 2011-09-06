using System;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class BaseHookListener : IDisposable
    {
        /// <summary>
        /// Stores the handle to the Keyboard or Mouse hook procedure.
        /// </summary>
        protected int HookHandle { get; set; }

        /// <summary>
        /// Keeps the reference to prevent garbage collection of delegate. See: CallbackOnCollectedDelegate //TODO: URL
        /// </summary>
        private HookCallback m_HookCallbackReferenceKeeper;

        protected abstract bool ProcessCallback(int wParam, IntPtr lParam);

        /// <summary>
        /// A callback function which will be called every Time a keyboard or mouse activity detected.
        /// </summary>
        /// <param name="nCode">
        /// [in] Specifies whether the hook procedure must process the message. 
        /// If nCode is HC_ACTION, the hook procedure must process the message. 
        /// If nCode is less than zero, the hook procedure must pass the message to the 
        /// CallNextHookEx function without further processing and must return the 
        /// value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies whether the message was sent by the current thread. 
        /// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
        /// </param>
        /// <param name="lParam">
        /// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
        /// </param>
        /// <returns>
        /// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
        /// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
        /// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
        /// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
        /// procedure does not call CallNextHookEx, the return value should be zero. 
        /// </returns>
        protected int HookCallback(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                bool handled = ProcessCallback(wParam, lParam);

                if (handled)
                {
                    return -1;
                }
            }

            return CallNextHook(nCode, wParam, lParam);
        }

        protected int CallNextHook(int nCode, int wParam, IntPtr lParam)
        {
            return Hooker.CallNextHookEx(HookHandle, nCode, wParam, lParam);
        }

        public void Start()
        {
            m_HookCallbackReferenceKeeper = new HookCallback(HookCallback);
            try
            {
                HookHandle = Hooker.Subscribe(GetHookId(), m_HookCallbackReferenceKeeper);
            }
            catch(Exception)
            {
                m_HookCallbackReferenceKeeper = null;
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                Hooker.Unsubscribe(HookHandle);
            }
            finally
            {
                m_HookCallbackReferenceKeeper = null;
            }
        }

        protected abstract int GetHookId();

        /// <summary>
        /// true - if component listens to global events and fires appropriate ones.
        /// false - otherwise.
        /// </summary>
        public bool Enabled
        {
            get { return HookHandle != 0; }
            set
            {
                bool mustEnable = value;
                if (mustEnable)
                {
                    if (!Enabled) 
                    {
                        Start();
                    }
                }
                else
                {
                    if (Enabled)
                    {
                        Stop();
                    }
                }
            }
        }

        /// <summary>
        /// Release delegates, unsubscribes from global hooks.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// Unsubscribes from global hooks skiping error handling.
        /// </summary>
        ~BaseHookListener()
        {
            if (HookHandle != 0)
            {
                Hooker.UnhookWindowsHookEx(HookHandle);
            }
        }
    }
}