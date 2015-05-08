using MouseKeyboardActivityMonitor.NewApi;

namespace MouseKeyboardActivityMonitor
{
    /// <summary>
    /// This is the class to start with.
    /// </summary>
    public static class Hook
    {
        /// <summary>
        /// Here you find all application wide events. Both mouse and keyboard.
        /// </summary>
        /// <returns>
        /// Returned instance is used for event subscriptions. 
        /// You can refetch it (you will get the same instance anyway).
        /// </returns>
        public static IKeyboardMouseEvents AppEvents()
        {
            return AppEventFacade.Instance();
        }

        /// <summary>
        /// Here you find all application wide events. Both mouse and keyboard.
        /// </summary>
        /// <returns>
        /// Returned instance is used for event subscriptions. 
        /// You can refetch it (you will get the same instance anyway).
        /// </returns>
        public static IKeyboardMouseEvents GlobalEvents()
        {
            return GlobalEventFacade.Instance();
        }
    }
}