// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;
using srx = System.Reactive.Linq;

namespace MouseKeyHook.Rx
{
    public static class KeyObserverExtensions
    {
        public static IObservable<Keys> KeyDownObservable(this IKeyboardEvents source)
        {
            return Observable
                .FromEventPattern<KeyEventArgs>(source, "KeyDown")
                .Select(ep => ep.EventArgs.KeyCode);
        }

        public static IObservable<Keys> KeyUpObservable(this IKeyboardEvents source)
        {
            return Observable
                .FromEventPattern<KeyEventArgs>(source, "KeyDown")
                .Select(ep => ep.EventArgs.KeyCode);
        }


        public static IObservable<KeyEvent> UpDownEvents(this IKeyboardEvents source)
        {
            return source
                .KeyDownObservable()
                .Select(key => key.Down())
                .Merge(source
                    .KeyUpObservable()
                    .Select(key => key.Down()));
        }


        public static IObservable<KeyWithState> WithState(this IObservable<Keys> source)
        {
            return source
                .Select(evt => new KeyWithState(evt, KeyboardState.GetCurrent()));
        }

        public static IObservable<TriggerChord> Matching(this IObservable<Keys> source, IEnumerable<TriggerChord> triggers)
        {
            return source
                .WithState()
                .SelectMany(se => triggers.Where(se.Matches));
        }

        public static IObservable<TriggerChord> MatchingLongest(this IObservable<Keys> source, IEnumerable<TriggerChord> triggers)
        {
            var sortedTriggers = triggers
                .GroupBy(t => t.TriggerKey)
                .Select(group => new KeyValuePair<Keys, IEnumerable<TriggerChord>>(group.Key, group.OrderBy(t => -t.Count())))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            return source
                .Where(keyCode => sortedTriggers.ContainsKey(keyCode))
                .WithState()
                .Select(se => sortedTriggers[se.KeyCode].First(se.Matches));
        }

    }
}