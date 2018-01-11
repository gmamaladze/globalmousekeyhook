using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public static class KeysExtensions
    {
        public static KeyEvent Up(this Keys key)
        {
            return new KeyEvent {KeyCode = key, Kind = KeyEventKind.Up};
        }

        public static KeyEvent Down(this Keys key)
        {
            return new KeyEvent { KeyCode = key, Kind = KeyEventKind.Down };
        }
    }
}