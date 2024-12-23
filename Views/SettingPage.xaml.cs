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
    public sealed partial class SettingPage : Page
    {
        private bool _isToggling; // Biến cờ để ngăn vòng lặp

        private readonly SettingsViewModel _viewModel;

        public SettingPage()
        {
            this.InitializeComponent();
            DataContext = App.SettingsViewModel; // Sử dụng SettingsViewModel toàn cục
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
                
            if (DataContext is SettingsViewModel viewModel && sender is ToggleSwitch toggleSwitch)
            {
                // Đồng bộ trạng thái của ViewModel với ToggleSwitch
                viewModel.IsDarkModeEnabled = toggleSwitch.IsOn;

                // Áp dụng theme cho root element
                if (App.MainWindow.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = viewModel.CurrentTheme; // Cập nhật theme cho root element
                }
            }
        }
    }
}
