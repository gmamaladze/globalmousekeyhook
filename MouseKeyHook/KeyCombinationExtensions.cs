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
            source.KeyDown += (sender, e) =>
            {
                if (!watchlists.Contains(e.KeyCode)) return;
                var state = KeyboardState.GetCurrent();
                var matches = map.Where(pair => state.AreAllDown(pair.Key));
                foreach (var  current in matches)
                {
                    current.Value();
                }
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
            source.KeyDown += (sender, e) =>
            {
                if (!watchlists.Contains(e.KeyCode)) return;
                var state = KeyboardState.GetCurrent();
                var matches = map.Where(pair => state.AreAllDown(pair.Key));
                foreach (var current in matches)
                {
                    current.Value();
                }
            };
        }
    }
}
