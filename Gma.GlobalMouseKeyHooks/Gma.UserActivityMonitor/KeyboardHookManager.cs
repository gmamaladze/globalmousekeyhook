using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor {
    /// <summary>
    /// This class monitors all mouse activities globally (also outside of the application) 
    /// and provides appropriate events.
    /// </summary>
    public class KeyboardHookManager
    {

        //##############################################################################
        #region Keyboard hook processing

        /// <summary>
        /// This field is not objectively needed but we need to keep a reference on a delegate which will be 
        /// passed to unmanaged code. To avoid GC to clean it up.
        /// When passing delegates to unmanaged code, they must be kept alive by the managed application 
        /// until it is guaranteed that they will never be called.
        /// </summary>
        private static HookCallback s_KeyboardDelegate;

        /// <summary>
        /// Stores the handle to the Keyboard hook procedure.
        /// </summary>
        private static int s_KeyboardHookHandle;

        /// <summary>
        /// A callback function which will be called every Time a keyboard activity detected.
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
        private static int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //indicates if any of underlaing events set e.Handled flag
            

                //read structure KeyboardHookStruct at lParam
                KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
        
            bool handled = 
                !CanContinue(nCode) || 
                    InvokeKeyDown(wParam, keyboardHookStruct) || 
                    InvokeKeyPress(wParam, keyboardHookStruct) ||
                    InvokeKeyUp(wParam, keyboardHookStruct);


            //if event handled in application do not handoff to other listeners
            if (handled)
                return -1;

            //forward to other application
            return Hooker.CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
        }

        private static bool InvokeKeyUp(int wParam, KeyboardHookStruct keyboardHookStruct)
        {
            if (s_KeyUp != null && (wParam == Messages.WM_KEYUP || wParam == Messages.WM_SYSKEYUP))
            {
                Keys keyData = (Keys)keyboardHookStruct.VirtualKeyCode;
                KeyEventArgs e = new KeyEventArgs(keyData);
                s_KeyUp.Invoke(null, e);
                return e.Handled;
            }
            return false;
        }

        private static bool InvokeKeyPress(int wParam, KeyboardHookStruct keyboardHookStruct)
        {
            if (s_KeyPress != null && wParam == Messages.WM_KEYDOWN)
            {
                bool isDownShift = ((Keyboard.GetKeyState(Keyboard.VK_SHIFT) & 0x80) == 0x80 ? true : false);
                bool isDownCapslock = (Keyboard.GetKeyState(Keyboard.VK_CAPITAL) != 0 ? true : false);

                byte[] keyState = new byte[256];
                Keyboard.GetKeyboardState(keyState);
                byte[] inBuffer = new byte[2];
                if (Keyboard.ToAscii(keyboardHookStruct.VirtualKeyCode,
                                     keyboardHookStruct.ScanCode,
                                     keyState,
                                     inBuffer,
                                     keyboardHookStruct.Flags) == 1)
                {
                    char key = (char)inBuffer[0];
                    if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
                    KeyPressEventArgs e = new KeyPressEventArgs(key);
                    s_KeyPress.Invoke(null, e);
                    return e.Handled;
                }
            }
            return false;
        }

        private static bool InvokeKeyDown(int wParam, KeyboardHookStruct keyboardHookStruct)
        {
            if (s_KeyDown != null && (wParam == Messages.WM_KEYDOWN || wParam == Messages.WM_SYSKEYDOWN))
            {
                Keys keyData = (Keys)keyboardHookStruct.VirtualKeyCode;
                KeyEventArgs e = new KeyEventArgs(keyData);
                s_KeyDown.Invoke(null, e);
                return e.Handled;
            }
            return false;
        }

        private static bool CanContinue(int nCode)
        {
            return nCode >= 0;
        }

        private static void EnsureSubscribedToGlobalKeyboardEvents()
        {
            // install Keyboard hook only if it is not installed and must be installed
            if (s_KeyboardHookHandle == 0)
            {
                //See comment of this field. To avoid GC to clean it up.
                s_KeyboardDelegate = KeyboardHookProc;
                //install hook
                s_KeyboardHookHandle = Hooker.SetWindowsHookEx(
                    Messages.WH_KEYBOARD_LL,
                    s_KeyboardDelegate,
                    Process.GetCurrentProcess().MainModule.BaseAddress,
                    0);
                //If SetWindowsHookEx fails.
                if (s_KeyboardHookHandle == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //do cleanup

                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void TryUnsubscribeFromGlobalKeyboardEvents()
        {
            //if no subsribers are registered unsubsribe from hook
            if (s_KeyDown == null &&
                s_KeyUp == null &&
                s_KeyPress == null)
            {
                ForceUnsunscribeFromGlobalKeyboardEvents();
            }
        }

        private static void ForceUnsunscribeFromGlobalKeyboardEvents()
        {
            if (s_KeyboardHookHandle != 0)
            {
                //uninstall hook
                int result = Hooker.UnhookWindowsHookEx(s_KeyboardHookHandle);
                //reset invalid handle
                s_KeyboardHookHandle = 0;
                //Free up for GC
                s_KeyboardDelegate = null;
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

        #endregion

        //################################################################
        #region Keyboard events

        private static event KeyPressEventHandler s_KeyPress;

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
        public static event KeyPressEventHandler KeyPress
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyPress += value;
            }
            remove
            {
                s_KeyPress -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        private static event KeyEventHandler s_KeyUp;

        /// <summary>
        /// Occurs when a key is released. 
        /// </summary>
        public static event KeyEventHandler KeyUp
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyUp += value;
            }
            remove
            {
                s_KeyUp -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        private static event KeyEventHandler s_KeyDown;

        /// <summary>
        /// Occurs when a key is preseed. 
        /// </summary>
        public static event KeyEventHandler KeyDown
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyDown += value;
            }
            remove
            {
                s_KeyDown -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }


        #endregion
    }
}
