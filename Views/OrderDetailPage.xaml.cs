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
using System.Threading;
using System.Timers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary> 
    /// Trang dùng để thêm và sửa thông tin đơn hàng. 
    /// Trang này cho phép người dùng tạo mới hoặc chỉnh sửa thông tin đơn hàng, bao gồm các mặt hàng, coupon, 
    /// tên khách hàng, ngày mua, và trạng thái giao hàng (hiện tại chỉ là giả lập).
    /// </summary>
    public sealed partial class OrderDetailPage : Page
    {
        public OrderDetailViewModel ViewModel { get; set; }
        public OrderPageViewModel OrderViewModel { get; set; }

        private System.Timers.Timer _typingTimer; 
        // Biến đếm thời gian trễ khi gõ phím
        private const int TypingDelay = 500;

        public OrderDetailPage()
        {
            this.InitializeComponent();

            // Lấy dữ liệu từ ViewModel
            ViewModel = (Application.Current as App).ServiceProvider.GetService<OrderDetailViewModel>();

            // Lấy dữ liệu từ OrderPageViewModel
            OrderViewModel = (Application.Current as App).ServiceProvider.GetService<OrderPageViewModel>();

            // Khởi tạo đối tượng _typingTimer với thời gian trễ là TypingDelay
            _typingTimer = new System.Timers.Timer(TypingDelay);
            // Gán sự kiện OnTypingTimerElapsed cho sự kiện Elapsed của _typingTimer
            _typingTimer.Elapsed += OnTypingTimerElapsed;
            // Thiết lập _typingTimer không tự động reset sau khi kích hoạt
            _typingTimer.AutoReset = false;
        }

        /// <summary>
        /// Xử lý sự kiên khi trang được chuyển đến (từ trang Order)
        /// Nếu có dữ liệu chuyển đến là 1 đơn hàng thì lấy thông tin đơn hàng đó là hiển thị lên trang
        /// Nếu không có dữ liệu chuyển đến thì hiển thị trang để tạo mới đơn hàng
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CouponsComboBox.ItemsSource = ViewModel.Coupons;

            // Đoạn này bí quá
            if (e.Parameter is Order order)
            {
                IsMemberCheckbox.IsEnabled = false;

                Order orderClone = (Order)order.Clone();

                // Nếu đang sửa thông tin đơn hàng
                // Bật nút cập nhật thông tin đơn hàng
                CreateOrderButton.Visibility = Visibility.Collapsed;
                UpdateOrderButton.Visibility = Visibility.Visible;

                // Đưa thông tin đơn hàng cần sửa vào ViewModel
                // Hiển thị thông tin đơn hàng cần sửa
                ViewModel.Order = orderClone;
                ViewModel.CustomerName = orderClone.Customer;
                ViewModel.PurchaseDate = orderClone.Date;
                ViewModel.IsDelivered = orderClone.IsDelivered;

                ViewModel.AddSelectedBooks(orderClone.OrderItems);
                ViewModel.AddSelectedCoupons(orderClone.Coupons);

                // Đưa danh sách các coupons đã chọn vào ComboBox
                CouponsComboBox.SelectedItems.Clear();
                var selectedCoupons = ViewModel.Coupons
                    .Where(coupon => ViewModel.SelectedCoupons.Any(selected => selected.Id == coupon.Id))
                    .ToList();
                selectedCoupons.ForEach(coupon => CouponsComboBox.SelectedItems.Add(coupon));

                // Đưa danh sách các sách đã chọn vào ComboBox
                BookSelectionComboBox.SelectedItems.Clear();
                var selectedBooks = ViewModel.Books
                    .Where(book => ViewModel.SelectedBooks.Any(selected => selected.Book.Index == book.Index))
                    .ToList();
                selectedBooks.ForEach(book => BookSelectionComboBox.SelectedItems.Add(book));

                // Kiểm tra xem khách hàng có phải là thành viên hay không
                PsqlDao dao = new PsqlDao();
                var customerInfo = await dao.GetCustomerOrderCountByOrderIdAsync(orderClone.ID);

                if (customerInfo != null)
                {
                    IsMemberCheckbox.IsChecked = true;

                    CustomerPhoneNumberTextBox.Text = customerInfo.Item1;
                    CustomerPhoneNumberTextBox.IsEnabled = false;

                    CustomerNameTextBox.IsEnabled = false;

                    CustomerTotalOrderTextBlock.Visibility = Visibility.Visible;
                    CustomerTotelOrderRun.Text = customerInfo.Item2.ToString();
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi thêm sách từ combobox vào danh sách sách đã chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bookComboBox_SelectionChanged(object sender, Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs e)
        {
            // Chuyển đến command trong ViewModel để xử lý việc thêm sách vào danh sách sách đã chọn
            if (ViewModel.BookSelectionChangedCommand.CanExecute(e))
            {
                ViewModel.BookSelectionChangedCommand.Execute(e);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút xóa sách khỏi danh sách sách đã chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSeletedButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.DataContext as OrderItem;

                // Xóa sách khỏi danh sách sách đã chọn
                ViewModel.SelectedBooks.Remove(item);

                // Xóa sách khỏi combobox
                BookSelectionComboBox.SelectedItems.Remove(item.Book);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi thêm coupon từ combobox vào danh sách coupon đã chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút tạo đơn hàng
        /// Validate thông tin đơn hàng
        /// Nếu thông tin không hợp lệ thì hiển thị thông báo lỗi
        /// Nếu thông tin hợp lệ thì tạo đơn hàng mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var converter = new DateTimeToStringConverter();

            // Validate thông tin đơn hàng
            ViewModel.ValidateAll();

            // Nếu thông tin không hợp lệ thì không tạo đơn hàng
            if (ViewModel.HasErrors)
            {
                return;
            }

            // Tạo đơn hàng mới
            Guid id = Guid.NewGuid();

            Order order = new Order()
            {
                ID = id.ToString(),
                Customer = ViewModel.CustomerName,
                Date = ViewModel.PurchaseDate,
                IsDelivered = ViewModel.IsDelivered,
                OrderItems = ViewModel.SelectedBooks.ToList(),
                Coupons = ViewModel.SelectedCoupons,
                Index = OrderViewModel.Orders.Count
            };

            try
            {
                bool success = await new PsqlDao().SaveOrderAsync(order);

                if (!success)
                {
                    return;
                }

                // Thêm đơn hàng mới vào danh sách đơn hàng
                this.OrderViewModel.Orders.Add(order);
                this.ViewModel.Order = order;

                // Payment giả lập
                PayBillOrderButtonGroup.Visibility = Visibility.Visible;
                CreateOrderButton.Visibility = Visibility.Collapsed;

                ViewModel.IsQrCodeVisible = true;
                ViewModel.IsBooksListViewVisible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Hiển thị dialog thông báo thanh toán thành công
        /// Nếu người dùng chọn "Yes", chuyển đến trang xuất hóa đơn
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
        /// Xử lý sự kiện khi chọn ngày mua
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            // Nếu ngày mua thay đổi thì gọi hàm xử lý sự kiện DateSelectedCommand
            if (args.NewDate.HasValue)
            {
                ViewModel.DateSelectedCommand.Execute(args.NewDate.Value.DateTime);
            }

        }

        /// <summary>
        /// Xử lý sự kiện khi chọn giờ mua
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            // Nếu giờ mua thay đổi thì gọi hàm xử lý sự kiện TimeSelectedCommand
            ViewModel.TimeSelectedCommand.Execute(e.NewTime);
        }

        /// <summary>
        ///  Xử lý sự kiện khi nhấn nút quay lại trang Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrderPage));
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút cập nhật thông tin đơn hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate thông tin đơn hàng
            ViewModel.ValidateAll();

            // Nếu thông tin không hợp lệ thì không cập nhật thông tin đơn hàng
            if (ViewModel.HasErrors)
            {
                return;
            }

            // Lấy thông tin đơn hàng cần cập nhật từ ViewModel và cập nhật thông tin đơn hàng
            ViewModel.Order.Customer = ViewModel.CustomerName;
            ViewModel.Order.Date = ViewModel.PurchaseDate;
            ViewModel.Order.IsDelivered = ViewModel.IsDelivered;

            ViewModel.Order.OrderItems = ViewModel.SelectedBooks.ToList();
            ViewModel.Order.Coupons = ViewModel.SelectedCoupons;

            try
            {
                bool success = await new PsqlDao().UpdateOrderAsync(ViewModel.Order);

                if (!success)
                {
                    return;
                }

                // Câp nhật thông tin đơn hàng vào danh sách đơn hàng
                OrderViewModel.Orders[ViewModel.Order.Index] = ViewModel.Order;

                // Chuyển về trang Order
                Frame.Navigate(typeof(OrderPage), this.GetType().Name);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
   
        }

        /// <summary>
        /// Xử lý sự kiện khi chọn phương thức thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentMethodCombobox_SelectionChanged(object sender, Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs e)
        {
            ViewModel.PaymentMethodQRCode = ViewModel.PaymentMethods[PaymentMethodCombobox.SelectedItem.ToString()];
        }

        /// <summary>
        /// Giả lập thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PayOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);

            ShowDialog("Payment", "Thanh toán thành công! Bạn có muốn xuất hóa đơn không?");
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút xuất hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Chuyển đến trang xuất hóa đơn
            Frame.Navigate(typeof(InvoicePage), ViewModel.Order);
        }

        /// <summary>
        /// Xử lý sự kiện khi giá trị của NumberBox Quantity thay đổi.
        /// Kiểm tra và giới hạn giá trị của Quantity để đảm bảo không vượt quá số lượng sách hiện có.
        /// Nếu giá trị Quantity lớn hơn số lượng sách hiện có, thì giá trị của Quantity sẽ được giới hạn lại bằng số lượng sách hiện có.
        /// Nếu giá trị Quantity nhỏ hơn hoặc bằng 0, thì giá trị của Quantity sẽ được giới hạn lại bằng 1.
        /// </summary>
        /// <param name="sender">Đối tượng gửi sự kiện</param>
        /// <param name="args">Thông tin về sự kiện</param>
        private void QuantityBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            // Kiểm tra xem sender.DataContext có phải là OrderItem hay không
            var orderItem = sender.DataContext as OrderItem;

            if (orderItem == null)
            {
                // Xử lý trường hợp DataContext không phải là OrderItem hoặc là null
                return; // Hoặc thực hiện xử lý phù hợp
            }

            var book = orderItem.Book;
            var quantity = (int)sender.Value;

            // Kiểm tra số lượng với số lượng trong book
            if (book != null && quantity > book.Quantity)
            {
                sender.Value = book.Quantity;
            }

            // Đảm bảo số lượng không dưới 1
            if (quantity <= 0)
            {
                sender.Value = 1;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi checkbox "IsMember" được chọn.
        /// Hiển thị nhóm điện thoại khách hàng nếu tồn tại.
        /// </summary>
        /// <param name="sender">Đối tượng gửi sự kiện</param>
        /// <param name="e">Đối tượng chứa thông tin về sự kiện</param>
        private void IsMemberCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (CustomerPhoneNumberGroup != null)
            {
                CustomerPhoneNumberGroup.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi checkbox "IsMember" không được chọn
        /// Ẩn nhóm hiển thị số điện thoại khách hàng
        /// Kích hoạt trường nhập tên khách hàng
        /// Ẩn hiển thị số đơn hàng của khách hàng
        /// </summary>
        /// <param name="sender">Đối tượng gửi sự kiện</param>
        /// <param name="e">Đối tượng chứa thông tin sự kiện</param>
        private void IsMemberCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            CustomerPhoneNumberGroup.Visibility = Visibility.Collapsed;
            CustomerNameTextBox.IsEnabled = true;
            CustomerTotalOrderTextBlock.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Xử lý sự kiện khi thay đổi nội dung của ô nhập số điện thoại khách hàng.
        /// Dừng và khởi động lại đếm thời gian gõ phím.
        /// </summary>
        /// <param name="sender">Đối tượng gửi sự kiện</param>
        /// <param name="e">Thông tin về sự kiện</param>
        private void CustomerPhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _typingTimer.Stop();
            _typingTimer.Start();
        }

        /// <summary>
        /// Xử lý sự kiện khi hết thời gian chờ sau khi gõ xong số điện thoại khách hàng.
        /// Kiểm tra thông tin khách hàng và cập nhật giao diện tương ứng.
        /// </summary>
        /// <param name="sender">Đối tượng gửi sự kiện</param>
        /// <param name="e">Thông tin về sự kiện</param>
        private void OnTypingTimerElapsed(object sender, ElapsedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(async () =>
            {
                PsqlDao dao = new PsqlDao();
                var customerInfo = await dao.GetCustomerOrderCountByPhoneAsync(CustomerPhoneNumberTextBox.Text.ToString());

                if (customerInfo != null)
                {
                    CustomerNameTextBox.Text = customerInfo.Item1;
                    CustomerNameTextBox.IsEnabled = false;

                    CustomerTotalOrderTextBlock.Visibility = Visibility.Visible;
                    CustomerTotelOrderRun.Text = customerInfo.Item2.ToString();

                    if (customerInfo.Item2 >= 10)
                    {
                        var coupon = ViewModel.Coupons.FirstOrDefault(c => c.Discount == 0.5);

                        // Thêm coupon vào danh sách coupon đã chọn
                        if (!ViewModel.SelectedCoupons.Contains(coupon))
                        {
                            ViewModel.SelectedCoupons.Add(coupon);
                            // Thêm coupon vào combobox
                            CouponsComboBox.SelectedItems.Add(coupon);

                            ViewModel.HasMemberDiscount = true;
                        }
                    }
                }
                else
                {
                    CustomerNameTextBox.IsEnabled = true;
                    CustomerNameTextBox.Text = "";

                    CustomerTotalOrderTextBlock.Visibility = Visibility.Collapsed;
                    CustomerTotelOrderRun.Text = "";

                    if (ViewModel.HasMemberDiscount == true)
                    {
                        var coupon = ViewModel.Coupons.FirstOrDefault(c => c.Discount == 0.5);

                        // Xóa coupon khỏi danh sách coupon đã chọn
                        if (ViewModel.SelectedCoupons.Contains(coupon))
                        {
                            ViewModel.SelectedCoupons.Remove(coupon);
                            // Xóa coupon khỏi combobox
                            CouponsComboBox.SelectedItems.Remove(coupon);
                        }

                        ViewModel.HasMemberDiscount = false;
                    }
                }
            });
        }
    }
}
