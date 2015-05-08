using MouseKeyboardActivityMonitor.NewApi;

namespace MouseKeyboardActivityMonitor
{
    internal class AppEventFacade : EventFacade
    {
        private static AppEventFacade _instance;

        private AppEventFacade()
        {
            
        }

        protected override MouseListener CreateMouseListener()
        {
            return new AppMouseListener();
        }

        protected override KeyListener CreateKeyListener()
        {
            return new AppKeyListener();
        }

        public static AppEventFacade Instance()
        {
            return _instance ?? (_instance = new AppEventFacade());
        }
    }
}