using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;
using Books_Store_Management_App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    // Dùng để hiển thị thông tin chi tiết của một đơn hàng
    public sealed partial class ReadOnlyOrderDetailPage : Page
    {
        public OrderDetailViewModel ViewModel { get; set; }
        public OrderPageViewModel OrderViewModel { get; set; }
        public ReadOnlyOrderDetailPage()
        {
            this.InitializeComponent();

            // Lấy ra OrderDetailViewModel và OrderPageViewModel từ ServiceProvider
            // Trong đó OrderDetailViewModel chứa thông tin chi tiết của một đơn hàng
            // OrderPageViewModel chứa thông tin của tất cả các đơn hàng
            ViewModel = (Application.Current as App).ServiceProvider.GetService<OrderDetailViewModel>();
            OrderViewModel = (Application.Current as App).ServiceProvider.GetService<OrderPageViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CouponsComboBox.ItemsSource = ViewModel.Coupons;

            // Đoạn này bí quá làm dài dòng
            if (e.Parameter is Order order)
            {
                // Gán thông tin của đơn hàng vào ViewModel, để hiển thị lên giao diện
                ViewModel.Order = order;
                ViewModel.CustomerName = order.Customer;
                ViewModel.PurchaseDate = DateTime.Parse(order.Date);
                ViewModel.IsDelivered = order.IsDelivered;

                // Thêm sách và mã giảm giá đã chọn vào SelectedBooks và SelectedCoupons
                ViewModel.AddSelectedBooks(order.OrderItems);
                ViewModel.AddSelectedCoupons(order.Coupons);

                // Hiển thị thông tin sách và mã giảm giá đã chọn lên giao diện
                CouponsComboBox.SelectedItems.Clear();
                var selectedCoupons = ViewModel.Coupons
                    .Where(coupon => ViewModel.SelectedCoupons.Any(selected => selected.Id == coupon.Id))
                    .ToList();
                selectedCoupons.ForEach(coupon => CouponsComboBox.SelectedItems.Add(coupon));

                BookSelectionComboBox.SelectedItems.Clear();
                var selectedBooks = ViewModel.Books
                    .Where(book => ViewModel.SelectedBooks.Any(selected => selected.Book.Index == book.Index))
                    .ToList();
                selectedBooks.ForEach(book => BookSelectionComboBox.SelectedItems.Add(book));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrderPage));
        }

        private void BillOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Order.Date = ViewModel.PurchaseDate.ToString();
            ViewModel.Order.OrderItems = ViewModel.SelectedBooks.ToList();
            ViewModel.Order.Coupons = ViewModel.SelectedCoupons;

            Frame.Navigate(typeof(InvoicePage), ViewModel.Order);
        }

    }

}