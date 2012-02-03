using System.Runtime.InteropServices;

namespace MouseKeyboardActivityMonitor.WinApi
{
    internal static class ThreadNativeMethods
    {
        /// <summary>
        /// Retrieves the unmanaged thread identifier of the calling thread.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32")]
        internal static extern int GetCurrentThreadId();
    }
}