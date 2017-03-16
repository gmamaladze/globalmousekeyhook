// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo
{
    public partial class Main : Form
    {
        private IKeyboardMouseEvents m_Events;
        List<string> lsEvents = new List<string>();
        List<KeyValuePair<string, string>> lsWriteMousePositions = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> lsReadMousePositions = new List<KeyValuePair<string, string>>();
        IEnumerable<String> lines;
        int mousePosX, mousePosY;
        string eventName = "";
        string eventValue = "";
        int a, b;

        public Main()
        {
            InitializeComponent();
            radioGlobal.Checked = true;
            FormClosing += Main_Closing;
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeApplication()
        {
            Unsubscribe();
            Subscribe(Hook.AppEvents());
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            m_Events.MouseDragStarted += OnMouseDragStarted;
            m_Events.MouseDragFinished += OnMouseDragFinished;

            if (checkBoxSupressMouseWheel.Checked)
                m_Events.MouseWheelExt += HookManager_MouseWheelExt;
            else
                m_Events.MouseWheel += HookManager_MouseWheel;

            if (checkBoxSuppressMouse.Checked)
                m_Events.MouseDownExt += HookManager_Supress;
            else
                m_Events.MouseDown += OnMouseDown;
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.MouseUp -= OnMouseUp;
            m_Events.MouseClick -= OnMouseClick;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;

            m_Events.MouseMove -= HookManager_MouseMove;

            m_Events.MouseDragStarted -= OnMouseDragStarted;
            m_Events.MouseDragFinished -= OnMouseDragFinished;

            if (checkBoxSupressMouseWheel.Checked)
                m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
            else
                m_Events.MouseWheel -= HookManager_MouseWheel;

            if (checkBoxSuppressMouse.Checked)
                m_Events.MouseDownExt -= HookManager_Supress;
            else
                m_Events.MouseDown -= OnMouseDown;

            m_Events.Dispose();
            m_Events = null;
        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                lsEvents.Add("MouseDown|" + e.Button + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
                Log(string.Format("MouseDown|" + e.Button));
                return;
            }

            lsEvents.Add("MouseDownSuppressed|" + e.Button + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("MouseDownSuppressed|" + e.Button));
            e.Handled = true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            b++;
            lsEvents.Add("KeyDown|" + e.KeyCode + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("KeyDown|" + e.KeyCode));
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            b++;
            lsEvents.Add("KeyUp|" + e.KeyCode + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("KeyUp|" + e.KeyCode));
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            b++;
            if (e.KeyChar == '\r')
            {
                lsEvents.Add("KeyPress|" + "{ENTER}" + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            }
            else if (e.KeyChar == '\t')
            {
                lsEvents.Add("KeyPress|" + "{TAB}" + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            }
            else
            {
                lsEvents.Add("KeyPress|" + e.KeyChar + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            }
            Log(string.Format("KeyPress|" + e.KeyChar));
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            b++;
            //lsX.Add(e.X);
            //lsY.Add(e.Y);
            //lsEvents.Add("Mouse X value:" + Cursor.Position.X + " Mouse Y value:" + Cursor.Position.Y);
            Log(string.Format("MouseMove|" + e.X + "," + e.Y));
            //lsEvents.Add(new KeyValuePair<object, object>(sender, e));
            //HookManager_MouseMove(sender, e);
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.Add("MouseDown|" + e.Button + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("MouseDown|" + e.Button));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.Add("MouseUp|" + e.Button + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("MouseUp|" + e.Button));
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.Add("MouseClick|" + e.Button + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("MouseClick|" + e.Button));
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.Add("MouseDoubleClick|" + e.Button + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log(string.Format("MouseDoubleClick|" + e.Button));
        }

        private void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.RemoveAt(lsEvents.Count - 2);
            lsEvents.Add("MouseDrag|Started" + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log("MouseDrag|MouseDragStarted");
        }

        private void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.RemoveRange(lsEvents.Count - 2, 2);
            lsEvents.Add("MouseDrag|Finished" + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            Log("MouseDrag|MouseDragFinished");
        }

        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            b++;
            lsEvents.Add("Wheel|" + e.Delta + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }

        private void HookManager_MouseWheelExt(object sender, MouseEventExtArgs e)
        {
            b++;
            lsEvents.Add("MouseWheelSupress|" + e.Delta + "|" + Cursor.Position.X + "," + Cursor.Position.Y);
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
            Log("Mouse Wheel Move Suppressed.\n");
            e.Handled = true;
        }

        private void Log(string text)
        {
            if (IsDisposed) return;
            DateTime time = DateTime.Now;
            textBoxLog.AppendText(text + "|" + time.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + "\n");
            textBoxLog.ScrollToCaret();
        }

        private void radioApplication_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) SubscribeApplication();
        }

        private void radioGlobal_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) SubscribeGlobal();
        }

        private void radioNone_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) Unsubscribe();
        }

        private void checkBoxSupressMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Events == null) return;

            if (((CheckBox)sender).Checked)
            {
                m_Events.MouseWheel -= HookManager_MouseWheel;
                m_Events.MouseWheelExt += HookManager_MouseWheelExt;
            }
            else
            {
                m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
                m_Events.MouseWheel += HookManager_MouseWheel;
            }
        }

        private void checkBoxSuppressMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Events == null) return;

            if (((CheckBox)sender).Checked)
            {
                m_Events.MouseDown -= OnMouseDown;
                m_Events.MouseDownExt += HookManager_Supress;
            }
            else
            {
                m_Events.MouseDownExt -= HookManager_Supress;
                m_Events.MouseDown += OnMouseDown;
            }
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }
        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        public static void ClickLeftMouseButton()
        {
            MouseLeftDown();
            MouseLeftUp();
        }
        public static void ClickRightMouseButton()
        {
            MouseRightDown();
            MouseRightUp();
        }

        public static void MouseLeftDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
        }

        public static void MouseLeftUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }

        public static void MouseRightDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
        }

        public static void MouseRightUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }

        private void MouseMove(int x, int y)
        {
            INPUT mouseMoveInput = new INPUT();
            mouseMoveInput.type = SendInputEventType.InputMouse;
            mouseMoveInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE;
            mouseMoveInput.mkhi.mi.dx = x;
            mouseMoveInput.mkhi.mi.dy = y;
            SendInput(1, ref mouseMoveInput, Marshal.SizeOf(new INPUT()));
        }

        //start
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            SubscribeGlobal();
        }

        /// <summary>
        /// Play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            ReadFromFile();
            lsEvents = lines.ToList();
            a = Convert.ToInt32(lsEvents[lsEvents.Count - 1].Split(',')[0]);
            b = Convert.ToInt32(lsEvents[lsEvents.Count - 1].Split(',')[1]);
            lsEvents.RemoveAt(lsEvents.Count - 1);
            timer2.Start();
        }

        /// <summary>
        /// Stop button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            Unsubscribe();
            WriteToFile(lsEvents);
            timer1.Stop();
            timer2.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lsEvents.Add("MouseMove|" + Cursor.Position.X.ToString() + "," + Cursor.Position.Y.ToString() + "|" + Cursor.Position.X.ToString() + "," + Cursor.Position.Y.ToString());
            b++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (a != b)
            {
                mousePosX = int.Parse(lsEvents[a].Split('|')[2].Split(',')[0]);
                mousePosY = int.Parse(lsEvents[a].Split('|')[2].Split(',')[1]);
                eventName = lsEvents[a].Split('|')[0];
                eventValue = lsEvents[a].Split('|')[1];

                //check for non value key inputs

                switch (eventName)
                {
                    case "MouseMove":
                        //trigger mousemove
                        Cursor.Position = new Point(mousePosX, mousePosY);
                        break;
                           case "MouseClick":
                        //trigger mouseclick
                        if (eventValue == "Left")
                        {
                            ClickLeftMouseButton();
                        }
                        else
                        {
                            ClickRightMouseButton();
                        }
                        break;
                    case "MouseDrag":
                        //trigger mouse drag
                        if (eventValue == "Started")
                        {
                            MouseLeftDown();
                        }
                        else if (eventValue == "Finished")
                        {
                            MouseLeftUp();
                        }
                        break;
                    case "KeyDown":
                        //implement keydown
                        if (eventValue.Contains("ControlKey"))
                        {
                            SendKeys.Send("^");
                        }
                        else if (eventValue == "Delete")
                        {
                            SendKeys.Send("{DELETE}");
                        }
                        else if (eventValue.Contains("ShiftKey"))
                        {
                            SendKeys.Send("+");
                        }
                        else if (eventValue.Contains("Menu"))
                        {
                            SendKeys.Send("%");
                        }
                        break;
                    case "KeyUp":
                        //implement keyup
                        break;
                    case "KeyPress":
                        //implement keypress
                        SendKeys.Send(eventValue);
                        break;
                    default:
                        //trigger default
                        break;
                }
                a++;
            }
            else
            {
                timer2.Stop();
                a = 0;
            }
        }

        private void WriteToFile(List<string> ls)
        {
            try
            {
                lsEvents.Add(a.ToString() + "," + b.ToString());
                File.WriteAllLines(@"D:\Souvik\New folder\globalmousekeyhook-vNext\globalmousekeyhook-vNext\recordedevents.txt", lsEvents); //modify the file path as required
            }
            catch
            {
                throw;
            }
        }

        private IEnumerable<String> ReadFromFile()
        {
            lines = File.ReadLines(@"D:\Souvik\New folder\globalmousekeyhook-vNext\globalmousekeyhook-vNext\recordedevents.txt"); //modify the file path as required
            return lines;
        }

    }
}
