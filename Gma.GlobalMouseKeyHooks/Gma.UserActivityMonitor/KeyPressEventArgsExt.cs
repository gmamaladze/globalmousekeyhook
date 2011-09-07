using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
    public class KeyPressEventArgsExt : KeyPressEventArgs
    {
        public KeyPressEventArgsExt(char keyChar) 
            : base(keyChar)
        {
            IsNonChar = false;
        }

        private static KeyPressEventArgsExt CreateNonChar()
        {
            KeyPressEventArgsExt e = new KeyPressEventArgsExt((char) 0x0);
            e.IsNonChar = true;
            return e;
        }

        internal static KeyPressEventArgsExt FromRawData(int wParam, IntPtr lParam, bool isGlobal)
        {
            return isGlobal ?
                FromRawDataGlobal(wParam, lParam) :
                FromRawDataApp(wParam, lParam);
        }

        private static KeyPressEventArgsExt FromRawDataApp(int wParam, IntPtr lParam)
        {
            //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

            const uint maskKeydown = 0x40000000; // for bit 30
            const uint maskKeyup = 0x80000000; // for bit 31
            const uint maskScanCode = 0xff0000; // for bit 23-16

            uint flags = (uint) lParam; //Marshal.ReadInt32(lParam);
            //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            bool wasKeyDown = (flags & maskKeydown) > 0;
            //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            bool isKeyReleased = (flags & maskKeyup) > 0;

            if (!wasKeyDown && !isKeyReleased)
            {
                return CreateNonChar();
            }

            int virtualKeyCode = wParam;
            int scanCode = checked((int)(flags & maskScanCode));
            const int fuState = 0;

            char ch;
            bool isSuccessfull = Keyboard.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, out ch);
            if (!isSuccessfull)
            {
                return CreateNonChar();
            }

            return new KeyPressEventArgsExt(ch);

        }


        internal static KeyPressEventArgsExt FromRawDataGlobal(int wParam, IntPtr lParam)
        {
            if (wParam != Messages.WM_KEYDOWN)
            {
                return CreateNonChar();
            }

            KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

            int virtualKeyCode = keyboardHookStruct.VirtualKeyCode;
            int scanCode = keyboardHookStruct.ScanCode;
            int fuState = keyboardHookStruct.Flags;

            char ch;
            bool isSuccessfull = Keyboard.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, out ch);
            if (!isSuccessfull)
            {
                return CreateNonChar();
            }
            
            return new KeyPressEventArgsExt(ch);
            
        }

        public bool IsNonChar { get; private set; }
    }
}
