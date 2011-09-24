using System;
using System.Runtime.InteropServices;

namespace MouseKeyboardActivityMonitor.WinApi
{

    /// <summary>
    /// The <see cref="GlobalMouseStruct"/> structure contains information about a system-level mouse input event.
    /// </summary>
    /// <remarks>
    /// See full documentation at TODO: Post Online Documentation
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    internal struct GlobalMouseStruct
    {
        /// <summary>
        /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
        /// </summary>
        [FieldOffset(0x00)]
        public Point Point;

        /// <summary>
        /// Specifies information associated with the message.
        /// </summary>
        /// <remarks>
        /// The possible values are:
        /// <list type="bullet">
        /// <item>
        /// <description>0 - No Information</description>
        /// </item>
        /// <item>
        /// <description>1 - X-Button1 Click</description>
        /// </item>
        /// /// <item>
        /// <description>2 - X-Button2 Click</description>
        /// </item>
        /// /// <item>
        /// <description>120 - Mouse Scroll Away from User</description>
        /// </item>
        /// /// <item>
        /// <description>-120 - Mouse Scroll Toward User</description>
        /// </item>
        /// </list>
        /// </remarks>
        [FieldOffset(0x0A)]
        public Int16 MouseData;

        /// <summary>
        /// Returns a Timestamp associated with the input, in System Ticks.
        /// </summary>
        [FieldOffset(0x10)]
        public Int32 Timestamp;

        /// <summary>
        /// Converts the current <see cref="GlobalMouseStruct"/> into a <see cref="MouseStruct"/>.
        /// </summary>
        /// <returns></returns>
        public MouseStruct ToMouseStruct()
        {
            MouseStruct tmp = new MouseStruct();
            tmp.Point = this.Point;
            tmp.MouseData = this.MouseData;
            tmp.Timestamp = this.Timestamp;
            return tmp;
        }
    }

    /// <summary>
    /// The AppMouseStruct structure contains information about a application-level mouse input event.
    /// </summary>
    /// <remarks>
    /// See full documentation at TODO: Post Online Documentation
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    internal struct AppMouseStruct
    {

        /// <summary>
        /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
        /// </summary>
        [FieldOffset(0x00)]
        public Point Point;

        /// <summary>
        /// Specifies information associated with the message.
        /// </summary>
        /// <remarks>
        /// The possible values are:
        /// <list type="bullet">
        /// <item>
        /// <description>0 - No Information</description>
        /// </item>
        /// <item>
        /// <description>1 - X-Button1 Click</description>
        /// </item>
        /// /// <item>
        /// <description>2 - X-Button2 Click</description>
        /// </item>
        /// /// <item>
        /// <description>120 - Mouse Scroll Away from User</description>
        /// </item>
        /// /// <item>
        /// <description>-120 - Mouse Scroll Toward User</description>
        /// </item>
        /// </list>
        /// </remarks>
#if IS_X64
        [FieldOffset(0x22)]
#else
        [FieldOffset(0x16)]
#endif
        public Int16 MouseData;

        /// <summary>
        /// Converts the current <see cref="AppMouseStruct"/> into a <see cref="MouseStruct"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The AppMouseStruct does not have a timestamp, thus one is generated at the time of this call.
        /// </remarks>
        public MouseStruct ToMouseStruct()
        {
            MouseStruct tmp = new MouseStruct();
            tmp.Point = this.Point;
            tmp.MouseData = this.MouseData;
            tmp.Timestamp = Environment.TickCount; // 0;
            return tmp;
        }
    }

    /// <summary>
    /// The MouseStruct contains information regarding the mouse input event.
    /// </summary>
    internal struct MouseStruct
    {
        /// <summary>
        /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
        /// </summary>
        public Point Point;

        /// <summary>
        /// Specifies information associated with the message.
        /// </summary>
        /// <remarks>
        /// The possible values are:
        /// <list type="bullet">
        /// <item>
        /// <description>0 - No Information</description>
        /// </item>
        /// <item>
        /// <description>1 - X-Button1 Click</description>
        /// </item>
        /// /// <item>
        /// <description>2 - X-Button2 Click</description>
        /// </item>
        /// /// <item>
        /// <description>120 - Mouse Scroll Away from User</description>
        /// </item>
        /// /// <item>
        /// <description>-120 - Mouse Scroll Toward User</description>
        /// </item>
        /// </list>
        /// </remarks>
        public Int16 MouseData;

        /// <summary>
        /// Returns a Timestamp associated with the input, in System Ticks.
        /// </summary>
        /// <remarks>
        /// For Application Hooks, the Timestamp is generated during hook processing, not at the instant the message occured.
        /// </remarks>
        public Int32 Timestamp;

    }

}
