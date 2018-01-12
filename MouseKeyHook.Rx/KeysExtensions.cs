using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public static class KeysExtensions
    {
        public static KeyEvent Up(this Keys key)
        {
            return new KeyEvent(key, KeyEventKind.Up);
        }

        public static KeyEvent Down(this Keys key)
        {
            return new KeyEvent(key, KeyEventKind.Down);
        }

        public static bool IsModifier(this Keys key)
        {
            return key.ToModifier()!=Keys.None;
        }

        public static Keys ToModifier(this Keys key)
        {
            if ((key & Keys.LControlKey) == Keys.LControlKey ||
                (key & Keys.RControlKey) == Keys.RControlKey) return Keys.Control;
            if ((key & Keys.LShiftKey) == Keys.LShiftKey ||
                (key & Keys.RShiftKey) == Keys.RShiftKey) return Keys.Shift;
            if ((key & Keys.LMenu) == Keys.LMenu ||
                (key & Keys.RMenu) == Keys.RMenu) return Keys.Alt;
            return Keys.None;
        }

        public static Keys KeyCode(this Keys key)
        {
            return key & Keys.KeyCode;
        }

        public static Keys Modifier(this Keys key)
        {
            return key & Keys.Modifiers;
        }

        public static Keys Control(this Keys key)
        {
            return Keys.Control | key;
        }

        public static Keys Shift(this Keys key)
        {
            return Keys.Shift | key;
        }

        public static Keys Alt(this Keys key)
        {
            return Keys.Alt | key;
        }
    }
}