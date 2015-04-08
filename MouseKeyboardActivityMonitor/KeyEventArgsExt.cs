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
        public KeyEventArgsExt( Keys keyData )
            : base( keyData )
        {
        }

        internal KeyEventArgsExt( Keys keyData, int timestamp, bool isKeyDown, bool isKeyUp, char unicodeChar )
            : this( keyData )
        {
            Timestamp = timestamp;
            IsKeyDown = isKeyDown;
            IsKeyUp = isKeyUp;
            UnicodeChar = unicodeChar;
        }

        /// <summary>
        /// Creates <see cref="KeyEventArgsExt"/> from Windows Message parameters.
        /// </summary>
        /// <param name="wParam">The first Windows Message parameter.</param>
        /// <param name="lParam">The second Windows Message parameter.</param>
        /// <param name="isGlobal">Specifies if the hook is local or global.</param>
        /// <returns>A new KeyEventArgsExt object.</returns>
        internal static KeyEventArgsExt FromRawData( int wParam, IntPtr lParam, bool isGlobal )
        {
            return isGlobal ?
                FromRawDataGlobal( wParam, lParam ) :
                FromRawDataApp( wParam, lParam );
        }

        /// <summary>
        /// Creates <see cref="KeyEventArgsExt"/> from Windows Message parameters, based upon
        /// a local application hook.
        /// </summary>
        /// <param name="wParam">The first Windows Message parameter.</param>
        /// <param name="lParam">The second Windows Message parameter.</param>
        /// <returns>A new KeyEventArgsExt object.</returns>
        private static KeyEventArgsExt FromRawDataApp( int wParam, IntPtr lParam )
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
            //updated from ( uint )lParam, which threw an integer overflow exception in Unicode characters
            flags = ( uint )lParam.ToInt64();
#endif

            //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            bool wasKeyDown = ( flags & maskKeydown ) > 0;
            //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            bool isKeyReleased = ( flags & maskKeyup ) > 0;

            Keys keyData = AppendModifierStates( ( Keys )wParam );

            bool isKeyDown = !wasKeyDown && !isKeyReleased;
            bool isKeyUp = wasKeyDown && isKeyReleased;

            char ch;

            //translated based on the active application's keyboard layout.
            KeyboardNativeMethods.TryGetCharFromKeyboardState( wParam, ( int )flags, out ch );
            return new KeyEventArgsExt( keyData, timestamp, isKeyDown, isKeyUp, ch );

        }

        /// <summary>
        /// Creates <see cref="KeyEventArgsExt"/> from Windows Message parameters, based upon
        /// a system-wide hook.
        /// </summary>
        /// <param name="wParam">The first Windows Message parameter.</param>
        /// <param name="lParam">The second Windows Message parameter.</param>
        /// <returns>A new KeyEventArgsExt object.</returns>
        private static KeyEventArgsExt FromRawDataGlobal( int wParam, IntPtr lParam )
        {

            KeyboardHookStruct keyboardHookStruct = ( KeyboardHookStruct )Marshal.PtrToStructure( lParam, typeof( KeyboardHookStruct ) );
            Keys keyData = AppendModifierStates( ( Keys )keyboardHookStruct.VirtualKeyCode );

            bool isKeyDown = ( wParam == Messages.WM_KEYDOWN || wParam == Messages.WM_SYSKEYDOWN );
            bool isKeyUp = ( wParam == Messages.WM_KEYUP || wParam == Messages.WM_SYSKEYUP );

            //sent explicitly as a Unicode character
            if ( keyboardHookStruct.VirtualKeyCode == KeyboardNativeMethods.VK_PACKET )
                return new KeyEventArgsExt( keyData, keyboardHookStruct.Time, isKeyDown, isKeyUp, ( char )AppendModifierStates( ( Keys )keyboardHookStruct.ScanCode ) );

            //Translate based on the application's keyboard layout
            char ch;
            KeyboardNativeMethods.TryGetCharFromKeyboardState( keyboardHookStruct.VirtualKeyCode, keyboardHookStruct.ScanCode, keyboardHookStruct.Flags, out ch );
            return new KeyEventArgsExt( keyData, keyboardHookStruct.Time, isKeyDown, isKeyUp, ch );

        }

        // # It is not possible to distinguish Keys.LControlKey and Keys.RControlKey when they are modifiers
        // Check for Keys.Control instead
        // Same for Shift and Alt(Menu)
        // See more at http://www.tech-archive.net/Archive/DotNet/microsoft.public.dotnet.framework.windowsforms/2008-04/msg00127.html #
        
        // A shortcut to make life easier
        private static bool CheckModifier( int vKey )
        {
            return (KeyboardNativeMethods.GetKeyState(vKey) & 0x8000) > 0;
        }
        
        private static Keys AppendModifierStates( Keys keyData )
        {
            // Is Control being held down?
            bool control = CheckModifier(KeyboardNativeMethods.VK_CONTROL);
            // Is Shift being held down?
            bool shift = CheckModifier(KeyboardNativeMethods.VK_SHIFT);
            // Is Alt being held down?
            bool alt = CheckModifier(KeyboardNativeMethods.VK_MENU);
            
            // Windows keys
            // # combine LWin and RWin key with other keys will potentially corrupt the data
            // notable F5 | Keys.LWin == F12, see https://globalmousekeyhook.codeplex.com/workitem/1188
            // and the KeyEventArgs.KeyData don't recognize combined data either

            // Function (Fn) key
            // # CANNOT determine state due to conversion inside keyboard
            // See http://en.wikipedia.org/wiki/Fn_key#Technical_details #

            return keyData |
                ( control ? Keys.Control : Keys.None ) |
                ( shift ? Keys.Shift : Keys.None ) |
                ( alt ? Keys.Alt : Keys.None );
        }

        /// <summary>
        /// The system tick count of when the event occurred.
        /// </summary>
        public int Timestamp { get; private set; }

        /// <summary>
        /// True if event signals key down..
        /// </summary>
        public bool IsKeyDown { get; private set; }

        /// <summary>
        /// True if event signals key up.
        /// </summary>
        public bool IsKeyUp { get; private set; }

        ///<summary>
        /// Returns the character representation
        ///</summary>
        public char UnicodeChar { get; private set; }

    }
}