using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation;

namespace MouseKeyHook.Rx
{
    public class KeyWithState : Tuple<Keys, KeyboardState>
    {
        public KeyWithState(Keys key, KeyboardState state) 
            : base(key, state)
        {
            
        }

        public Keys KeyCode => this.Item1;
        public KeyboardState State => this.Item2;

        public bool Matches(Trigger trigger)
        {
            return 
                KeyCode == trigger.TriggerKey && 
                State.AreAllDown(trigger.AdditionalKeys);
        }
    }
}