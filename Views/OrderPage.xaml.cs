using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.Services.Maps;
using Microsoft.UI.Xaml.Documents;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Windows.UI.Popups;
using Microsoft.UI;
using System.ComponentModel;
using static Books_Store_Management_App.Views.DashboardPage;
using System.Drawing;
using Books_Store_Management_App.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Lớp OrderPage hiển thị danh sách các đơn hàng
    /// </summary>
    public sealed partial class OrderPage : Page
    { 
        public int[] ShowEntities = { 5, 10, 15, 20 }; // Chọn hiển thị số đơn hàng trên mỗi trang
        public ObservableCollection<Order> AllOrdersDisplay { get; set; } = new ObservableCollection<Order>(); // Danh sách tất cả các đơn hàng
        public ObservableCollection<Order> DisplayedOrders { get; set; } = new ObservableCollection<Order>(); // Danh sách đơn hàng hiển thị
        public int[] monthSearch = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }; // Tìm kiếm theo tháng
        public string[] priceSearch = { "Greater than $100", "Smaller than $100" }; // Tìm kiếm theo giá
        private int ItemsPerPage = 10; // Số đơn hàng trên mỗi trang
        private int currentPage = 1;  // Page hiện tại được gán giá trị đầu tiên bằng 1
        private int totalPages; // Tổng số trang

        /// <summary>
        /// Lớp OrderPageViewModel được gọi để thực hiện các nghiệp vụ liên quan đến đơn hàng theo mô hình MVVM
        /// </summary>
        public OrderPageViewModel ViewModel { get; set; }


        /// <summary>
        /// Cấp phát và khởi tạo các đối tượng trong lớp OrderPage, gọi tất cả các orders, 
        /// thực hiện tình tổng số trang và hiện thị các trang  trên màn hình theo thông số đã cài đặt 
        /// </summary>
        public OrderPage()
        {
            this.InitializeComponent();

            // ViewModel dùng chung
            ViewModel = (Microsoft.UI.Xaml.Application.Current as App).ServiceProvider.GetService<OrderPageViewModel>();

            AllOrdersDisplay = ViewModel.Orders;
            totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
            UpdateDisplayedOrders();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string previousPage = e.Parameter as string;
            
            if (previousPage == nameof(OrderDetailPage))
            {
                AllOrdersDisplay = ViewModel.Orders;
                totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
                UpdateDisplayedOrders();
            }

        }
        /// <summary>
        /// Update các đơn hàng theo thông số được chọn bao gồm số đơn hàng trên mỗi trang, tính toán paging
        /// </summary>
        private void UpdateDisplayedOrders()
        {
            var skip = (currentPage - 1) * ItemsPerPage;
            DisplayedOrders.Clear();
            int cnt = 0;
            foreach (var Order in AllOrdersDisplay)
            {
                Order.Index = cnt;
                cnt++;
            }

            foreach (var Order in AllOrdersDisplay.Skip(skip).Take(ItemsPerPage))
            {
                DisplayedOrders.Add(Order);
            }
            PageInfo.Text = $"Page {currentPage} of {totalPages}";
            PreviousButton.IsEnabled = currentPage > 1;
            NextButton.IsEnabled = currentPage < totalPages;
        }

        /// <summary>
        /// Chuyển sang page kế tiếp 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdateDisplayedOrders();
            }
        }

        /// <summary>
        /// Quay trở lại trang trước đó
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDisplayedOrders();
            }
        }

        /// <summary>
        /// Thêm đơn hàng mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            //New add page
            Frame.Navigate(typeof(OrderDetailPage));
        }

        /// <summary>
        /// Xoá đơn hàng được chọn xoá
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var Order = (sender as Button).DataContext as Order;
            AllOrdersDisplay.Remove(Order);
            totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
            if (currentPage > totalPages) currentPage = totalPages; // Adjust page if last page is removed

            UpdateDisplayedOrders();
        }

        /// <summary>
        /// Chỉnh sửa đơn hàng được chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            //New edit page
            Frame.Navigate(typeof(OrderDetailPage), (sender as Button).DataContext);
        }
        /// <summary>
        /// Hàm này thực hiện lấy tên của cột được chọn (property) và chọn thứ tự sắp xếp tăng hay giảm (order)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublisherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuFlyoutItem;
            if (menuItem != null)
            {
                string[] parameters = menuItem.CommandParameter.ToString().Split(',');
                string property = parameters[0];
                string order = parameters[1];

                SortOrders(property, order);
            }
        }

        /// <summary>
        /// Hàm sắp xếp khi chọn button trên đầu mỗi cột theo 2 chế độ ASC, DES
        /// </summary>
        /// <param name="property">Tên của cột được chọn sẳp xếp</param>
        /// <param name="order">Có 2 chế độ được chọn ASC, DES được truyển vào order</param>
        private void SortOrders(string property, string order)
        {
            if (order == "ASC")
            {
                var sortedOrders = AllOrdersDisplay.OrderBy(b => b.GetType().GetProperty(property).GetValue(b)).ToList();
                AllOrdersDisplay.Clear();
                int cnt = 0;
                foreach (var Order in sortedOrders)
                {
                    Order.Index = cnt;
                    AllOrdersDisplay.Add(Order);
                    cnt++;
                }
                UpdateDisplayedOrders();

            }
            else if (order == "DES")
            {
                var sortedOrders = AllOrdersDisplay.OrderByDescending(b => b.GetType().GetProperty(property).GetValue(b)).ToList();
                AllOrdersDisplay.Clear();
                int cnt = 0;
                foreach (var Order in sortedOrders)
                {
                    Order.Index = cnt;
                    AllOrdersDisplay.Add(Order);
                    cnt++;
                }
                UpdateDisplayedOrders();
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Hàm này nhận vào thông số yêu cầu hiện thị số lượng mỗi row trên 1 listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Combo3_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            // Get the submitted text
            string submittedText = args.Text;

            // Attempt to convert the submitted text to an integer
            if (int.TryParse(submittedText, out int itemsPerPage))
            {
                ItemsPerPage = itemsPerPage;
            }
        }
        /// <summary>
        /// Hàm thực hiện Search theo tên của khách hàng ứng với order và hàm này thực hiện autotextsearch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredOrders = ViewModel.Orders.Where(order =>
                    order.Customer.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                DisplayedOrders.Clear();
                foreach (var order in filteredOrders)
                {
                    DisplayedOrders.Add(order);
                }
            }
            else
            {
                UpdateDisplayedOrders();
            }
        }
        /// <summary>
        /// Chọn một order để xem chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var order = e.ClickedItem as Order;
            Frame.Navigate(typeof(ReadOnlyOrderDetailPage), order);
        }
    }
}