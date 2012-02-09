using System;

namespace MouseKeyboardActivityMonitor.HotKeys
{
    ///<summary>
    /// The event arguments passed when a HotKeySet's OnHotKeysDownHold event is triggered.
    ///</summary>
    public sealed class HotKeyArgs : EventArgs
    {

        private readonly HotKeySet _sender;

        ///<summary>
        /// Creates an instance of the HotKeyArgs, using the HotKeySet as its context, which will be handed back to the user
        /// when the event is triggered.
        ///</summary>
        ///<param name="sender">The context/sender</param>
        public HotKeyArgs( HotKeySet sender )
        {
            _sender = sender;
        }

        ///<summary>
        /// The HotKeySet that triggered the event
        ///</summary>
        public HotKeySet Sender
        {
            get { return _sender; }
        }

    }
}
