using System.Collections.Generic;
using System.Linq;

namespace MouseKeyHook.Rx
{
    public class KeySequence
    {
        public bool Matches(IEnumerable<KeyEvent> events)
        {
            return events.SequenceEqual(this.Events);
        }

        public static KeySequence Of(params KeyEvent[] events)
        {
            return new KeySequence(events);
        }

        protected bool Equals(KeySequence other)
        {
            return Events.Equals(other.Events);
        }

        public override bool Equals(object obj)
        {
            var chord = (KeySequence)obj;
            return chord != null && Matches(chord.Events);
        }

        internal static int CombineHashCodes(int h1, int h2)
        {
            return (((h1 << 5) + h1) ^ h2);
        }

        public override int GetHashCode()
        {
            return this
                .Events
                .Select(evt => evt.GetHashCode())
                .Aggregate(37, CombineHashCodes);
        }

        public override string ToString()
        {
            return Events.Aggregate(string.Empty, (s, e) => s+" "+e.ToString());
        }

        public IEnumerable<KeyEvent> Events { get; }

        public KeySequence(IEnumerable<KeyEvent> events)
        {
            Events = events;
        }
    }
}