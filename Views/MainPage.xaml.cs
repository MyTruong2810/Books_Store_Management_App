using Books_Store_Management_App.ViewModels;
using Catel.MVVM;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Books_Store_Management_App.Views
{
    public sealed partial class MainPage : Page
    {
        public string username = "";
        public MainPage()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                username = e.Parameter.ToString();
                navAdmin.Content = username;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["username"] = username;
            }
        }

        /// <summary>
        /// Hàm chuyển trang khi chọn mục trong NavigationView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void nvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
                return;

            var selectedItem = args.SelectedItem as NavigationViewItem;
            string selectedTag = selectedItem?.Tag?.ToString();

            switch (selectedTag)
            {
                case "DashboardPage":
                    content.Navigate(typeof(DashboardPage));
                    break;

                case "StockPage":
                    content.Navigate(typeof(StockPage));
                    break;

                case "OrderPage":
                    content.Navigate(typeof(OrderPage));
                    break;

                case "Admin":
                    content.Navigate(typeof(AdminPage));
                    break;

                case "ClassificationPage":
                    content.Navigate(typeof(ClassificationPage));
                    break;

                case "StatisticsPage":
                    content.Navigate(typeof(StatisticsPage));
                    break;

                case "CustomerPage":
                    content.Navigate(typeof(CustomerPage));
                    break;

                case "SettingPage":
                    content.Navigate(typeof(SettingPage));
                    break;

                case "LogoutPage":
                    HandleLogout();
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine($"Unhandled navigation tag: {selectedTag}");
                    break;
            }
        }

        private void HandleLogout()
        {
            // Đặt lại theme về mặc định (Light Mode)
            App.SettingsViewModel.IsDarkModeEnabled = false;

            if (App.MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = App.SettingsViewModel.CurrentTheme;

                if (App.Current is App appInstance)
                {
                    appInstance.UpdateThemeResources(App.SettingsViewModel.CurrentTheme);
                }
            }

            // Điều hướng về trang đăng nhập
            MainWindow.AppFrame.Navigate(typeof(LoginPage));

            // Xóa thông tin người dùng khỏi LocalSettings (nếu cần)
            Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove("username");
        }

    }
}
