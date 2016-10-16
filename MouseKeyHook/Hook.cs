// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using Gma.System.MouseKeyHook.HotKeys;
using Gma.System.MouseKeyHook.Implementation;

namespace Gma.System.MouseKeyHook
{
    /// <summary>
    ///     This is the class to start with.
    /// </summary>
    public static class Hook
    {
        /// <summary>
        ///     Here you find all application wide events. Both mouse and keyboard.
        /// </summary>
        /// <returns>
        ///     Returned instance is used for event subscriptions.
        ///     You can refetch it (you will get the same instance anyway).
        /// </returns>
        public static IKeyboardMouseEvents AppEvents()
        {
            return new AppEventFacade();
        }

        /// <summary>
        ///     Here you find all application wide events. Both mouse and keyboard.
        /// </summary>
        /// <returns>
        ///     Returned instance is used for event subscriptions.
        ///     You can refetch it (you will get the same instance anyway).
        /// </returns>
        public static IKeyboardMouseEvents GlobalEvents()
        {
            return new GlobalEventFacade();
        }

        /// <summary>
        ///     Provides a means to manage application HotKeySets.
        /// </summary>
        /// <returns>
        ///     Returned instance is used for managing HotKeySets.
        /// </returns>
        public static IHotKeyManager AppHotkeyManager()
        {
            return new AppHotKeySetsManager();
        }

        /// <summary>
        ///     Provides a means to manage global HotKeySets.
        /// </summary>
        /// <returns>
        ///     Returned instance is used for managing HotKeySets.
        /// </returns>
        public static IHotKeyManager GlobalHotkeyManager()
        {
            return new GlobalHotKeySetsManager();
        }
    }
}