using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;
using Books_Store_Management_App.Models.Payment.Enums;
using Books_Store_Management_App.Services;
using Books_Store_Management_App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Dùng để hiển thị thông tin chi tiết của một đơn hàng
    /// </summary>
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

        /// <summary>
        /// Lấy dữ liệu từ trang Order Detail khi từ trang Order Detail chuyển qua
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Gán danh sách sách và mã giảm giá vào ComboBox
            CouponsComboBox.ItemsSource = ViewModel.Coupons;

            // Đoạn này bí quá làm dài dòng
            if (e.Parameter is Order order)
            {
                IsMemberCheckbox.IsEnabled = false;

                // Gán thông tin của đơn hàng vào ViewModel, để hiển thị lên giao diện
                ViewModel.Order = order;
                ViewModel.CustomerName = order.Customer;
                ViewModel.PurchaseDate = order.Date;
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

                // Kiểm tra xem khách hàng có phải là thành viên hay không
                PsqlDao dao = new PsqlDao();
                var customerInfo = await dao.GetCustomerOrderCountByOrderIdAsync(order.ID);

                if (customerInfo != null)
                {
                    IsMemberCheckbox.IsChecked = true;

                    CustomerPhoneNumberTextBox.Text = customerInfo.Item1;
                    CustomerPhoneNumberGroup.Visibility = Visibility.Visible;

                    CustomerTotalOrderTextBlock.Visibility = Visibility.Visible;
                    CustomerTotelOrderRun.Text = customerInfo.Item2.ToString();
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Back
        /// Chuyển về trang OrderPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrderPage));
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Bill Order
        /// Chuyển về trang InvoicePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Order.Date = ViewModel.PurchaseDate;
            ViewModel.Order.OrderItems = ViewModel.SelectedBooks.ToList();
            ViewModel.Order.Coupons = ViewModel.SelectedCoupons;

            Frame.Navigate(typeof(InvoicePage), ViewModel.Order);
        }

        /// <summary>
        /// Giả lập thanh toán.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PayOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var _paymentService = (Application.Current as App).ServiceProvider.GetService<PaymentService>();

            var method = PaymentMethod.Cash;
            
            if (Enum.TryParse<PaymentMethod>((string)PaymentMethodCombobox.SelectedItem, true, out var t))
            {
                method = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), (string)PaymentMethodCombobox.SelectedItem, true);
            }

            var result = await _paymentService.ProcessPayment(PaymentMethod.Demo, new Models.Payment.PaymentRequest()
            {
                Amount = Math.Ceiling(ViewModel.ActualTotal * 25462.5).ToString(),
                Description = "Thanh toán đơn hàng",
                orderId = ViewModel.Order.ID,
                MemberPaymentId = null,
                MemberPhoneNumber = CustomerPhoneNumberTextBox.Text,
            });

            var builder = new AppNotificationBuilder()
                .AddText($"Đơn hàng id: {ViewModel.Order.ID} của {ViewModel.CustomerName}")
                .AddText(result.Message)
                .AddArgument("Order", JsonConvert.SerializeObject(ViewModel.Order));

            var notificationManager = AppNotificationManager.Default;
            notificationManager.Show(builder.BuildNotification());

            if (result.Success)
            {
                ShowDialog("Payment", "Thanh toán thành công! Bạn có muốn xuất hóa đơn không?");

                var PsqlDao = new PsqlDao();
                await PsqlDao.UpdateOrderPaidStatusAsync(ViewModel.Order.ID, true);

                return;
            }

            if ((string)PaymentMethodCombobox.SelectedItem == "ZaloPay")
            {
                // Tạo mã QR Code
                var QRCODE = await ViewModel.CreateOrderAsync();
                ViewModel.PaymentMethods["ZaloPay"] = QRCODE;
                ViewModel.PaymentMethodQRCode = QRCODE;

                ViewModel.IsQrCodeVisible = true;
                ViewModel.IsBooksListViewVisible = false;

                bool isPaymentSuccess = await ViewModel.WaitForPaymentAsync(ViewModel.app_trans_id);

                //if (!isPaymentSuccess)
                //{
                //    var builder = new AppNotificationBuilder()
                //        .AddText($"Đơn hàng: {ViewModel.Order.ID} của {ViewModel.CustomerName}")
                //        .AddText("Thanh toán thất bại!")
                //        .AddText("Vui lòng thử lại sau.");

                //    var notificationManager = AppNotificationManager.Default;
                //    notificationManager.Show(builder.BuildNotification());

                //    return;
                //}

                ShowDialog("Payment", "Thanh toán thành công! Bạn có muốn xuất hóa đơn không?");
            }
            else
            {
                Thread.Sleep(2000);

                ShowDialog("Payment", "Thanh toán thành công! Bạn có muốn xuất hóa đơn không?");
            }
        }

        /// <summary>
        /// Hiển thị dialog thông báo thanh toán thành công.
        /// Nếu người dùng chọn "Yes", chuyển đến trang xuất hóa đơn.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
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

            // Nếu người dùng chọn "Yes", chuyển đến trang xuất hóa đơn
            if (result == ContentDialogResult.Primary)
            {
                BillOrderButton_Click(null, null);
            }
            else
            {
                // Nếu người dùng chọn "Cancel", chuyển về trang Order
                Frame.Navigate(typeof(OrderPage), this.GetType().Name);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi chọn phương thức thanh toán.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentMethodCombobox_SelectionChanged(object sender, Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs e)
        {
            ViewModel.PaymentMethodQRCode = ViewModel.PaymentMethods[PaymentMethodCombobox.SelectedItem.ToString()];
        }
    }

}
