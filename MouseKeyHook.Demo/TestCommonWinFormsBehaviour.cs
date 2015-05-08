﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Windows.Forms;

namespace Gma.System.MouseKeyHook.Demo
{
    public partial class TestCommonWinFormsBehaviour : Form
    {
        public TestCommonWinFormsBehaviour()
        {
            InitializeComponent();
        }

        private void clickableArea_MouseClick(object sender, MouseEventArgs e)
        {
            Log("MouseClick", e);
        }

        private void clickableArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Log("MouseDoubleClick", e);
        }

        private void clickableArea_MouseDown(object sender, MouseEventArgs e)
        {
            Log("MouseDown", e);
        }

        private void clickableArea_MouseUp(object sender, MouseEventArgs e)
        {
            Log("MouseUp", e);
        }

        private void Log(string eventName, MouseEventArgs e)
        {
            Log(string.Format("{0}\t{1} {2}\n", eventName, e.Button, e.Clicks));
        }

        private void Log(string text)
        {
            textBoxLog.AppendText(text);
            textBoxLog.ScrollToCaret();
        }
    }
}