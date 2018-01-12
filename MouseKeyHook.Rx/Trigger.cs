using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public class Trigger
    {
        public Keys TriggerKey { get; }

        public IEnumerable<Keys> AdditionalKeys => _addKeys;

        public IEnumerable<Keys> AllKeys => _addKeys.Concat(Enumerable.Repeat(TriggerKey, 1)); 

        private readonly Stack<Keys> _addKeys;

        public int Length => _addKeys.Count + 1;

        private Trigger(Keys triggerKey, IEnumerable<Keys> additionalKeys)
        {
            TriggerKey = triggerKey;
            _addKeys = new Stack<Keys>(additionalKeys.OrderBy(k=>k));
        }

        public static Trigger On(Keys key)
        {
            return new Trigger(key, new Stack<Keys>());
        }

        public Trigger And(Keys key)
        {
            return new Trigger(this.TriggerKey, _addKeys.Concat(Enumerable.Repeat(key, 1)));
        }

        public Trigger Control()
        {
            return And(Keys.Control);
        }

        public Trigger Alt()
        {
            return And(Keys.Alt);
        }

        public Trigger Shift()
        {
            return And(Keys.Shift);
        }

        public override string ToString()
        {
            return string.Join("+", AllKeys);
        }

        public static Trigger FromString(string trigger)
        {
            var parts = trigger
                .Split('+')
                .Select(p=>Enum.Parse(typeof(Keys), p))
                .Cast<Keys>();
            var stack = new Stack<Keys>(parts);
            var triggerKey = stack.Pop();
            return new Trigger(triggerKey, stack);
        }

        protected bool Equals(Trigger other)
        {
            if (TriggerKey != other.TriggerKey) return false;
            if (_addKeys.Count != other._addKeys.Count) return false;
            return _addKeys.SequenceEqual(other._addKeys);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Trigger) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return _addKeys.Count+13 ^ 
                       ((_addKeys.Count!=0 ? (int)_addKeys.Peek() :0 ) * 397) ^ 
                       (int) TriggerKey;
            }
        }

        public static bool operator ==(Trigger left, Trigger right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Trigger left, Trigger right)
        {
            return !Equals(left, right);
        }
    }
}