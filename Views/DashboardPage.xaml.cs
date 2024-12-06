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
using Windows.ApplicationModel.Appointments;
using System.Diagnostics.Eventing.Reader;

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Lớp xử lý các sự kiện của trang Dashboard.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashboardViewModel ViewModel { get; set; }

        private double temp;
        public DashboardPage()
        {
            this.InitializeComponent();
            ViewModel = new DashboardViewModel();
            ViewModel.Init();
            temp = ViewModel.totalRevenue;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            ViewModel.totalRevenue = temp;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            ViewModel.totalRevenue = 678;
        }
    }
}
