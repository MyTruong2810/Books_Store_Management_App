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

            // Áp dụng theme cho MainWindow
            if (MainWindow != null && MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = themeToApply;
            }
        }


        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Activate();
            // Áp dụng theme sau khi MainWindow được tạo
            ApplySavedTheme();
        }
    }
}