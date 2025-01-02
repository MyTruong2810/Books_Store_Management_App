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

        // DLL imports để hiển thị và đưa cửa sổ lên phía trước
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // DLL imports để hiển thị và đưa cửa sổ lên phía trước
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        /// <summary>
        /// Hiển thị cửa sổ WinUI.
        /// </summary>
        /// <param name="window"></param>
        public static void ShowWindow(Window window)
        {
            // Đưa cửa sổ lên phía trước... trước tiên lấy handle của cửa sổ...
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Khôi phục cửa sổ nếu đã bị thu nhỏ... yêu cầu DLL import ở trên
            // 0x00000009 là mã lệnh cho SW_RESTORE
            ShowWindow(hwnd, 0x00000009);

            // Đưa cửa sổ lên phía trước
            SetForegroundWindow(hwnd);
        }
    }
}