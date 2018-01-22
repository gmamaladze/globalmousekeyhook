using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gma.System.MouseKeyHook
{
    /// <summary>
    /// Used to represent a simultanously pressed key combination where the ORDER DOES NOT metter.
    /// </summary>
    public class Chord : IEnumerable<Keys>
    {
        private readonly Stack<Keys> _keys;

        internal Chord(IEnumerable<Keys> additionalKeys)
        {
            _keys = new Stack<Keys>(additionalKeys.Select(k=>k.Normalize()).OrderBy(k => k));
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        /// <param name="key"></param>
        public static Chord Create(Keys key)
        {
            return new Chord(new []{key});
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        /// <param name="key"></param>
        public Chord And(Keys key)
        {
            return new Chord(_keys.Concat(new []{key}));
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        public Chord Control()
        {
            return And(Keys.Control);
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        public Chord Alt()
        {
            return And(Keys.Alt);
        }

        /// <summary>
        /// A chainable builder method to simplify chord creation. Used along with <see cref="Create"/>, <see cref="And"/>, <see cref="Control"/>, <see cref="Shift"/>, <see cref="Alt"/>. 
        /// </summary>
        public Chord Shift()
        {
            return And(Keys.Shift);
        }

        /// <inheritdoc />
        public IEnumerator<Keys> GetEnumerator()
        {
            return _keys.GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join("+", _keys);
        }

        /// <summary>
        /// Create a chord from any string like this 'R+Alt+Control' 
        /// </summary>
        public static Chord FromString(string chord)
        {
            var parts = chord
                .Split('+')
                .Select(p => Enum.Parse(typeof(Keys), p))
                .Cast<Keys>();
            var stack = new Stack<Keys>(parts);
            return new Chord(stack);
        }

        /// <inheritdoc />
        protected bool Equals(Chord other)
        {
            if (_keys.Count != other._keys.Count) return false;
            return _keys.SequenceEqual(other._keys);
        }
        
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TriggerChord)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (_keys.Count + 13) ^
                       ((_keys.Count != 0 ? (int)_keys.Peek() : 0) * 397);
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public static bool operator ==(Chord left, Chord right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(Chord left, Chord right)
        {
            return !Equals(left, right);
        }

        internal static IEnumerable<Keys> Empty()
        {
            return new Chord(Enumerable.Empty<Keys>());
        }
    }
}