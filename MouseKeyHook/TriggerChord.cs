using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gma.System.MouseKeyHook
{
    /// <summary>
    /// Used to represent a key combination as frequently used in application as shortcuts.
    /// e.g. Alt+Shift+R. This combination is triggered when 'R' is pressed after 'Alt' and 'Shift' are already down.
    /// </summary>
    public class TriggerChord : IEnumerable<Keys>
    {
        private readonly Chord _chord;

        private TriggerChord(Keys triggerKey, IEnumerable<Keys> chordKeys)
            : this(triggerKey, new Chord(chordKeys))
        {
        }

        private TriggerChord(Keys triggerKey, Chord chord)
        {
            TriggerKey = triggerKey;
            _chord =chord;
        }

        /// <summary>
        /// Last key which triggers the combination.
        /// </summary>
        public Keys TriggerKey { get; }
            
        internal IEnumerable<Keys> GetAllKeys
        {
            get { return _chord.Concat(Enumerable.Repeat(TriggerKey, 1)); }
        }

        /// <summary>
        /// Length of the key combination.
        /// </summary>
        public int Length
        {
            get { return _chord.Length + 1; }
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        /// <param name="key"></param>
        public static TriggerChord Create(Keys key)
        {
            return new TriggerChord(key, Chord.Empty());
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        /// <param name="key"></param>
        public TriggerChord And(Keys key)
        {
            return new TriggerChord(TriggerKey, _chord.And(key));
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        public TriggerChord Control()
        {
            return And(Keys.Control);
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        public TriggerChord Alt()
        {
            return And(Keys.Alt);
        }
        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        public TriggerChord Shift()
        {
            return And(Keys.Shift);
        }

        /// <inheritdoc />
        public IEnumerator<Keys> GetEnumerator()
        {
            return GetAllKeys.GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join("+", GetAllKeys);
        }

        /// <summary>
        /// Create a chord from any string like this 'Alt+Shift+R'.
        /// Nothe that the trigger key must be the last one. 
        /// </summary>
        public static TriggerChord FromString(string trigger)
        {
            var parts = trigger
                .Split('+')
                .Select(p => Enum.Parse(typeof(Keys), p))
                .Cast<Keys>();
            var stack = new Stack<Keys>(parts);
            var triggerKey = stack.Pop();
            return new TriggerChord(triggerKey, stack);
        }

        /// <inheritdoc />
        protected bool Equals(TriggerChord other)
        {
            return 
                TriggerKey == other.TriggerKey 
                && _chord.Equals(other._chord);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TriggerChord) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _chord.GetHashCode() ^
                   (int) TriggerKey;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public static bool operator ==(TriggerChord left, TriggerChord right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(TriggerChord left, TriggerChord right)
        {
            return !Equals(left, right);
        }
    }
}