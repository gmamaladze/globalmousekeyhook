using System;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class BaseHookListener : IDisposable
    {
        private readonly BaseHooker m_Hooker;

        public BaseHookListener(BaseHooker hooker)
        {
            m_Hooker = hooker;
        }

        /// <summary>
        /// Stores the handle to the Keyboard or Mouse hook procedure.
        /// </summary>
        protected int HookHandle { get; set; }

        /// <summary>
        /// Keeps the reference to prevent garbage collection of delegate. See: CallbackOnCollectedDelegate //TODO: URL
        /// </summary>
        protected HookCallback HookCallbackReferenceKeeper { get; set; }

        internal bool IsGlobal
        {
            get
            {
                return m_Hooker.IsGlobal;
            }
        }

        protected abstract bool ProcessCallback(int wParam, IntPtr lParam);

        /// <summary>
        /// A callback function which will be called every time a keyboard or mouse activity detected.
        /// <see cref="WinApi.HookCallback"/>
        /// </summary>
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
            return BaseHooker.CallNextHookEx(HookHandle, nCode, wParam, lParam);
        }

        public void Start()
        {
            HookCallbackReferenceKeeper = new HookCallback(HookCallback);
            try
            {
                HookHandle = m_Hooker.Subscribe(GetHookId(), HookCallbackReferenceKeeper);
            }
            catch(Exception)
            {
                HookCallbackReferenceKeeper = null;
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                m_Hooker.Unsubscribe(HookHandle);
            }
            finally
            {
                HookCallbackReferenceKeeper = null;
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
                BaseHooker.UnhookWindowsHookEx(HookHandle);
            }
        }
    }
}