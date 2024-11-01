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
    /// Lớp hiển thị danh sách Order
    /// </summary>
    public sealed partial class OrderPage : Page
    { 
        public int[] ShowEntities = { 5, 10, 15, 20 }; // Số lượng Order hiển thị trên mỗi trang
        public ObservableCollection<Order> AllOrdersDisplay { get; set; } = new ObservableCollection<Order>(); // Danh order Order hiển thị
        public ObservableCollection<Order> DisplayedOrders { get; set; } = new ObservableCollection<Order>(); // Danh order Order hiển thị trên mỗi trang
        public int[] monthSearch = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }; // Danh sách tháng trong trường filter
        public string[] priceSearch = { "Greater than $100", "Smaller than $100" }; // Danh sách giá tiền trong trường filter
        private int ItemsPerPage = 10; // Số lượng Order hiển thị trên mỗi trang
        private int currentPage = 1; // Trang hiện tại
        private int totalPages; // Tổng số trang

        public OrderPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Hàm khởi tạo, gọi vỉewmodel và khởi tạo dữ liệu, định dạng trang
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

        /// <summary>
        /// Chuyển trang theo các tính năng được chọn
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string previousPage = e.Parameter as string;
            
            if (previousPage == nameof(OrderDetailPage))
            {
                //ViewModel.Init();
                //AllOrdersDisplay = ViewModel.AllOrders;
                AllOrdersDisplay = ViewModel.Orders;
                totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
                UpdateDisplayedOrders();
            }

        }

        /// <summary>
        /// Cập nhật lại các danh sách thông tin order theo yêu cầu định dạng trang được chọn
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
        /// Trang listview kế tiếp được chọn
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
        /// Quay lại trang listview trước đó
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
        /// Thêm order mới 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            //var newOrder = new Order("#001", "Peter Pan", "12:00AM - 12/10/2024", 30, 3, 12.2, 0);
            //AllOrdersDisplay.Add(newOrder);
            //totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
            //UpdateDisplayedOrders();

            //New add page
            Frame.Navigate(typeof(OrderDetailPage));
        }

        /// <summary>
        /// Xoá order ra khỏi trang
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
        /// Chỉnh sửa thông tin order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            //New edit page
            Frame.Navigate(typeof(OrderDetailPage), (sender as Button).DataContext);
        }

        /// <summary>
        /// Thực hiện lấy trường và yêu cầu sắp xếp tăng hay giảm dần
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
        /// Xử lý sắp xếp tăng hoặc giảm dần, xử lý trên nguyên tắc update lại danh sách order
        /// </summary>
        /// <param name="property"></param>
        /// <param name="order"></param>
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
        /// Chọn số lượng order hiển thị trên mỗi trang
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
        /// AutotextSearch, xử lý search theo tên khách hàng
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
        /// Hiện thị thông tin chi tiết order khi chọn vào danh sách hiển thị
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