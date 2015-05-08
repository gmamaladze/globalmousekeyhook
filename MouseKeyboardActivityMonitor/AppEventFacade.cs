// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

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