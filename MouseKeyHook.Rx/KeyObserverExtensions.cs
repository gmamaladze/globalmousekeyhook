using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gma.System.MouseKeyHook;
using srx=System.Reactive.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public static class KeyObserverExtensions
    {
        public static IObservable<KeyEvent> UpDownObservable(this IKeyboardEvents source)
        {
            var downObserver = srx
                .Observable
                .FromEventPattern<KeyEventArgs>(source, "KeyDown")
                .Select(ep=>ep.EventArgs.KeyCode)
                .Select(code => new KeyEvent {KeyCode = code, Kind = KeyEventKind.Down});

            var upObserver = srx
                .Observable
                .FromEventPattern<KeyEventArgs>(source, "KeyUp")
                .Select(ep => ep.EventArgs.KeyCode)
                .Select(code => new KeyEvent { KeyCode = code, Kind = KeyEventKind.Up });

            return downObserver
                .Merge(upObserver);
        }

        public static IObservable<Chord> Chords(this IObservable<KeyEvent> upDownObservable, int minChodLength=2, int maxChordLength = 3)
        {
            return Enumerable
                .Range(minChodLength, maxChordLength)
                .Select(n => upDownObservable
                    .Buffer(n, 1)
                .Select(buffer => new Chord(buffer)))
                .Merge();
        }

        public static IObservable<T> ChordsMapped<T>(this IObservable<KeyEvent> upDownObservable,
            IDictionary<Chord, T> map)
        {
            var min = map
                .Keys
                .Select(k => k.Events.Count())
                .Min();

            var max = map
                .Keys
                .Select(k => k.Events.Count())
                .Max();

            return upDownObservable
                .Chords(min, max)
                .Where(map.ContainsKey)
                .Select(chord => map[chord]);
        }
    }
}
