using Books_Store_Management_App;
using Books_Store_Management_App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Books_Store_Management_App.Models;
using WinRT.Interop;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using SkiaSharp;

namespace Books_Store_Management_App
{
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        public static SettingsViewModel SettingsViewModel { get; } = new SettingsViewModel();
        public IServiceProvider ServiceProvider { get; private set; }
        // Sử dụng thuộc tính tĩnh MainWindow
        public static Window MainWindow { get; private set; }

        public App()
        {
            this.InitializeComponent();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IDao<Order>, MockOrderDao>();
            //services.AddSingleton<IDao<Book>, MockBookDao>();
            //services.AddTransient<OrderViewModel>();
            services.AddSingleton<OrderPageViewModel>();
            services.AddTransient<OrderDetailViewModel>();
        }

        // Áp dụng theme được chọn (gọi từ nơi cần thiết, ví dụ từ SettingPage)
        public void ApplySelectedTheme()
        {
            var themeToApply = SettingsViewModel.CurrentTheme;

            if (MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = themeToApply;
                SettingsViewModel.TitlePaint.Color = SettingsViewModel.IsDarkModeEnabled ? SKColors.White : SKColors.Black;
                SettingsViewModel.AxisNamePaint.Color = SettingsViewModel.IsDarkModeEnabled ? SKColors.LightGray : SKColors.DarkGray;
                UpdateThemeResources(themeToApply);
            }
        }

        public void UpdateThemeResources(ElementTheme theme)
        {
            var dictionaries = Application.Current.Resources.MergedDictionaries;

            // Tìm và xóa các từ điển có chứa tài nguyên liên quan đến theme
            var themeDictionaries = dictionaries.Where(d =>
                d.Source != null &&
                (d.Source.AbsoluteUri.Contains("Themes/Light.xaml") ||
                 d.Source.AbsoluteUri.Contains("Themes/Dark.xaml"))).ToList();

            foreach (var dict in themeDictionaries)
            {
                dictionaries.Remove(dict);
            }

            // Thêm tài nguyên theme mới
            if (theme == ElementTheme.Dark)
            {
                dictionaries.Add(new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Dark.xaml") });
            }
            else
            {
                dictionaries.Add(new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Light.xaml") });
            }
        }

        private void SetWindowIcon()
        {
            // Lấy HWND sau khi cửa sổ chính được tạo
            var hwnd = WindowNative.GetWindowHandle(MainWindow);
            if (hwnd == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to retrieve window handle (HWND).");
            }

            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.SetIcon("Assets/Icons/icon.ico");
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Activate();

            // Áp dụng theme mặc định (Light Mode) khi ứng dụng khởi động
            if (MainWindow.Content is FrameworkElement rootElement)
            {
                if (App.Current is App appInstance)
                {
                    appInstance.UpdateThemeResources(ElementTheme.Light);
                }
                rootElement.RequestedTheme = ElementTheme.Light;
            }
            // Thiết lập biểu tượng cho cửa sổ
            SetWindowIcon();
        }
    }
}