using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Books_Store_Management_App.Models;
using Books_Store_Management_App.ViewModels;
namespace Books_Store_Management_App.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardViewModel ViewModel { get; set; }

        public DashboardPage()
        {
            this.InitializeComponent();
            ViewModel = new DashboardViewModel();
            ViewModel.Init();
        }
        /// <summary>
        /// Hàm chuyển tab hiện thị các thông số về tổng sách, tổng khách hàng, tổng đơn hàng, tổng doanh thu ==> Cài đặt sau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.IsChecked == true)
                {
                    // Todo: Implement code later
                }
            }
        }
    }
}
