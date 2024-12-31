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


namespace Books_Store_Management_App
{
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 
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
    }
}