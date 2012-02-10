using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MouseKeyboardActivityMonitor.WinApi
{
    internal static class KeyboardNativeMethods
    {
        //values from Winuser.h in Microsoft SDK.
        public const byte VK_SHIFT = 0x10;
        public const byte VK_CAPITAL = 0x14;
        public const byte VK_NUMLOCK = 0x90;
        public const byte VK_LSHIFT = 0xA0;
        public const byte VK_RSHIFT = 0xA1;
        public const byte VK_LCONTROL = 0xA2;
        public const byte VK_RCONTROL = 0xA3;
        public const byte VK_LMENU = 0xA4;
        public const byte VK_RMENU = 0xA5;
        public const byte VK_LWIN = 0x5B;
        public const byte VK_RWIN = 0x5C;
        public const byte VK_SCROLL = 0x91;
        public const byte VK_INSERT = 0x2D;
        //may be possible to use these aggregates instead of L and R separately (untested)
        public const byte VK_CONTROL = 0x11;
        public const byte VK_MENU = 0x12;
        public const byte VK_PACKET = 0xE7; //Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods

        //buffer used in ToUnicode translation.  It's used repeatedly, very quickly so it's currently set as a static class variable.
        private static readonly StringBuilder m_pwszBuffer = new StringBuilder( 64 );

        internal static bool TryGetCharFromKeyboardState( int virtualKeyCode, int scanCode, int fuState, out char ch )
        {
            KeyboardState keyboardState = KeyboardState.GetCurrent();

            byte[] nativekeyboardState = keyboardState.GetNativeState();

            if ( ToUnicode( virtualKeyCode, scanCode, nativekeyboardState, m_pwszBuffer, m_pwszBuffer.Capacity, fuState ) != 1 )
            {
                ch = ( char )0;
                return false;
            }

            ch = m_pwszBuffer[ 0 ];

            bool isDownShift = keyboardState.IsDown( Keys.ShiftKey );
            bool isToggledCapsLock = keyboardState.IsToggled( Keys.CapsLock );

            if ( ( isToggledCapsLock ^ isDownShift ) && Char.IsLetter( ch ) )
            {
                ch = Char.ToUpper( ch );
            }
            return true;
        }

        /// <summary>
        /// The ToAscii function translates the specified virtual-key code and keyboard
        /// state to the corresponding character or characters. The function translates the code
        /// using the input language and physical keyboard layout identified by the keyboard layout handle.
        /// </summary>
        /// <param name="uVirtKey">
        /// [in] Specifies the virtual-key code to be translated.
        /// </param>
        /// <param name="uScanCode">
        /// [in] Specifies the hardware scan code of the key to be translated.
        /// The high-order bit of this value is set if the key is up (not pressed).
        /// </param>
        /// <param name="lpbKeyState">
        /// [in] Pointer to a 256-byte array that contains the current keyboard state.
        /// Each element (byte) in the array contains the state of one key.
        /// If the high-order bit of a byte is set, the key is down (pressed).
        /// The low bit, if set, indicates that the key is toggled on. In this function,
        /// only the toggle bit of the CAPS LOCK key is relevant. The toggle state
        /// of the NUM LOCK and SCROLL LOCK keys is ignored.
        /// </param>
        /// <param name="lpwTransKey">
        /// [out] Pointer to the buffer that receives the translated character or characters.
        /// </param>
        /// <param name="fuState">
        /// [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.
        /// </param>
        /// <returns>
        /// If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values.
        /// Value Meaning
        /// 0 The specified virtual key has no translation for the current state of the keyboard.
        /// 1 One character was copied to the buffer.
        /// 2 Two characters were copied to the buffer. This usually happens when a dead-key character
        /// (accent or diacritic) stored in the keyboard layout cannot be composed with the specified
        /// virtual key to form a single character.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [Obsolete( "Use ToUnicode instead" )]
        [DllImport( "user32" )]
        public static extern int ToAscii(
            int uVirtKey,
            int uScanCode,
            byte[] lpbKeyState,
            byte[] lpwTransKey,
            int fuState );


        /// <summary>
        /// Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or characters.
        /// </summary>
        /// <param name="wVirtKey">[in] The virtual-key code to be translated.</param>
        /// <param name="wScanCode">[in] The hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up.</param>
        /// <param name="lpKeyState">[in, optional] A pointer to a 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down.</param>
        /// <param name="pwszBuff">[out] The buffer that receives the translated Unicode character or characters. However, this buffer may be returned without being null-terminated even though the variable name suggests that it is null-terminated.</param>
        /// <param name="cchBuff">[in] The size, in characters, of the buffer pointed to by the pwszBuff parameter.</param>
        /// <param name="wFlags">[in] The behavior of the function. If bit 0 is set, a menu is active. Bits 1 through 31 are reserved.</param>
        /// <returns>
        ///     -1 &lt;= return &lt;= n
        /// <list type="bullet">
        ///     <item>-1    = The specified virtual key is a dead-key character (accent or diacritic). This value is returned regardless of the keyboard layout, even if several characters have been typed and are stored in the keyboard state. If possible, even with Unicode keyboard layouts, the function has written a spacing version of the dead-key character to the buffer specified by pwszBuff. For example, the function writes the character SPACING ACUTE (0x00B4), rather than the character NON_SPACING ACUTE (0x0301).</item>
        ///     <item> 0    = The specified virtual key has no translation for the current state of the keyboard. Nothing was written to the buffer specified by pwszBuff.</item>
        ///     <item> 1    = One character was written to the buffer specified by pwszBuff.</item>
        ///     <item> n    = Two or more characters were written to the buffer specified by pwszBuff. The most common cause for this is that a dead-key character (accent or diacritic) stored in the keyboard layout could not be combined with the specified virtual key to form a single character. However, the buffer may contain more characters than the return value specifies. When this happens, any extra characters are invalid and should be ignored.</item>
        /// </list>
        /// </returns>
        [DllImport( "user32" )]
        public static extern int ToUnicode( int wVirtKey,
                                            int wScanCode,
                                            byte[] lpKeyState,
                                            [Out, MarshalAs( UnmanagedType.LPWStr, SizeConst = 64 )]
                                            StringBuilder pwszBuff,
                                            int cchBuff,
                                            int wFlags );

        /// <summary>
        /// The GetKeyboardState function copies the status of the 256 virtual keys to the
        /// specified buffer.
        /// </summary>
        /// <param name="pbKeyState">
        /// [in] Pointer to a 256-byte array that contains keyboard key states.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport( "user32" )]
        public static extern int GetKeyboardState( byte[] pbKeyState );

        /// <summary>
        /// The GetKeyState function retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled
        /// (on, off—alternating each time the key is pressed).
        /// </summary>
        /// <param name="vKey">
        /// [in] Specifies a virtual key. If the desired virtual key is a letter or digit (A through Z, a through z, or 0 through 9), nVirtKey must be set to the ASCII value of that character. For other keys, it must be a virtual-key code.
        /// </param>
        /// <returns>
        /// The return value specifies the status of the specified virtual key, as follows:
        ///If the high-order bit is 1, the key is down; otherwise, it is up.
        ///If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, and off when the key is untoggled.
        /// </returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/ms646301.aspx</remarks>
        [DllImport( "user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall )]
        public static extern short GetKeyState( int vKey );


    }

}