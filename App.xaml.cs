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
using Microsoft.Extensions.Configuration;
using Microsoft.Windows.AppLifecycle;
using Microsoft.Windows.AppNotifications;
using Books_Store_Management_App.Helpers;
using Microsoft.UI.Dispatching;
using System.Diagnostics;
using Books_Store_Management_App.Models;
using Books_Store_Management_App.Services;
using Newtonsoft.Json;
using Books_Store_Management_App.Views;
using WinRT.Interop;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using SkiaSharp;

namespace Books_Store_Management_App
{
    /// <summary>
    /// Represents the main application class that initializes and manages application-level resources and settings.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Provides access to the settings view model instance.
        /// </summary>
        public static SettingsViewModel SettingsViewModel { get; } = new SettingsViewModel();

        /// <summary>
        /// Provides access to the service provider instance for dependency injection.
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Provides access to the main application window instance.
        /// </summary>
        public static Window MainWindow { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IDao<Order>, MockOrderDao>();
            //services.AddSingleton<IDao<Book>, MockBookDao>();
            //services.AddTransient<OrderViewModel>();
            services.AddSingleton<OrderPageViewModel>();
            services.AddTransient<OrderDetailViewModel>();

            services.AddScoped<PaymentRepository>();
            services.AddScoped<PaymentStrategyFactory>();
            services.AddScoped<PaymentService>();
        }

        /// <summary>
        /// Phương thức được gọi khi ứng dụng được khởi chạy.
        /// </summary>
        /// <param name="args">Thông tin về việc khởi chạy ứng dụng.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            //MainWindow.Activate();
			

            // Để đảm bảo tất cả việc xử lý thông báo xảy ra trong cùng một quá trình, đăng ký sự kiện NotificationInvoked trước khi gọi Register().
            // Nếu không, một quá trình mới sẽ được khởi chạy để xử lý thông báo.
            AppNotificationManager notificationManager = AppNotificationManager.Default;
            notificationManager.NotificationInvoked += NotificationManager_NotificationInvoked;
            notificationManager.Register();

            var activatedArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
            var activationKind = activatedArgs.Kind;
            if (activationKind != ExtendedActivationKind.AppNotification)
            {
                LaunchAndBringToForegroundIfNeeded();
            }
            else
            {
                HandleNotification((AppNotificationActivatedEventArgs)activatedArgs.Data);
            }
			
			if (MainWindow.Content is FrameworkElement rootElement)
            {
                if (App.Current is App appInstance)
                {
                    appInstance.UpdateThemeResources(ElementTheme.Light);
                }
                rootElement.RequestedTheme = ElementTheme.Light;
            }

            SetWindowIcon();
        }
		
		/// <summary>
        /// Kiểm tra và khởi chạy ứng dụng nếu cần thiết.
        /// Nếu cửa sổ chính (MainWindow) chưa được khởi tạo, hàm này sẽ tạo mới cửa sổ và đưa nó lên phía trước.
        /// Nếu ứng dụng được kích hoạt thông qua một thông báo ứng dụng, hàm này sẽ xử lý thông báo đó.
        /// </summary>
        private void LaunchAndBringToForegroundIfNeeded()
        {
            if (MainWindow == null)
            {
                MainWindow = new MainWindow();
                MainWindow.Activate();

                // Đồng thời, chúng ta sử dụng helper của chúng tôi để hiển thị cửa sổ, vì nếu được kích hoạt thông qua một thông báo ứng dụng, nó sẽ không
                // kích hoạt cửa sổ một cách chính xác.
                WindowHelper.ShowWindow(MainWindow);
            }
            else
            {
                WindowHelper.ShowWindow(MainWindow);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi thông báo ứng dụng được kích hoạt.
        /// </summary>
        /// <param name="sender">Đối tượng gửi sự kiện.</param>
        /// <param name="args">Thông tin về sự kiện được kích hoạt.</param>
        private void NotificationManager_NotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
        {
            HandleNotification(args);
        }

        /// <summary>
        /// Xử lý sự kiện khi thông báo ứng dụng được kích hoạt.
        /// </summary>
        /// <param name="args">Thông tin về sự kiện được kích hoạt.</param>
        private void HandleNotification(AppNotificationActivatedEventArgs args)
        {
            // Use the dispatcher from the window if present, otherwise the app dispatcher
            var dispatcherQueue = MainWindow?.DispatcherQueue ?? DispatcherQueue.GetForCurrentThread();


            dispatcherQueue.TryEnqueue(async delegate
            {
                if (args.Argument != "")
                {
                    // Get the order
                    var order = JsonConvert.DeserializeObject<Order>(args.Arguments["Order"]);

                    // If the UI app isn't open
                    if (MainWindow == null)
                    {
                        // Close since we're done
                        Process.GetCurrentProcess().Kill();
                    }

                    var newWindow = new Window();
                    var invoicePage = new InvoicePage(newWindow);
                    invoicePage.ViewModel.Order = order;

                    //LaunchAndBringToForegroundIfNeeded();
                    newWindow.Content = invoicePage;
                    newWindow.Activate();

                    return;
                }

                if (args.Arguments == null || !args.Arguments.ContainsKey("action"))
                {
                    return;
                }

                switch (args.Arguments["action"])
                {
                    // Send a background message
                    case "sendMessage":
                        string message = args.UserInput["textBox"].ToString();
                        // TODO: Send it

                        // If the UI app isn't open
                        if (MainWindow == null)
                        {
                            // Close since we're done
                            Process.GetCurrentProcess().Kill();
                        }

                        break;

                    // View a message
                    case "viewMessage":

                        // Launch/bring window to foreground
                        LaunchAndBringToForegroundIfNeeded();

                        // TODO: Open the message
                        break;
                }
            });
		}
		/// <summary>
        /// Toggles the application theme and updates the relevant resources.
        /// </summary>
        public void ApplySelectedTheme()
        {
            var themeToApply = SettingsViewModel.CurrentTheme;

            if (MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = themeToApply;
                UpdateThemeResources(themeToApply);
            }
        }

        /// <summary>
        /// Updates the application resources based on the selected theme.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        public void UpdateThemeResources(ElementTheme theme)
        {
            var dictionaries = Application.Current.Resources.MergedDictionaries;

            // Remove existing theme-related resource dictionaries.
            var themeDictionaries = dictionaries.Where(d =>
                d.Source != null &&
                (d.Source.AbsoluteUri.Contains("Themes/Light.xaml") ||
                 d.Source.AbsoluteUri.Contains("Themes/Dark.xaml"))).ToList();

            foreach (var dict in themeDictionaries)
            {
                dictionaries.Remove(dict);
            }

            // Add the new theme resource dictionary.
            if (theme == ElementTheme.Dark)
            {
                dictionaries.Add(new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Dark.xaml") });
            }
            else
            {
                dictionaries.Add(new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Light.xaml") });
            }
        }

        /// <summary>
        /// Sets the application icon for the main window.
        /// </summary>
        private void SetWindowIcon()
        {
            var hwnd = WindowNative.GetWindowHandle(MainWindow);
            if (hwnd == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to retrieve window handle (HWND).");
            }

            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.SetIcon("Assets/Icons/icon.ico");
        }
    }
}
