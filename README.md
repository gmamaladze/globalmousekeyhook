### What it does?

This library allows you to tap keyboard and mouse, to detect and record their activity even when an application is inactive and runs in background.

### How?
This library attaches to windows global hooks, tracks keyboard and mouse clicks and movement and raises common .NET events with KeyEventArgs and MouseEventArgs, so you can easily retrieve any information you need:
* Mouse coordinates
* Mouse buttons clicked
* Mouse wheel scrolls
* Key presses and releases
* Special key states

Additionally, there are `MouseEventExtArgs` and `KeyEventExtArgs` which provide further options:
* Input suppression
* Timestamp
* IsMouseDown/Up
* IsKeyDown/Up.
* Background

### History

|  Year       |     URL
--------------|--------------------------------
| 2000 - 2008 | http://www.codeproject.com/KB/cs/globalhook.aspx
| 2008 - 2015 | https://globalmousekeyhook.codeplex.com/
| 2015 - now  | https://github.com/gmamaladze/globalmousekeyhook

### Installation

NuGet comming soon

### Usage

```
private MouseHookListener m_mouseListener;

public void Activate()
{
    // Note: for an application hook, use the AppHooker class instead
    m_mouseListener = new MouseHookListener(new GlobalHooker());

    // The listener is not enabled by default
    m_mouseListener.Enabled = true;

    // Set the event handler
    // recommended to use the Extended handlers, which allow input suppression among other additional information
    m_mouseListener.MouseDownExt += MouseListener_MouseDownExt;
}

public void Deactivate()
{
    m_mouseListener.Dispose();
}

private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
{
    // log the mouse click
    Console.WriteLine(string.Format("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp));

    // uncommenting the following line will suppress the middle mouse button click
    // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
}
```
