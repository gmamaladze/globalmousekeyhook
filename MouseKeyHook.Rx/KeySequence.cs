using System.Linq;
using System.Windows.Forms;

namespace MouseKeyHook.Rx
{
    public class KeySequence : Sequence<Keys>
    {
        public KeySequence(params Keys[] keys) 
            : base(keys.Select(k=>KeysExtensions.Normalize(k)).ToArray())
        {
            
        }
    }
}