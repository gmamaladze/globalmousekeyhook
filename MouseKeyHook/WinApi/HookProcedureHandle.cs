﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Gma.System.MouseKeyHook.WinApi
{
    internal class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private static bool _closing = false;

        ~HookProcedureHandle()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => {_closing = true;};
        }

        public HookProcedureHandle()
            : base(true)
        {
            
        }

        protected override bool ReleaseHandle()
        {
            //NOTE Calling Unhook during processexit causes deley
            if (_closing) return true;
            return HookNativeMethods.UnhookWindowsHookEx(handle) != 0;
        }
    }
}