using MouseKeyboardActivityMonitor.NewApi;

namespace MouseKeyboardActivityMonitor
{
    public static class Hook
    {
        public static IKeyboardMouseEvents AppEvents()
        {
            return new AppEventFacade();
        }

        public static IKeyboardMouseEvents GlobalEvents()
        {
            return new GlobalEventFacade();
        }
    }
}