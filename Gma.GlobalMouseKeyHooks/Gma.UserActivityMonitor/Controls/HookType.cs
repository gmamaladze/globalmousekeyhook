namespace Gma.UserActivityMonitor.Controls
{
    /// <summary>
    /// Indicates which hooks to listen to application or global.
    /// </summary>
    public enum HookType
    {
        /// <summary>
        /// Only events inside application are monitored and fired.
        /// </summary>
        Application,

        /// <summary>
        /// All events system wide are monitored and fired.
        /// </summary>
        Global
    }
}
