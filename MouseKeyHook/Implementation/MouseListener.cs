// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.Implementation
{
    internal abstract class MouseListener : BaseListener
    {
        private readonly int m_SystemDoubleClickTime;
        private Point m_PreviousPosition;
        private MouseButtons m_SuppressButtonUpFlags;

        protected MouseListener(Subscribe subscribe)
            : base(subscribe)
        {
            m_PreviousPosition = new Point(-1, -1);
            m_SuppressButtonUpFlags = MouseButtons.None;
            m_SystemDoubleClickTime = MouseNativeMethods.GetDoubleClickTime();
        }

        protected int SystemDoubleClickTime
        {
            get { return m_SystemDoubleClickTime; }
        }

        protected override bool Callback(CallbackData data)
        {
            var e = GetEventArgs(data);

            if (e.IsMouseKeyDown)
            {
                ProcessMouseDown(ref e);
            }

            if (e.Clicks == 1 && e.IsMouseKeyUp && !e.Handled)
            {
                ProcessMouseClick(ref e);
            }

            if (e.Clicks == 2 && !e.Handled)
            {
                OnMouseDoubleClick(e);
            }

            if (e.IsMouseKeyUp)
            {
                ProcessMouseUp(ref e);
            }

            if (e.WheelScrolled)
            {
                OnMouseWheel(e);
            }

            if (HasMoved(e.Point))
            {
                ProcessMouseMove(ref e);
            }

            return !e.Handled;
        }

        protected abstract MouseEventExtArgs GetEventArgs(CallbackData data);

        protected virtual void ProcessMouseDown(ref MouseEventExtArgs e)
        {
            OnMouseDown(e);
            OnMouseDownExt(e);
            if (e.Handled)
            {
                SetSupressButtonUpFlag(e.Button);
                e.Handled = true;
            }
        }

        protected virtual void ProcessMouseClick(ref MouseEventExtArgs e)
        {
            OnMouseClick(e);
        }

        protected virtual void ProcessMouseUp(ref MouseEventExtArgs e)
        {
            if (!HasSupressButtonUpFlag(e.Button))
            {
                OnMouseUp(e);
            }
            else
            {
                RemoveSupressButtonUpFlag(e.Button);
                e.Handled = true;
            }
        }

        private void ProcessMouseMove(ref MouseEventExtArgs e)
        {
            m_PreviousPosition = e.Point;

            OnMouseMove(e);
            OnMouseMoveExt(e);
        }

        private void RemoveSupressButtonUpFlag(MouseButtons button)
        {
            m_SuppressButtonUpFlags = m_SuppressButtonUpFlags ^ button;
        }

        private bool HasSupressButtonUpFlag(MouseButtons button)
        {
            return (m_SuppressButtonUpFlags & button) != 0;
        }

        private bool HasMoved(Point actualPoint)
        {
            return m_PreviousPosition != actualPoint;
        }

        private void SetSupressButtonUpFlag(MouseButtons button)
        {
            m_SuppressButtonUpFlags = m_SuppressButtonUpFlags | button;
        }

        public event MouseEventHandler MouseMove;
        public event EventHandler<MouseEventExtArgs> MouseMoveExt;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event EventHandler<MouseEventExtArgs> MouseDownExt;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseWheel;
        public event MouseEventHandler MouseDoubleClick;

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            var handler = MouseMove;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseMoveExt(MouseEventExtArgs e)
        {
            var handler = MouseMoveExt;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            var handler = MouseClick;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            var handler = MouseDown;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseDownExt(MouseEventExtArgs e)
        {
            var handler = MouseDownExt;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            var handler = MouseUp;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            var handler = MouseWheel;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
            var handler = MouseDoubleClick;
            if (handler != null) handler(this, e);
        }
    }
}