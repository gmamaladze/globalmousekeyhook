// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Windows.Forms;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.Implementation
{
    internal class GlobalMouseListener : MouseListener
    {
        private MouseButtons m_DownButtons;
        private MouseButtons m_PreviousClicked;
        private Point m_PreviousClickedPosition;
        private int m_PreviousClickedTime;

        public GlobalMouseListener()
            : base(HookHelper.HookGlobalMouse)
        {
            ResetDoubleClickWaiting();
        }

        protected override void ProcessMouseDown(ref MouseEventExtArgs e)
        {
            base.ProcessMouseDown(ref e);
            Set(e.Button);
        }

 
        protected override void ProcessMouseUp(ref MouseEventExtArgs e)
        {
            base.ProcessMouseUp(ref e);
            var isSupressed = e.Handled;
            if (isSupressed) return;
            var wasDownBeforeUp = IsSet(e.Button);
            if (wasDownBeforeUp) ProcessMouseClick(ref e);
            Unset(e.Button);
        }

        private void Set(MouseButtons button)
        {
            m_DownButtons |= button;
        }

        private void Unset(MouseButtons button)
        {
            m_DownButtons &= ~button;
        }

        private bool IsSet(MouseButtons button)
        {
            return (m_DownButtons & button) != MouseButtons.None;
        }

        protected override void ProcessMouseClick(ref MouseEventExtArgs e)
        {
            base.ProcessMouseClick(ref e);
        }

        private bool IsDoubleClickWaitingFor(MouseButtons button)
        {
            return (m_DownButtons & button) != MouseButtons.None;
        }

        private void StartNewDoubleClickWaiting(MouseEventExtArgs e)
        {
            m_PreviousClicked = e.Button;
            m_PreviousClickedPosition = e.Point;
            m_DownButtons = MouseButtons.None;
        }

        protected override MouseEventExtArgs GetEventArgs(CallbackData data)
        {
            return MouseEventExtArgs.FromRawDataApp(data);
        }

        private void ProcessPossibleDoubleClick(ref MouseEventExtArgs e)
        {
            if (IsDoubleClick(e.Button, e.Timestamp, e.Point))
            {
                e = e.ToDoubleClickEventArgs();
                ResetDoubleClickWaiting();
            }
            else
            {
                m_PreviousClickedTime = e.Timestamp;
            }
        }

        private void ResetDoubleClickWaiting()
        {
            m_DownButtons = MouseButtons.None;
            m_PreviousClicked = MouseButtons.None;
            m_PreviousClickedTime = 0;
        }

        private bool IsDoubleClick(MouseButtons button, int timestamp, Point pos)
        {
            return
                button == m_PreviousClicked &&
                pos == m_PreviousClickedPosition && // Click-move-click exception, see Patch 11222
                timestamp - m_PreviousClickedTime <= SystemDoubleClickTime; // Mouse.GetDoubleClickTime();
        }
    }
}