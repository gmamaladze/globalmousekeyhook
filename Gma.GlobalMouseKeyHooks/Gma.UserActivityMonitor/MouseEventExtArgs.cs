using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// Provides data for the MouseClickExt and MouseMoveExt events. It also provides a property Handled.
    /// Set this property to <b>true</b> to prevent further processing of the event in other applications.
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {

        internal static MouseEventExtArgs FromRawData(int wParam, MouseHookStruct mouseHookStruct)
        {
            MouseButtons button = MouseButtons.None;
            short mouseDelta = 0;
            int clickCount = 0;

            bool isMouseKeyDown = false;
            bool isMouseKeyUp = false;


            switch (wParam)
            {
                case Messages.WM_LBUTTONDOWN:
                    isMouseKeyDown = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case Messages.WM_LBUTTONUP:
                    isMouseKeyUp = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case Messages.WM_LBUTTONDBLCLK:
                    button = MouseButtons.Left;
                    clickCount = 2;
                    break;
                case Messages.WM_RBUTTONDOWN:
                    isMouseKeyDown = true;
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case Messages.WM_RBUTTONUP:
                    isMouseKeyUp = true;
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case Messages.WM_RBUTTONDBLCLK:
                    button = MouseButtons.Right;
                    clickCount = 2;
                    break;
                case Messages.WM_MOUSEWHEEL:
                    //If the message is WM_MOUSEWHEEL, the high-order word of MouseData member is the wheel delta. 
                    //One wheel click is defined as WHEEL_DELTA, which is 120. 
                    //(value >> 16) & 0xffff; retrieves the high-order word from the given 32-bit value
                    mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);
                    break;

                case Messages.WM_XBUTTONDOWN:
                    //If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, 
                    //or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
                    //and the low-order word is reserved. This value can be one or more of the following values. 
                    //Otherwise, MouseData is not used.
                    button = (mouseHookStruct.MouseData == 0x00010000) ? MouseButtons.XButton1 :
                                                                         MouseButtons.XButton2;
                    isMouseKeyDown = true;
                    clickCount = 1;
                    break;

                case Messages.WM_XBUTTONUP:
                    button = (mouseHookStruct.MouseData == 0x00010000) ? MouseButtons.XButton1 :
                                                                         MouseButtons.XButton2;
                    isMouseKeyUp = true;
                    clickCount = 1;
                    break;

                case Messages.WM_XBUTTONDBLCLK:
                    button = (mouseHookStruct.MouseData == 0x00010000) ? MouseButtons.XButton1 :
                                                                         MouseButtons.XButton2;
                    clickCount = 2;
                    break;
            }

            var e = new MouseEventExtArgs(
                button,
                clickCount,
                mouseHookStruct.Point.X,
                mouseHookStruct.Point.Y,
                mouseDelta);

            e.IsMouseKeyDown = isMouseKeyDown;
            e.IsMouseKeyUp = isMouseKeyUp;

            return e;
        }

        /// <summary>
        /// Initializes a new instance of the MouseEventArgs class. 
        /// </summary>
        /// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
        /// <param name="clicks">The number of times a mouse button was pressed.</param>
        /// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
        /// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
        /// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        {}

        /// <summary>
        /// Initializes a new instance of the MouseEventArgs class. 
        /// </summary>
        /// <param name="e">An ordinary <see cref="MouseEventArgs"/> argument to be extended.</param>
        internal MouseEventExtArgs(MouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {}

        /// <summary>
        /// Set this property to <b>true</b> inside your event handler to prevent further processing of the event in other applications.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// True if event contains information about wheel scroll.
        /// </summary>
        public bool WheelScrolled
        {
            get { return Delta != 0; }
        }

        /// <summary>
        /// True if event signals a click. False if it was only a move or wheel scroll.
        /// </summary>
        public bool Clicked
        {
            get { return Clicks > 0; }
        }

        /// <summary>
        /// True if event singnals mouse button down.
        /// </summary>
        public bool IsMouseKeyDown
        {
            get; private set;
        }

        /// <summary>
        /// True if event singnals mouse button up.
        /// </summary>
        public bool IsMouseKeyUp
        {
            get; private set;
        }
    }
}
