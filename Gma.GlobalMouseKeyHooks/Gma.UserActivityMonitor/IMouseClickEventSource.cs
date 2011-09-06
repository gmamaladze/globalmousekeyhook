using System;
using System.Windows.Forms;

namespace Gma.UserActivityMonitor
{
    public interface IMouseClickEventSource
    {
        event MouseEventHandler MouseUp;
    }
}