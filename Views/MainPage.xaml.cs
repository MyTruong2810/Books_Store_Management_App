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

            if (selectedTag == "DashboardPage")
            {
                content.Navigate(typeof(DashboardPage));
            }
            else if (selectedTag == "StockPage")
            {
                content.Navigate(typeof(StockPage));
            }
            else if (selectedTag == "OrderPage")
            {
                content.Navigate(typeof(OrderPage));
            }
            else if (selectedTag == "Admin")
            {
                content.Navigate(typeof(AdminPage));
            }
            else if (selectedTag == "ClassificationPage")
            {
                content.Navigate(typeof(ClassificationPage));
            }
            else if (selectedTag == "StatisticsPage")
            {
                content.Navigate(typeof(StatisticsPage));
            }
            else if (selectedTag == "CustomerPage")
            {
                content.Navigate(typeof(CustomerPage));
            }
            else if (selectedTag == "LogoutPage")
            {
                MainWindow.AppFrame.Navigate(typeof(LoginPage));
            }
            else if (selectedTag == "StatisticsPage")
            {
                content.Navigate(typeof(StatisticsPage));
            }
            else if (selectedTag == "CustomerPage")
            {
                content.Navigate(typeof(CustomerPage));
            }
            else if (selectedTag == "SettingPage")
            {
                content.Navigate(typeof(SettingPage));
            }
            else if (selectedTag == "LogoutPage")
            {
                App.SettingsViewModel.IsDarkModeEnabled = false; // Đặt lại theme mặc định


                if (DataContext is SettingsViewModel viewModel)
                {

                    // Áp dụng theme cho root element
                    if (App.MainWindow.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = viewModel.CurrentTheme; // Cập nhật theme cho root element
                    }
                }
                MainWindow.AppFrame.Navigate(typeof(LoginPage));
            }
        }
    }
}
