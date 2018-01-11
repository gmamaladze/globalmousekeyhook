using System;
using System.Linq;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation;

namespace MouseKeyHook.Rx
{
    public class Chord
    {
        public Keys Trigger { get; set; }
        public Keys[] Modifiers { get; set; }

        public static Chord FromString(string chord)
        {
            var parts = chord.Split('+');
            var parsed = new Keys[parts.Length];
            for (var i = 0; i < parts.Length; i++)
            {
                Keys keyCode;
                var isOk = Enum.TryParse(parts[i], out keyCode);
                if (!isOk) return null;
                parsed[i] = keyCode;
            }

            var result = new Chord();

            var lastIdx = parsed.Length - 1;
            for (var i = 0; i < lastIdx; i++)
            {
                result.Modifiers[i] = parsed[i];
            }
            result.Trigger = parsed[lastIdx];
            return result;
        }

        public override string ToString()
        {
            return string.Join("+", Modifiers) + "+" + Trigger;
        }

        public bool Matches(Keys trigger)
        {
            if (trigger != Trigger) return false;
            var keyboardState = KeyboardState.GetCurrent();
            return Modifiers.All(modifier => IsDown(keyboardState, modifier));
        }

        private static bool IsDown(KeyboardState state, Keys modifier)
        {
            switch (modifier)
            {
                case Keys.Alt:
                    return state.IsDown(Keys.LMenu) | state.IsDown(Keys.RMenu);
                case Keys.Control:
                    return state.IsDown(Keys.LControlKey) | state.IsDown(Keys.RControlKey);
                case Keys.Shift:
                    return state.IsDown(Keys.LShiftKey) | state.IsDown(Keys.RShiftKey);
            }
            return state.IsDown(modifier);
        }
    }
}