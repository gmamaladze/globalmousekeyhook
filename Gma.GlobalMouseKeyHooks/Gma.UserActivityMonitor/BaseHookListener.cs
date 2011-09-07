using System;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// Base class used to implement mouse or keybord hook listeners.
    /// It provides base methods to subscribe and unsubscribe to hooks.
    /// Common processing, error handling and cleanup logic.
    /// </summary>
    public abstract class BaseHookListener : IDisposable
    {
        private Hooker m_Hooker;

        /// <summary>
        /// Base constructor of <see cref="BaseHookListener"/>
        /// </summary>
        /// <param name="hooker">Depending on this parameter the listener hooks either application or global keyboard events.</param>
        /// <remarks>Hooks are not active after instantiation. You need to use either <see cref="BaseHookListener.Enabled"/> property or call <see cref="BaseHookListener.Start"/> method.</remarks>
        protected BaseHookListener(Hooker hooker)
        {
            if (hooker == null)
            {
                throw new ArgumentNullException("hooker");
            }
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

        /// <summary>
        /// Override this method to modify logic of firing events.
        /// </summary>
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

        private int CallNextHook(int nCode, int wParam, IntPtr lParam)
        {
            return Hooker.CallNextHookEx(HookHandle, nCode, wParam, lParam);
        }

        /// <summary>
        /// Subscribes to the hook and starts firing events.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
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

        /// <summary>
        /// Unsubscribes from the hook and stops firing events.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
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

        /// <summary>
        /// Unsubscribes and subscribes again to the hooks.
        /// Enables you to switch from application hooks to global hooks and vice versa on the fly
        /// without unsubscribing from events.
        /// </summary>
        /// <param name="hooker"></param>
        public void Restart(Hooker hooker)
        {
            Stop();
            m_Hooker = hooker;
            Start();
        }

        /// <summary>
        /// Override to deliver correct id to be used for <see cref="Hooker.SetWindowsHookEx"/> call.
        /// </summary>
        /// <returns></returns>
        protected abstract int GetHookId();

        /// <summary>
        /// true - if component listens to hooks and fires appropriate ones.
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
        /// Release delegates, unsubscribes from hooks.
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