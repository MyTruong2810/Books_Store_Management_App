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
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderDetailPage : Page
    {
        public OrderDetailViewModel ViewModel { get; set; }
        public OrderPageViewModel OrderViewModel { get; set; }
        public OrderDetailPage()
        {
            this.InitializeComponent();

            ViewModel = (Application.Current as App).ServiceProvider.GetService<OrderDetailViewModel>();
            OrderViewModel = (Application.Current as App).ServiceProvider.GetService<OrderPageViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CouponsComboBox.ItemsSource = ViewModel.Coupons;

            // Đoạn này bí quá
            if (e.Parameter is Order order)
            {

                CreateOrderButton.Visibility = Visibility.Collapsed;
                UpdateOrderButton.Visibility = Visibility.Visible;

                ViewModel.Order = order;
                ViewModel.CustomerName = order.Customer;
                ViewModel.PurchaseDate = DateTime.Parse(order.Date);
                ViewModel.IsDelivered = order.IsDelivered;

                ViewModel.AddSelectedBooks(order.OrderItems);
                ViewModel.AddSelectedCoupons(order.Coupons);

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

        private void bookComboBox_SelectionChanged(object sender, Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs e)
        {
            if (ViewModel.BookSelectionChangedCommand.CanExecute(e))
            {
                ViewModel.BookSelectionChangedCommand.Execute(e);
            }
        }

        private void DeleteSeletedButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.DataContext as OrderItem;
                ViewModel.SelectedBooks.Remove(item);

                BookSelectionComboBox.SelectedItems.Remove(item.Book);
            }
        }

        private void couponComboBox_SelectionChanged(object sender, Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs e)
        {
            // Thêm các coupon mới vào SelectedCoupons
            foreach (var addedItem in e.AddedItems)
            {

                if (!ViewModel.SelectedCoupons.Contains(addedItem as Coupon))
                {
                    ViewModel.SelectedCoupons.Add(addedItem as Coupon);
                }
            }

            // Xóa các coupon đã bỏ chọn khỏi SelectedCoupons
            foreach (var removedItem in e.RemovedItems)
            {
                Coupon couponToRemove = ViewModel.SelectedCoupons.FirstOrDefault(c => c.Id == (removedItem as Coupon).Id);
                if (couponToRemove != null)
                {
                    ViewModel.SelectedCoupons.Remove(couponToRemove);
                }
            }

        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var converter = new DateTimeToStringConverter();

            ViewModel.ValidateAll();

            if (ViewModel.HasErrors)
            {
                return;
            }

            Order order = new Order()
            {
                ID = ViewModel.SelectedBooks.Count + 1,
                Customer = ViewModel.CustomerName,
                Date = ViewModel.PurchaseDate.ToString(),
                IsDelivered = ViewModel.IsDelivered,
                OrderItems = ViewModel.SelectedBooks.ToList(),
                Coupons = ViewModel.SelectedCoupons,
                Index = OrderViewModel.Orders.Count
            };

            this.OrderViewModel.Orders.Add(order);

            // Payment giả lập
            PayBillOrderButtonGroup.Visibility = Visibility.Visible;
            CreateOrderButton.Visibility = Visibility.Collapsed;

            ViewModel.IsQrCodeVisible = true;
            ViewModel.IsBooksListViewVisible = false;
        }

        private async void ShowDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = title;
            dialog.PrimaryButtonText = "Yes";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new TextBlock { Text = message };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                BillOrderButton_Click(null, null);
            }
            else
            {
                Frame.Navigate(typeof(OrderPage), this.GetType().Name);
            }
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.HasValue)
            {
                ViewModel.DateSelectedCommand.Execute(args.NewDate.Value.DateTime);
            }

        }

        private void TimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            ViewModel.TimeSelectedCommand.Execute(e.NewTime);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrderPage));
        }

        //private void PayOrderButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void BillOrderButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ViewModel.Order.Date = ViewModel.PurchaseDate.ToString();
        //    ViewModel.Order.OrderItems = ViewModel.SelectedBooks.ToList();
        //    ViewModel.Order.Coupons = ViewModel.SelectedCoupons;

        //    //Frame.Navigate(typeof(InvoicePage), ViewModel.Order);
        //}

        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ValidateAll();

            if (ViewModel.HasErrors)
            {
                return;
            }

            ViewModel.Order.Customer = ViewModel.CustomerName;
            ViewModel.Order.Date = ViewModel.PurchaseDate.ToString();
            ViewModel.Order.IsDelivered = ViewModel.IsDelivered;
            ViewModel.Order.OrderItems = ViewModel.SelectedBooks.ToList();
            ViewModel.Order.Coupons = ViewModel.SelectedCoupons;

            OrderViewModel.Orders[ViewModel.Order.Index] = ViewModel.Order;

            Frame.Navigate(typeof(OrderPage), this.GetType().Name);
        }

        private void PaymentMethodCombobox_SelectionChanged(object sender, Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs e)
        {
            ViewModel.PaymentMethodQRCode = ViewModel.PaymentMethods[PaymentMethodCombobox.SelectedItem.ToString()];
        }

        private void PayOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);

            ShowDialog("Payment", "Thanh toán thành công! Bạn có muốn xuất hóa đơn không?");
        }

        private void BillOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Order = new Order();
            ViewModel.Order.ID = OrderViewModel.Orders.Count;
            ViewModel.Order.Customer = ViewModel.CustomerName;
            ViewModel.Order.Date = ViewModel.PurchaseDate.ToString();
            ViewModel.Order.OrderItems = ViewModel.SelectedBooks.ToList();
            ViewModel.Order.IsDelivered = ViewModel.IsDelivered;
            ViewModel.Order.Coupons = ViewModel.SelectedCoupons;

            Frame.Navigate(typeof(InvoicePage), ViewModel.Order);
        }
    }

}
