using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MouseKeyHook.Rx
{
    public class KeySequence : IEnumerable<KeyEvent>
    {
        public IEnumerator<KeyEvent> GetEnumerator()
        {
            return _keyEvents.Cast<KeyEvent>().GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(",", _keyEvents);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private readonly KeyEvent[] _keyEvents;

        public KeySequence(string id, params KeyEvent[] keyEvents)
        {
            Id = id;
            _keyEvents = keyEvents;
        }

        public string Id { get; }

        public KeySequence(params KeyEvent[] keyEvents) 
            : this(string.Empty, keyEvents)
        {
           
        }
    }
}