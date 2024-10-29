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
using Windows.Storage;
using Windows.Storage.Pickers;
using Book_Store_Management;
using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml.Media.Imaging;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {

        public CustomerViewModel CustomerVM { get; set; }

        private Customer editingCustomer = null;
        public CustomerPage()
        {
            this.InitializeComponent();
            CustomerVM = new CustomerViewModel();
            CustomerVM.Init();
            UpdatePagingInfo_bootstrap();
            UpdateRowPerPageIfo_bootstrap();
        }

        void UpdatePagingInfo_bootstrap()
        {
            var infoList = new List<object>();
            for (int i = 1; i <= CustomerVM.TotalPages; i++)
            {
                infoList.Add(new
                {
                    Page = i,
                    Total = CustomerVM.TotalPages
                });
            }

            pagesComboBox.ItemsSource = infoList;
            pagesComboBox.SelectedIndex = 0;
        }

        void UpdateRowPerPageIfo_bootstrap()
        {

            var infoShow = new List<object>();
            for (int i = 1; i <= CustomerVM.TotalItems; i++)
            {
                infoShow.Add(new { item = i });
            }
            rowsPerPageComboBox.ItemsSource = infoShow;
            rowsPerPageComboBox.SelectedIndex = 9;
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var newCustomer = new Customer();
            newCustomer.ID = $"#{CustomerVM.TotalItems + 1}";
            // Lưu thông tin khách hàng vào CustomerVM.SelectedCustomer
            CustomerVM.SelectedCustomer = newCustomer;

            // Hiển thị hộp thoại xác nhận
            ContentDialogResult result = await AddCustomerDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                CustomerVM.InsertCustomer(CustomerVM.SelectedCustomer);
                CustomerVM.GetAllCustomers();
            }
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ID của khách hàng từ nút được nhấn
            var button = sender as Button;
            var customerId = button?.Tag?.ToString();

            if (customerId != null)
            {
                // Tìm khách hàng dựa trên ID
                var newCustomer = CustomerVM.Customers.FirstOrDefault(c => c.ID == customerId);

                if (newCustomer != null)
                {
                    // Lưu thông tin khách hàng vào CustomerVM.SelectedCustomer
                    CustomerVM.SelectedCustomer = newCustomer;
                    // Hiển thị hộp thoại xác nhận
                    ContentDialogResult result = await EditCustomerDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        CustomerVM.EditCustomer(CustomerVM.SelectedCustomer);
                        CustomerVM.GetAllCustomers();
                    }
                }
            }
        }
        private async void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            // Mở FileOpenPicker để chọn ảnh
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            // Gán picker với cửa sổ hiện tại
            var hwnd = WindowHelper.GetWindowHandle(App.MainWindow);  
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                // Tạo BitmapImage và cập nhật Avatar
                BitmapImage bitmapImage = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await bitmapImage.SetSourceAsync(stream);
                }

                CustomerVM.SelectedCustomer.Avatar = bitmapImage;
            }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ID của khách hàng từ nút được nhấn
            var button = sender as Button;
            var customerId = button?.Tag?.ToString();

            if (customerId != null)
            {
                // Tìm khách hàng dựa trên ID
                var delcustomer = CustomerVM.Customers.FirstOrDefault(c => c.ID == customerId);

                if (delcustomer != null)
                {
                    // Lưu thông tin khách hàng vào CustomerVM.SelectedCustomer
                    CustomerVM.SelectedCustomer = delcustomer;

                    // Hiển thị hộp thoại xác nhận
                    ContentDialogResult result = await DeleteCustomerDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        // Xóa khách hàng nếu người dùng chọn "Xóa"
                        CustomerVM.DeleteCustomer(customerId);
                        CustomerVM.GetAllCustomers();
                    }
                }
            }
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedItem;

            if (item != null)
            {
                CustomerVM.LoadingPage(item.Page);
            }
        }

        private void sortbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerVM.TypeOfSort = sortbyComboBox.SelectedIndex + 1;
            CustomerVM.LoadingPage(1);
        }
        private void filterbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerVM.TypeOfSearch = filterSearchComboBox.SelectedIndex + 1;
            CustomerVM.LoadingPage(1);
        }
        private void rowsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerVM.RowsPerPage = rowsPerPageComboBox.SelectedIndex + 1;
            CustomerVM.LoadingPage(1);
            UpdatePagingInfo_bootstrap();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedIndex;
            if (item < pagesComboBox.Items.Count - 1)
            {
                pagesComboBox.SelectedIndex += 1;
            }
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedIndex;
            if (item > 0)
            {
                pagesComboBox.SelectedIndex -= 1;
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerVM.Keyword = keywordTextBox.Text;
            CustomerVM.LoadingPage(1);
            UpdatePagingInfo_bootstrap();
        }
    }
}
