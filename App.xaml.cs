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

        /// <summary>
        /// Handles the application launch event and initializes the main window.
        /// </summary>
        /// <param name="args">Launch activation arguments.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Activate();

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
    }
}
