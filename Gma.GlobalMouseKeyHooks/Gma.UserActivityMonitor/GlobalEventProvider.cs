using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// This component monitors all mouse activities globally (also outside of the application) 
    /// and provides appropriate events.
    /// </summary>
    public class GlobalEventProvider : Component
    {
        /// <summary>
        /// This component raises events. The value is always true.
        /// </summary>
        protected override bool CanRaiseEvents
        {
            get
            {
                return true;
            }
        }

        //################################################################
        #region Mouse events

        private event MouseEventHandler m_MouseMove;

        /// <summary>
        /// Occurs when the mouse pointer is moved. 
        /// </summary>
        public event MouseEventHandler MouseMove
        {
            add
            {
                if (m_MouseMove == null)
                {
                    HookManager.MouseMove += HookManager_MouseMove;
                }
                m_MouseMove += value;
            }

            remove
            {
                m_MouseMove -= value;
                if (m_MouseMove == null)
                {
                    HookManager.MouseMove -= HookManager_MouseMove;
                }
            }
        }

        void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseMove != null)
            {
                m_MouseMove.Invoke(this, e);
            }
        }

        private event MouseEventHandler m_MouseClick;
        /// <summary>
        /// Occurs when a click was performed by the mouse. 
        /// </summary>
        public event MouseEventHandler MouseClick
        {
            add
            {
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick += HookManager_MouseClick;
                }
                m_MouseClick += value;
            }

            remove
            {
                m_MouseClick -= value;
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick -= HookManager_MouseClick;
                }
            }
        }

        void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_MouseClick != null)
            {
                m_MouseClick.Invoke(this, e);
            }
        }

        private event MouseEventHandler m_MouseDown;

        /// <summary>
        /// Occurs when the mouse a mouse button is pressed. 
        /// </summary>
        public event MouseEventHandler MouseDown
        {
            add
            {
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown += HookManager_MouseDown;
                }
                m_MouseDown += value;
            }

            remove
            {
                m_MouseDown -= value;
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown -= HookManager_MouseDown;
                }
            }
        }

        void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_MouseDown != null)
            {
                m_MouseDown.Invoke(this, e);
            }
        }


        private event MouseEventHandler m_MouseUp;

        /// <summary>
        /// Occurs when a mouse button is released. 
        /// </summary>
        public event MouseEventHandler MouseUp
        {
            add
            {
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp += HookManager_MouseUp;
                }
                m_MouseUp += value;
            }

            remove
            {
                m_MouseUp -= value;
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp -= HookManager_MouseUp;
                }
            }
        }

        void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_MouseUp != null)
            {
                m_MouseUp.Invoke(this, e);
            }
        }

        private event MouseEventHandler m_MouseDoubleClick;

        /// <summary>
        /// Occurs when a double clicked was performed by the mouse. 
        /// </summary>
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
                }
                m_MouseDoubleClick += value;
            }

            remove
            {
                m_MouseDoubleClick -= value;
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick -= HookManager_MouseDoubleClick;
                }
            }
        }

        void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_MouseDoubleClick != null)
            {
                m_MouseDoubleClick.Invoke(this, e);
            }
        }


        private event EventHandler<MouseEventExtArgs> m_MouseMoveExt;

        /// <summary>
        /// Occurs when the mouse pointer is moved. 
        /// </summary>
        /// <remarks>
        /// This event provides extended arguments of type <see cref="MouseEventArgs"/> enabling you to 
        /// supress further processing of mouse movement in other applications.
        /// </remarks>
        public event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt += HookManager_MouseMoveExt;
                }
                m_MouseMoveExt += value;
            }

            remove
            {
                m_MouseMoveExt -= value;
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt -= HookManager_MouseMoveExt;
                }
            }
        }

        void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            if (m_MouseMoveExt != null)
            {
                m_MouseMoveExt.Invoke(this, e);
            }
        }

        private event EventHandler<MouseEventExtArgs> m_MouseClickExt;

        /// <summary>
        /// Occurs when a click was performed by the mouse. 
        /// </summary>
        /// <remarks>
        /// This event provides extended arguments of type <see cref="MouseEventArgs"/> enabling you to 
        /// supress further processing of mouse click in other applications.
        /// </remarks>
        public event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt += HookManager_MouseClickExt;
                }
                m_MouseClickExt += value;
            }

            remove
            {
                m_MouseClickExt -= value;
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt -= HookManager_MouseClickExt;
                }
            }
        }

        void HookManager_MouseClickExt(object sender, MouseEventExtArgs e)
        {
            if (m_MouseClickExt != null)
            {
                m_MouseClickExt.Invoke(this, e);
            }
        }


        #endregion

        //################################################################
        #region Keyboard events

        private event KeyPressEventHandler m_KeyPress;

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <remarks>
        /// Key events occur in the following order: 
        /// <list type="number">
        /// <item>KeyDown</item>
        /// <item>KeyPress</item>
        /// <item>KeyUp</item>
        /// </list>
        ///The KeyPress event is not raised by noncharacter keys; however, the noncharacter keys do raise the KeyDown and KeyUp events. 
        ///Use the KeyChar property to sample keystrokes at run time and to consume or modify a subset of common keystrokes. 
        ///To handle keyboard events only in your application and not enable other applications to receive keyboard events, 
        /// set the KeyPressEventArgs.Handled property in your form's KeyPress event-handling method to <b>true</b>. 
        /// </remarks>
        public event KeyPressEventHandler KeyPress
        {
            add
            {
                if (m_KeyPress==null)
                {
                    HookManager.KeyPress +=HookManager_KeyPress;
                }
                m_KeyPress += value;
            }
            remove
            {
                m_KeyPress -= value;
                if (m_KeyPress == null)
                {
                    HookManager.KeyPress -= HookManager_KeyPress;
                }
            }
        }

        void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_KeyPress != null)
            {
                m_KeyPress.Invoke(this, e);
            }
        }

        private event KeyEventHandler m_KeyUp;

        /// <summary>
        /// Occurs when a key is released. 
        /// </summary>
        public event KeyEventHandler KeyUp
        {
            add
            {
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp += HookManager_KeyUp;
                }
                m_KeyUp += value;
            }
            remove
            {
                m_KeyUp -= value;
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp -= HookManager_KeyUp;
                }
            }
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (m_KeyUp != null)
            {
                m_KeyUp.Invoke(this, e);
            }
        }

        private event KeyEventHandler m_KeyDown;

        /// <summary>
        /// Occurs when a key is preseed. 
        /// </summary>
        public event KeyEventHandler KeyDown
        {
            add
            {
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown += HookManager_KeyDown;
                }
                m_KeyDown += value;
            }
            remove
            {
                m_KeyDown -= value;
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown -= HookManager_KeyDown;
                }
            }
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            m_KeyDown.Invoke(this, e);
        }

        #endregion

        
    }
}
