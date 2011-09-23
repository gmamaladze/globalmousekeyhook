using System.Runtime.InteropServices;

namespace MouseKeyboardActivityMonitor.WinApi
{
    /// <summary>
    /// The MouseHookStruct structure contains information about a low-level mouse input event. 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/ms644970(VS.85).aspx
    /// <para>
    /// In 64-bit Windows, the 
    ///     <see cref="MouseHookStruct.MouseData"/>,
    ///     <see cref="MouseHookStruct.Flags"/>,
    ///     <see cref="MouseHookStruct.Time"/>, and
    ///     <see cref="MouseHookStruct.ExtraInfo"/>
    /// are all 8 byte datatypes.
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseHookStruct
    {
        /// <summary>
        /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
        /// </summary>
        public Point Point;

        /// <summary>
        /// Specifies information associated with the message.
        /// </summary>
        /// <remarks>
        /// If the hook is global and the message is <see cref="Messages.WM_MOUSEWHEEL"/>, the high order word is the wheel delta.
        /// Positive value indicates a forward scroll, and negative value indicates a scroll toward the user.
        /// <para>
        /// If the message is <see cref="Messages.WM_XBUTTONDOWN"/>, <see cref="Messages.WM_XBUTTONUP"/>, or <see cref="Messages.WM_XBUTTONDBLCLK"/>
        /// the high-order word specifies which X button was pressed or released.
        /// </para>
        /// </remarks>
        public System.IntPtr MouseData;

        /// <summary>
        /// Specifies the event-injected flag.
        /// </summary>
        public System.IntPtr Flags;

        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public System.IntPtr Time;

        /// <summary>
        /// Specifies extra information associated with the message. 
        /// </summary>
        /// <remarks>
        /// If the hook is local and the message is <see cref="Messages.WM_MOUSEWHEEL"/>, the high order word is the wheel delta.
        /// </remarks>
        public System.IntPtr ExtraInfo;
    }
}