using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    /// <summary>
    /// This class monitors all mouse activities globally (also outside of the application) 
    /// and provides appropriate events.
    /// </summary>
    public class KeyboardHookListener : BaseHookListener
    {
        protected override bool ProcessCallback(int wParam, IntPtr lParam)
        {
            //read structure KeyboardHookStruct at lParam
            KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

            return 
                   InvokeKeyDown(wParam, keyboardHookStruct) ||
                   InvokeKeyPress(wParam, keyboardHookStruct) ||
                   InvokeKeyUp(wParam, keyboardHookStruct);
        }

        /// <summary>
        /// Occurs when a key is preseed. 
        /// </summary>
        public event KeyEventHandler KeyDown;

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
        public event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Occurs when a key is released. 
        /// </summary>
        public event KeyEventHandler KeyUp;

        private bool InvokeKeyDown(int wParam, KeyboardHookStruct keyboardHookStruct)
        {
            return Keyboard.IsKeyDown(wParam)
                       ? InvokeKeyEventHandler(KeyDown, keyboardHookStruct)
                       : false;
        }

        private bool InvokeKeyUp(int wParam, KeyboardHookStruct keyboardHookStruct)
        {
            return Keyboard.IsKeyUp(wParam)
                       ? InvokeKeyEventHandler(KeyUp, keyboardHookStruct)
                       : false;
        }

        private static bool InvokeKeyEventHandler(KeyEventHandler keyEventHandler, KeyboardHookStruct keyboardHookStruct)
        {
            if (keyEventHandler == null)
            {
                return false;
            }

            Keys keyData = (Keys)keyboardHookStruct.VirtualKeyCode;
            KeyEventArgs e = new KeyEventArgs(keyData);
            keyEventHandler.Invoke(null, e);
            return e.Handled;
        }

        private bool InvokeKeyPress(int wParam, KeyboardHookStruct keyboardHookStruct)
        {
            KeyPressEventHandler keyPress = KeyPress;
            if (keyPress == null || wParam != Messages.WM_KEYDOWN)
            {
                return false;
            }

            char ch;
            if (!Keyboard.TryGetCharFromKeyboardState(keyboardHookStruct, out ch))
            {
                return false;
            }

            KeyPressEventArgs e = new KeyPressEventArgs(ch);
            keyPress.Invoke(null, e);
            return e.Handled;
        }

        public override void Dispose()
        {
            KeyPress = null;
            KeyDown = null;
            KeyUp = null;

            base.Dispose();
        }

        /// <summary>
        /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        protected override int GetHookId()
        {
            return WH_KEYBOARD_LL;
        }
    }
}