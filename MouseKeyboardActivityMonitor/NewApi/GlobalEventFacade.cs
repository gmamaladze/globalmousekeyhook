using MouseKeyboardActivityMonitor.NewApi;

namespace MouseKeyboardActivityMonitor
{
    internal class GlobalEventFacade : EventFacade
    {
        private static GlobalEventFacade _instance;

        private GlobalEventFacade()
        {
            
        }

        protected override MouseListener CreateMouseListener()
        {
            return new GlobalMouseListener();
        }

        protected override KeyListener CreateKeyListener()
        {
            return new GlobalKeyListener();
        }

        public static GlobalEventFacade Instance()
        {
            return _instance ?? (_instance = new GlobalEventFacade());
        }
    }
}