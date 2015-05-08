using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MouseKeyboardActivityMonitor.NewApi
{
    /// <summary>
    /// Provides keyboard and mouse events.
    /// </summary>
    public interface IKeyboardMouseEvents : IKeyboardEvents, IMouseEvents
    {
    }
}
