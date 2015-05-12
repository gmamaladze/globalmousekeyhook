using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gma.System.MouseKeyHook.Implementation
{
    internal class ButtonSet
    {
        private MouseButtons m_Set;

        public ButtonSet()
        {
            m_Set = MouseButtons.None;
        }

        public void Add(MouseButtons element)
        {
            m_Set |= element;
        }

        public void Remove(MouseButtons element)
        {
            m_Set &= ~element;
        }

        public bool Contains(MouseButtons element)
        {
            return (m_Set & element) != MouseButtons.None;
        }
    }
}
