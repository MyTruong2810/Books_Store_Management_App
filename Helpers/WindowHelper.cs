using System;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace Books_Store_Management_App.Helpers;
public static class WindowHelper
{
    public static IntPtr GetWindowHandle(Window window)
    {
        return WindowNative.GetWindowHandle(window);
    }

}