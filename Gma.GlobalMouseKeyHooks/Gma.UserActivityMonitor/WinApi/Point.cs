using System.Runtime.InteropServices;

namespace Gma.UserActivityMonitor.WinApi
{
    /// <summary>
    /// The Point structure defines the X- and Y- coordinates of a point. 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/rectangl_0tiq.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Point {
        /// <summary>
        /// Specifies the X-coordinate of the point. 
        /// </summary>
        public int X;
        /// <summary>
        /// Specifies the Y-coordinate of the point. 
        /// </summary>
        public int Y;
    }
}