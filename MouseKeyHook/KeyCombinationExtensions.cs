using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation;

namespace Gma.System.MouseKeyHook
{
    /// <summary>
    /// Extension methods to detect key combinations
    /// </summary>
    public static class KeyCombinationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="map"></param>
        public static void OnCombination(this IKeyboardEvents source, IEnumerable<KeyValuePair<Chord, Action>> map)
        {
            var watchlists = new HashSet<Keys>(map.SelectMany(p=>p.Key));
            OnCombination<Chord>(source, map, watchlists, null);
        }

        private static void OnCombination<T>(
            IKeyboardEvents source, 
            IEnumerable<KeyValuePair<T, Action>> map, 
            IEnumerable<Keys> watchlists, 
            Action reset) 
                where T:IEnumerable<Keys>
        {
            source.KeyDown += (sender, e) =>
            {
                if (!watchlists.Contains(e.KeyCode)) return;
                var state = KeyboardState.GetCurrent();
                var matches = map.Where(pair => state.AreAllDown(pair.Key)).Select(pair=>pair.Value);
                var isEmpyty = true;
                foreach (var current in matches)
                {
                    current();
                    isEmpyty = false;
                }
                if (isEmpyty) reset?.Invoke();
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="map"></param>
        public static void OnCombination(this IKeyboardEvents source, IEnumerable<KeyValuePair<TriggerChord, Action>> map)
        {
            var watchlists = new HashSet<Keys>(map.Select(p => p.Key.TriggerKey));
            OnCombination<TriggerChord>(source, map, watchlists, null);
        }


        public static void OnSequence(this IKeyboardEvents source, IDictionary<Sequence, Action> map)
        {

            var endsWith = new Func<Queue<TriggerChord>, Sequence, bool>((chords, sequence) =>
            {
                var skipCount = chords.Count - sequence.Length;
                return skipCount >= 0 && chords.Skip(skipCount).SequenceEqual(sequence);
            });

            var max = map.Select(p => p.Key).Max(c => c.Length);
            var min = map.Select(p => p.Key).Min(c => c.Length);
            Queue<TriggerChord> buffer = new Queue<TriggerChord>(max);

            var wrapMap = map.SelectMany(p => p.Key).ToDictionary(c => c, c => new Action(() =>
            {
                buffer.Enqueue(c);
                if (buffer.Count > max) buffer.Dequeue();
                if (buffer.Count < min) return;
                foreach (var pair in map)
                {
                    var sequence = pair.Key;
                    if (!endsWith(buffer, sequence)) continue;
                    var action = pair.Value;
                    action();
                }
            }));


            var watchlists = new HashSet<Keys>(
                map.Select(p => p.Key).SelectMany(s => s.SelectMany(k => k.GetAllKeys)));
            OnCombination(source, wrapMap, watchlists, buffer.Clear);
        }



        public static Keys Normalize(this Keys key)
        {
            if ((key & Keys.LControlKey) == Keys.LControlKey ||
                (key & Keys.RControlKey) == Keys.RControlKey) return Keys.Control;
            if ((key & Keys.LShiftKey) == Keys.LShiftKey ||
                (key & Keys.RShiftKey) == Keys.RShiftKey) return Keys.Shift;
            if ((key & Keys.LMenu) == Keys.LMenu ||
                (key & Keys.RMenu) == Keys.RMenu) return Keys.Alt;
            return key;
        }
    }
}
