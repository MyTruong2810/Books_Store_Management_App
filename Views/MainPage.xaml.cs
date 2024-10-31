using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace Books_Store_Management_App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        //Todo: Implement code to change the navigation withoit using the if else statement, hardcode
        /// <summary>
        /// Hàm chuyển page theo navigation view được chọn, ở đây đang thực hiện hardcode cần cải thiện
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
            else if (selectedTag == "CustomerPage")
            {
                content.Navigate(typeof(CustomerPage));
            }
            else if (selectedTag == "LogoutPage")
            {
                MainWindow.AppFrame.Navigate(typeof(Views.LoginPage));
            }

            /* ====================================================
             * You: Implement code to change the navigation      ||
             * ====================================================
             */
        }
    }
}
