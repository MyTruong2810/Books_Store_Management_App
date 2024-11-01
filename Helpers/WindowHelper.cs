using System;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// A helper class to provide utility methods related to Windows handling in WinUI applications.
    /// </summary>
    public static class WindowHelper
    {
        /// <summary>
        /// Retrieves the window handle (IntPtr) for the specified WinUI Window.
        /// </summary>
        /// <param name="window">The WinUI Window for which to get the handle.</param>
        /// <returns>The window handle as an IntPtr.</returns>
        public static IntPtr GetWindowHandle(Window window)
        {
            // Use the WindowNative class from the WinRT.Interop namespace to get the window handle
            return WindowNative.GetWindowHandle(window);
        }
    }
}