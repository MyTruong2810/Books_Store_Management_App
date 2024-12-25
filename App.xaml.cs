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

            // Áp dụng theme khi khởi chạy
            ApplySavedTheme();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IDao<Order>, MockOrderDao>();
            //services.AddSingleton<IDao<Book>, MockBookDao>();
            //services.AddTransient<OrderViewModel>();
            services.AddSingleton<OrderPageViewModel>();
            services.AddTransient<OrderDetailViewModel>();
        }

        private void ApplySavedTheme()
        {
            // Kiểm tra theme đã được lưu trong Settings
            var themeToApply = SettingsViewModel.CurrentTheme;
             
            // Đảm bảo MainWindow đã được tạo trước khi áp dụng theme
            if (MainWindow != null)
            {
                // Áp dụng theme cho MainWindow khi cửa sổ đã được tạo
                MainWindow.Activated += (sender, e) =>
                {
                    if (MainWindow.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = themeToApply;
                        // Cập nhật tài nguyên động khi theme thay đổi
                        UpdateThemeResources(themeToApply);
                    }
                };
            }
            else
            {
                // Nếu MainWindow chưa được tạo, có thể đăng ký lại khi MainWindow được khởi tạo
                System.Diagnostics.Debug.WriteLine("MainWindow is not initialized yet.");
            }
        }

        private void UpdateThemeResources(ElementTheme theme)
        {
            var resourceDict = (ResourceDictionary)Application.Current.Resources;
            resourceDict.MergedDictionaries.Clear();

            // Thêm các tài nguyên tương ứng với theme
            if (theme == ElementTheme.Dark)
            {
                resourceDict.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Dark.xaml") });
            }
            else
            {
                resourceDict.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Light.xaml") });
            }
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Activate();
            // Áp dụng theme sau khi MainWindow được tạo
            // Lấy HWND sau khi cửa sổ chính được tạo
            // Đảm bảo rằng bạn chỉ gọi ApplySavedTheme sau khi MainWindow đã được tạo
            MainWindow.Activated += (sender, e) =>
            {
                // Lấy HWND sau khi cửa sổ chính được tạo
                var hwnd = WindowNative.GetWindowHandle(MainWindow);

                if (hwnd == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Unable to retrieve window handle (HWND).");
                }

                var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
                appWindow.SetIcon("Assets/Icons/icon.ico");

                // Áp dụng theme sau khi MainWindow đã sẵn sàng
                ApplySavedTheme();
            };
        }
    }
}