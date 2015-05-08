namespace MouseKeyboardActivityMonitor
{
    internal class AppEventFacade : EventFacade
    {
        protected override MouseListener CreateMouseListener()
        {
            return new AppMouseListener();
        }

        protected override KeyListener CreateKeyListener()
        {
            return new AppKeyListener();
        }
    }
}