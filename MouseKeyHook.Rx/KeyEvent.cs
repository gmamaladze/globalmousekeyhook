using System.Collections.Generic;
using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public class KeyEvent
    {

        public KeyEventKind Kind { get; set; }
        public Keys KeyCode { get; set; }

        protected bool Equals(KeyEvent other)
        {
            return Kind == other.Kind && (KeyCode & other.KeyCode) == other.KeyCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Kind * 397) ^ (int) KeyCode;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", KeyCode, Kind);
        }

        public static bool operator ==(KeyEvent left, KeyEvent right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KeyEvent left, KeyEvent right)
        {
            return !Equals(left, right);
        }
    }
}