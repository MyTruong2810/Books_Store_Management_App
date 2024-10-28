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

namespace Books_Store_Management_App.Views
{
    public sealed partial class OrderPage : Page
    { 
        public int[] ShowEntities = { 5, 10, 15, 20 };
        public ObservableCollection<Order> AllOrdersDisplay { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Order> DisplayedOrders { get; set; } = new ObservableCollection<Order>();
        public int[] monthSearch = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public string[] priceSearch = { "Greater than $100", "Smaller than $100" };
        private int ItemsPerPage = 10;
        private int currentPage = 1;
        private int totalPages;
        public class OrderPageViewModel
        {
            public ObservableCollection<Order> AllOrders { get; set; }
            public void Init()
            {
                IDao dao = new PsqlDao();
                AllOrders = dao.GetAllOrders();
            }
        }

        public OrderPageViewModel ViewModel { get; set; }

        public OrderPage()
        {
            this.InitializeComponent();
            ViewModel = new OrderPageViewModel();
            ViewModel.Init();
            AllOrdersDisplay = ViewModel.AllOrders;
            totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
            UpdateDisplayedOrders();
        }
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
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdateDisplayedOrders();
            }
        }
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDisplayedOrders();
            }
        }
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            //var newOrder = new Order("#001", "Peter Pan", "12:00AM - 12/10/2024", 30, 3, 12.2, 0);
            //AllOrdersDisplay.Add(newOrder);
            //totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
            //UpdateDisplayedOrders();

            //New add page
            Frame.Navigate(typeof(OrderDetailPage));
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var Order = (sender as Button).DataContext as Order;
            AllOrdersDisplay.Remove(Order);
            totalPages = (int)Math.Ceiling((double)AllOrdersDisplay.Count / ItemsPerPage);
            if (currentPage > totalPages) currentPage = totalPages; // Adjust page if last page is removed

            UpdateDisplayedOrders();
        }
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            //New edit page
            Frame.Navigate(typeof(OrderDetailPage), (sender as Button).DataContext);
        }
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
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredOrders = ViewModel.AllOrders.Where(order =>
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
    }
}