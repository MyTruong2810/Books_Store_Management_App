using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Books_Store_Management_App.ViewModels;
using Microsoft.UI.Xaml.Navigation;

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Represents the settings page of the application.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        /// <summary>
        /// A flag to prevent redundant toggling loops.
        /// </summary>
        private bool _isToggling;

        /// <summary>
        /// The ViewModel associated with the settings page.
        /// </summary>
        private readonly SettingsViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingPage"/> class.
        /// </summary>
        public SettingPage()
        {
            this.InitializeComponent();
            DataContext = App.SettingsViewModel; // Use the global SettingsViewModel
        }

        /// <summary>
        /// Handles the toggling of the theme switch.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides additional context.</param>
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel viewModel && sender is ToggleSwitch toggleSwitch)
            {
                // Synchronize the ViewModel state with the ToggleSwitch
                viewModel.IsDarkModeEnabled = toggleSwitch.IsOn;

                // Apply the theme to the root element
                if (App.MainWindow.Content is FrameworkElement rootElement)
                {
                    if (App.Current is App appInstance)
                    {
                        System.Diagnostics.Debug.WriteLine("Updating theme resources...");
                        appInstance.UpdateThemeResources(viewModel.CurrentTheme);
                        System.Diagnostics.Debug.WriteLine("Theme updated successfully.");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("App.Current is not of type App.");
                    }
                    rootElement.RequestedTheme = viewModel.CurrentTheme; // Update theme for the root element
                }
            }
        }
    }
}
