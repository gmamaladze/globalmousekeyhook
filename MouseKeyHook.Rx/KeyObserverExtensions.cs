using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gma.System.MouseKeyHook;
using srx=System.Reactive.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

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

        public static IObservable<KeySequence> KeySequences(this IObservable<KeyEvent> upDownObservable, int minLength=2, int maxLength = 3)
        {
            return Enumerable
                .Range(minLength, maxLength)
                .Select(n => upDownObservable
                    .Buffer(n, 1)
                .Select(buffer => new KeySequence(buffer)))
                .Merge();
        }

        public static IObservable<T> KeySequenceMapped<T>(this IObservable<KeyEvent> upDownObservable,
            IDictionary<KeySequence, T> map)
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
                .KeySequences(min, max)
                .Where(map.ContainsKey)
                .Select(chord => map[chord]);
        }

        public static IObservable<Chord> ChordObservable(this IKeyboardEvents source, IEnumerable<Chord> expected)
        {
            return srx
                .Observable
                .FromEventPattern<KeyEventArgs>(source, "KeyDown")
                .Select(ep => ep.EventArgs.KeyCode)
                .Select(keyCode => expected.FirstOrDefault(current => current.Matches(keyCode)))
                .Where(chord=>chord!=null);
        } 
    }
}
