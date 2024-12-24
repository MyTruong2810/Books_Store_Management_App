using System;
using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Lớp giúp lấy handle của cửa sổ WinUI.
    /// </summary>
    public static class WindowHelper
    {
        /// <summary>
        /// Hàm lấy handle của cửa sổ WinUI.
        /// </summary>
        /// <param name="window">The WinUI Window for which to get the handle.</param>
        /// <returns>The window handle as an IntPtr.</returns>
        public static IntPtr GetWindowHandle(Window window)
        {
            // Use the WindowNative class from the WinRT.Interop namespace to get the window handle
            return WindowNative.GetWindowHandle(window);
        }
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void ShowWindow(Window window)
        {
            // Bring the window to the foreground... first get the window handle...
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Restore window if minimized... requires DLL import above
            ShowWindow(hwnd, 0x00000009);

            // And call SetForegroundWindow... requires DLL import above
            SetForegroundWindow(hwnd);
        }
    }
}