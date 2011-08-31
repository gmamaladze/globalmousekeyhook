using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MouseHookManager
    {

        //##############################################################################
        #region Mouse hook processing

        /// <summary>
        /// A callback function which will be called every Time a mouse activity detected.
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
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //Marshall the data from callback.
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                //detect button clicked
                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                int clickCount = 0;
                bool mouseDown = false;
                bool mouseUp = false;

                switch (wParam)
                {
                    case Messages.WM_LBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case Messages.WM_LBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case Messages.WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case Messages.WM_RBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case Messages.WM_RBUTTONUP:
                        mouseUp = true;
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

                        //TODO: X BUTTONS (I havent them so was unable to test)
                        //If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, 
                        //or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
                        //and the low-order word is reserved. This value can be one or more of the following values. 
                        //Otherwise, MouseData is not used. 
                        break;
                }

                //generate event 
                MouseEventExtArgs e = new MouseEventExtArgs(
                    button,
                    clickCount,
                    mouseHookStruct.Point.X,
                    mouseHookStruct.Point.Y,
                    mouseDelta);

                //Mouse up
                if (MouseHookManager.s_MouseUp != null && mouseUp)
                {
                    MouseHookManager.s_MouseUp.Invoke(null, e);
                }

                //Mouse down
                if (MouseHookManager.s_MouseDown != null && mouseDown)
                {
                    MouseHookManager.s_MouseDown.Invoke(null, e);
                }

                //If someone listens to click and a click is heppened
                if (MouseHookManager.s_MouseClick != null && clickCount > 0)
                {
                    MouseHookManager.s_MouseClick.Invoke(null, e);
                }

                //If someone listens to click and a click is heppened
                if (MouseHookManager.s_MouseClickExt != null && clickCount > 0)
                {
                    MouseHookManager.s_MouseClickExt.Invoke(null, e);
                }

                //If someone listens to double click and a click is heppened
                if (MouseHookManager.s_MouseDoubleClick != null && clickCount == 2)
                {
                    MouseHookManager.s_MouseDoubleClick.Invoke(null, e);
                }

                //Wheel was moved
                if (MouseHookManager.s_MouseWheel != null && mouseDelta != 0)
                {
                    MouseHookManager.s_MouseWheel.Invoke(null, e);
                }

                //If someone listens to move and there was a change in coordinates raise move event
                if ((MouseHookManager.s_MouseMove != null || MouseHookManager.s_MouseMoveExt != null) && (MouseHookManager.m_OldX != mouseHookStruct.Point.X || MouseHookManager.m_OldY != mouseHookStruct.Point.Y))
                {
                    MouseHookManager.m_OldX = mouseHookStruct.Point.X;
                    MouseHookManager.m_OldY = mouseHookStruct.Point.Y;
                    if (MouseHookManager.s_MouseMove != null)
                    {
                        MouseHookManager.s_MouseMove.Invoke(null, e);
                    }

                    if (MouseHookManager.s_MouseMoveExt != null)
                    {
                        MouseHookManager.s_MouseMoveExt.Invoke(null, e);
                    }
                }

                if (e.Handled)
                {
                    return -1;
                }
            }

            //call next hook
            return Hooker.CallNextHookEx(MouseHookManager.s_MouseHookHandle, nCode, wParam, lParam);
        }

        #endregion

        /// <summary>
        /// This field is not objectively needed but we need to keep a reference on a delegate which will be 
        /// passed to unmanaged code. To avoid GC to clean it up.
        /// When passing delegates to unmanaged code, they must be kept alive by the managed application 
        /// until it is guaranteed that they will never be called.
        /// </summary>
        private static HookCallback s_MouseDelegate;

        /// <summary>
        /// Stores the handle to the mouse hook procedure.
        /// </summary>
        private static int s_MouseHookHandle;

        private static int m_OldX;
        private static int m_OldY;

        private static void EnsureSubscribedToGlobalMouseEvents()
        {
            // install Mouse hook only if it is not installed and must be installed
            if (s_MouseHookHandle == 0)
            {
                //See comment of this field. To avoid GC to clean it up.
                s_MouseDelegate = (new MouseHookManager()).MouseHookProc;
                //install hook
                s_MouseHookHandle = Hooker.SetWindowsHookEx(
                    Messages.WH_MOUSE_LL,
                    s_MouseDelegate,
                    Marshal.GetHINSTANCE(
                        Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                //If SetWindowsHookEx fails.
                if (s_MouseHookHandle == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //do cleanup

                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void TryUnsubscribeFromGlobalMouseEvents()
        {
            //if no subsribers are registered unsubsribe from hook
            if (s_MouseClick == null &&
                s_MouseDown == null &&
                s_MouseMove == null &&
                s_MouseUp == null &&
                s_MouseClickExt == null &&
                s_MouseMoveExt == null &&
                s_MouseWheel == null)
            {
                ForceUnsunscribeFromGlobalMouseEvents();
            }
        }

        private static void ForceUnsunscribeFromGlobalMouseEvents()
        {
            if (s_MouseHookHandle != 0)
            {
                //uninstall hook
                int result = Hooker.UnhookWindowsHookEx(s_MouseHookHandle);
                //reset invalid handle
                s_MouseHookHandle = 0;
                //Free up for GC
                s_MouseDelegate = null;
                //if failed and exception must be thrown
                if (result == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static event MouseEventHandler s_MouseMove;

        /// <summary>
        /// Occurs when the mouse pointer is moved. 
        /// </summary>
        public static event MouseEventHandler MouseMove
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseMove += value;
            }

            remove
            {
                s_MouseMove -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event EventHandler<MouseEventExtArgs> s_MouseMoveExt;

        /// <summary>
        /// Occurs when the mouse pointer is moved. 
        /// </summary>
        /// <remarks>
        /// This event provides extended arguments of type <see cref="MouseEventArgs"/> enabling you to 
        /// supress further processing of mouse movement in other applications.
        /// </remarks>
        public static event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseMoveExt += value;
            }

            remove
            {

                s_MouseMoveExt -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseClick;

        /// <summary>
        /// Occurs when a click was performed by the mouse. 
        /// </summary>
        public static event MouseEventHandler MouseClick
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseClick += value;
            }
            remove
            {
                s_MouseClick -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event EventHandler<MouseEventExtArgs> s_MouseClickExt;

        /// <summary>
        /// Occurs when a click was performed by the mouse. 
        /// </summary>
        /// <remarks>
        /// This event provides extended arguments of type <see cref="MouseEventArgs"/> enabling you to 
        /// supress further processing of mouse click in other applications.
        /// </remarks>
        public static event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseClickExt += value;
            }
            remove
            {
                s_MouseClickExt -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseDown;

        /// <summary>
        /// Occurs when the mouse a mouse button is pressed. 
        /// </summary>
        public static event MouseEventHandler  MouseDown
        {
            add 
            { 
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseDown += value;
            }
            remove
            {
                s_MouseDown -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseUp;

        /// <summary>
        /// Occurs when a mouse button is released. 
        /// </summary>
        public static event MouseEventHandler MouseUp
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseUp += value;
            }
            remove
            {
                s_MouseUp -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseWheel;

        /// <summary>
        /// Occurs when the mouse wheel moves. 
        /// </summary>
        public static event MouseEventHandler MouseWheel
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseWheel += value;
            }
            remove
            {
                s_MouseWheel -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseDoubleClick;

        /// <summary>
        /// Occurs when a double clicked was performed by the mouse. 
        /// </summary>
        public static event MouseEventHandler MouseDoubleClick
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                if (s_MouseDoubleClick == null)
                {
                    //We create a timer to monitor interval between two clicks
                    s_DoubleClickTimer = new Timer
                                             {
                                                 //This interval will be set to the value we retrive from windows. This is a windows setting from contro planel.
                                                 Interval = Mouse.GetDoubleClickTime(),
                                                 //We do not start timer yet. It will be start when the click occures.
                                                 Enabled = false
                                             };
                    //We define the callback function for the timer
                    s_DoubleClickTimer.Tick += DoubleClickTimeElapsed;
                    //We start to monitor mouse up event.
                    MouseUp += OnMouseUp;
                }
                s_MouseDoubleClick += value;
            }
            remove
            {
                if (s_MouseDoubleClick != null)
                {
                    s_MouseDoubleClick -= value;
                    if (s_MouseDoubleClick == null)
                    {
                        //Stop monitoring mouse up
                        MouseUp -= OnMouseUp;
                        //Dispose the timer
                        s_DoubleClickTimer.Tick -= DoubleClickTimeElapsed;
                        s_DoubleClickTimer = null;
                    }
                }
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static MouseButtons s_PrevClickedButton;
        private static Timer s_DoubleClickTimer;

        private static void DoubleClickTimeElapsed(object sender, EventArgs e)
        {
            //Timer is alapsed and no second click ocuured
            s_DoubleClickTimer.Enabled = false;
            s_PrevClickedButton = MouseButtons.None;
        }

        /// <summary>
        /// This method is designed to monitor mouse clicks in order to fire a double click event if interval between 
        /// clicks was short enaugh.
        /// </summary>
        /// <param name="sender">Is always null</param>
        /// <param name="e">Some information about click heppened.</param>
        private static void OnMouseUp(object sender, MouseEventArgs e)
        {
            //This should not heppen
            if (e.Clicks < 1) { return;}
            //If the secon click heppened on the same button
            if (e.Button.Equals(s_PrevClickedButton))
            {
                if (s_MouseDoubleClick!=null)
                {
                    //Fire double click
                    s_MouseDoubleClick.Invoke(null, e);
                }
                //Stop timer
                s_DoubleClickTimer.Enabled = false;
                s_PrevClickedButton = MouseButtons.None;
            }
            else
            {
                //If it was the firts click start the timer
                s_DoubleClickTimer.Enabled = true;
                s_PrevClickedButton = e.Button;
            }
        }
    }
}
