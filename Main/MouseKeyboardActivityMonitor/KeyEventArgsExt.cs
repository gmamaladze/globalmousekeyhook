using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor.WinApi;

namespace MouseKeyboardActivityMonitor
{
    /// <summary>
    /// Provides extended argument data for the <see cref='KeyboardHookListener.KeyDown'/> or <see cref='KeyboardHookListener.KeyUp'/> event.
    /// </summary>
    public class KeyEventArgsExt : KeyEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgsExt"/> class.
        /// </summary>
        /// <param name="keyData"></param>
        public KeyEventArgsExt(Keys keyData) : base(keyData)
        {
        }

        internal KeyEventArgsExt(Keys keyData, int timestamp, bool isKeyDown, bool isKeyUp)
            : this(keyData)
        {
        	Timestamp = timestamp; 
            IsKeyDown = isKeyDown;
            IsKeyUp = isKeyUp;
        }

        /// <summary>
        /// Creates <see cref="KeyEventArgsExt"/> from Windows Message parameters.
        /// </summary>
        /// <param name="wParam">The first Windows Message parameter.</param>
        /// <param name="lParam">The second Windows Message parameter.</param>
        /// <param name="isGlobal">Specifies if the hook is local or global.</param>
        /// <returns>A new KeyEventArgsExt object.</returns>
        internal static KeyEventArgsExt FromRawData(int wParam, IntPtr lParam, bool isGlobal)
        {
            return isGlobal ? 
                FromRawDataGlobal(wParam, lParam) : 
                FromRawDataApp(wParam, lParam);
        }

        /// <summary>
        /// Creates <see cref="KeyEventArgsExt"/> from Windows Message parameters, based upon
        /// a local application hook.
        /// </summary>
        /// <param name="wParam">The first Windows Message parameter.</param>
        /// <param name="lParam">The second Windows Message parameter.</param>
        /// <returns>A new KeyEventArgsExt object.</returns>
        private static KeyEventArgsExt FromRawDataApp(int wParam, IntPtr lParam)
        {
            //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

            const uint maskKeydown = 0x40000000; // for bit 30
            const uint maskKeyup = 0x80000000; // for bit 31

            int timestamp = Environment.TickCount;

            uint flags = 0u;
#if IS_X64
            // both of these are ugly hacks. Is there a better way to convert a 64bit IntPtr to uint?

            // flags = uint.Parse(lParam.ToString());
            flags = Convert.ToUInt32(lParam.ToInt64());
#else
            flags = (uint)lParam;
#endif
            

            //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            bool wasKeyDown = (flags & maskKeydown) > 0;
            //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            bool isKeyReleased = (flags & maskKeyup) > 0;

            Keys keyData = (Keys)wParam;

            bool isKeyDown = !wasKeyDown && !isKeyReleased;
            bool isKeyUp = wasKeyDown && isKeyReleased;

            return new KeyEventArgsExt(keyData, timestamp, isKeyDown, isKeyUp);
        }

        /// <summary>
        /// Creates <see cref="KeyEventArgsExt"/> from Windows Message parameters, based upon
        /// a system-wide hook.
        /// </summary>
        /// <param name="wParam">The first Windows Message parameter.</param>
        /// <param name="lParam">The second Windows Message parameter.</param>
        /// <returns>A new KeyEventArgsExt object.</returns>
        private static KeyEventArgsExt FromRawDataGlobal(int wParam, IntPtr lParam)
        {
            KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
            Keys keyData = (Keys)keyboardHookStruct.VirtualKeyCode;
            bool isKeyDown = (wParam == Messages.WM_KEYDOWN || wParam == Messages.WM_SYSKEYDOWN);
            bool isKeyUp = (wParam == Messages.WM_KEYUP || wParam == Messages.WM_SYSKEYUP);
            
            return new KeyEventArgsExt(keyData, keyboardHookStruct.Time, isKeyDown, isKeyUp);
        }

        /// <summary>
        /// The system tick count of when the event occured.
        /// </summary> 
        public int Timestamp { get; private set; }
        
        /// <summary>
        /// True if event singnals key down..
        /// </summary>
        public bool IsKeyDown { get; private set; }
        
        /// <summary>
        /// True if event singnals key up.
        /// </summary>
        public bool IsKeyUp { get; private set; }
    }
}
