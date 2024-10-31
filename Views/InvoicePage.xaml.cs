using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QuestPDF.Infrastructure;
using QuestPDF;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Books_Store_Management_App.ViewModels;
using Books_Store_Management_App.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Trang này được sử dụng để hiển thị chi tiết hóa đơn và cho phép người dùng in hóa đơn. 
    /// Nó lấy dữ liệu từ trang Chi tiết Đơn hàng và sử dụng QuestPDF để tạo file PDF hóa đơn.
    /// </summary>
    public sealed partial class InvoicePage : Page
    {
        public InvoiceViewModel ViewModel { get; set; }
        public InvoicePage()
        {
            this.InitializeComponent();

            ViewModel = new InvoiceViewModel();
        }

        // Lấy dữ liệu từ trang Order Detail khi từ trang Order Detail chuyển qua
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Order order)
            {
                // Gán dữ liệu từ trang Order Detail vào ViewModel
                ViewModel.Order = order;
            }
        }

        // Xử lý sự kiện khi nhấn nút Print Invoice
        private void PrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            // Gán license cho QuestPDF
            Settings.License = LicenseType.Community;

            // For documentation and implementation details, please visit:
            // https://www.questpdf.com/getting-started.html
            //var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            ViewModel.CustomerAddress = new Address();
            ViewModel.SellerAddress = new Address();

            //ViewModel.CustomerAddress.CompanyName = "Customer Company";
            // Dữ liệu giả lập cho địa chỉ của khách hàng và người bán
            // Để test chức năng in hóa đơn
            ViewModel.CustomerAddress.Street = "Customer Street";
            ViewModel.CustomerAddress.City = "Customer City";
            ViewModel.CustomerAddress.State = "Customer State";
            ViewModel.CustomerAddress.Email = "Customer Email";
            ViewModel.CustomerAddress.Phone = "Customer Phone";

            ViewModel.SellerAddress.CompanyName = "Seller Company";
            ViewModel.SellerAddress.Street = "Seller Street";
            ViewModel.SellerAddress.City = "Seller City";
            ViewModel.SellerAddress.State = "Seller State";
            ViewModel.SellerAddress.Email = "Seller Email";
            ViewModel.SellerAddress.Phone = "Seller Phone";
            //

            var document = new InvoiceDocument(ViewModel);

            // Generate PDF file and show it in the default viewer
            document.GeneratePdfAndShow();
        }

        // Xử lý sự kiện khi nhấn nút Back để quay lại trang Order Detail
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }

}
