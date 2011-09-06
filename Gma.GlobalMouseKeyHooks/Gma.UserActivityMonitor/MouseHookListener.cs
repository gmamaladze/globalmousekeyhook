#region

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

#endregion

namespace Gma.UserActivityMonitor
{
    /// <summary>
    ///   TODO
    /// </summary>
    public class MouseHookListener : BaseHookListener, IMouseClickEventSource
    {

        private Point m_PreviousPosition = new Point(0,0);

        protected override bool ProcessCallback(int wParam, IntPtr lParam)
        {
            MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
            MouseEventExtArgs e = MouseEventExtArgs.FromRawData(wParam, mouseHookStruct);

            if (e.IsMouseKeyUp)
            {
                InvokeMouseEventHandler(MouseUp, e);
            }

            if (e.IsMouseKeyDown)
            {
                InvokeMouseEventHandler(MouseDown, e);
            }

            if (e.Clicked)
            {
                InvokeMouseEventHandler(MouseClick, e);
                InvokeMouseEventHandlerExt(MouseClickExt, e);
            }

            if (e.WheelScrolled)
            {
                InvokeMouseEventHandler(MouseWheel, e);
            }

            if (HasMoved(mouseHookStruct))
            {
                m_PreviousPosition = mouseHookStruct.Point;

                InvokeMouseEventHandler(MouseMove, e);
                InvokeMouseEventHandlerExt(MouseMoveExt, e);
            }

            return e.Handled;
        }

        protected override int GetHookId()
        {
            return Messages.WH_MOUSE_LL;
        }

        private bool HasMoved(MouseLLHookStruct mouseHookStruct)
        {
            return m_PreviousPosition != mouseHookStruct.Point;
        }

        private void InvokeMouseEventHandler(MouseEventHandler handler, MouseEventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void InvokeMouseEventHandlerExt(EventHandler<MouseEventExtArgs> handler, MouseEventExtArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        ///   Occurs when the mouse pointer is moved.
        /// </summary>
        public event MouseEventHandler MouseMove;

        /// <summary>
        ///   Occurs when the mouse pointer is moved.
        /// </summary>
        /// <remarks>
        ///   This event provides extended arguments of type <see cref = "MouseEventArgs" /> enabling you to 
        ///   supress further processing of mouse movement in other applications.
        /// </remarks>
        public event EventHandler<MouseEventExtArgs> MouseMoveExt;

        /// <summary>
        ///   Occurs when a click was performed by the mouse.
        /// </summary>
        public event MouseEventHandler MouseClick;

        /// <summary>
        ///   Occurs when a click was performed by the mouse.
        /// </summary>
        /// <remarks>
        ///   This event provides extended arguments of type <see cref = "MouseEventArgs" /> enabling you to 
        ///   supress further processing of mouse click in other applications.
        /// </remarks>
        public event EventHandler<MouseEventExtArgs> MouseClickExt;

        /// <summary>
        ///   Occurs when the mouse a mouse button is pressed.
        /// </summary>
        public event MouseEventHandler MouseDown;

        /// <summary>
        ///   Occurs when a mouse button is released.
        /// </summary>
        public event MouseEventHandler MouseUp;

        /// <summary>
        ///   Occurs when the mouse wheel moves.
        /// </summary>
        public event MouseEventHandler MouseWheel;
    }
}