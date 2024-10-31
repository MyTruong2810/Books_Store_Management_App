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
using Books_Store_Management_App.ViewModels;

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Lớp hiện thị trang quản lý sách
    /// </summary>
    public sealed partial class StockPage : Page
    {
        public int[] ShowEntities = { 5, 10, 15, 20 }; // Số lượng sách hiển thị trên một trang

        public string[] generSearch = { "Drama", "Novel", "Science" }; // Filter theo thể loại sách
        public string[] priceSearch = { "Greater than $100", "Smaller than $100" }; //  Filter theo giá sách
        public ObservableCollection<Book> AllBooksDisplay { get; set; } = new ObservableCollection<Book>(); // Danh sách sách hiển thị
        public ObservableCollection<Book> DisplayedBooks { get; set; } = new ObservableCollection<Book>(); // Danh sách sách hiển thị trên một trang
        private int ItemsPerPage = 10; // Số lượng sách hiển thị trên một trang
        private int currentPage = 1; // Trang hiện tại
        private int totalPages; // Tổng số trang

        /// <summary>
        /// Lớp ViewModel của trang quản lý sách
        /// </summary>
        public StockPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Hàm khởi tạo trang quản lý sách
        /// </summary>
        public StockPage()
        {
            this.InitializeComponent();
            ViewModel = new StockPageViewModel();
            ViewModel.Init();
            AllBooksDisplay = ViewModel.AllBooks;
            totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
            UpdateDisplayedBooks();

            // Init BookPopupControl
            StandardPopup.HorizontalOffset = this.ActualWidth - 730;
            BookPopupControl.RightDialogHeight = this.ActualHeight;
            BookPopupControl.SaveButtonClicked += BookPopupControl_SaveButtonClicked;
        }
        private void BookPopupControl_SaveButtonClicked(object sender, Book e)
        {
            if (BookPopupControl.GetButton() == "Add")
            {
                // Todo in view model
                //e.Index = ViewModel.Books.Count + 1;
                //ViewModel.Books.Add(e);

                // Update the displayed books
                e.Index = AllBooksDisplay.Count + 1;
                AllBooksDisplay.Add(e);
                totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
                UpdateDisplayedBooks();
            }
            else if (BookPopupControl.GetButton() == "Edit")
            {
                //var index = ViewModel.Books.ToList().FindIndex(x => x.Id == e.Id);
                //if (index != -1)
                //{
                //    ViewModel.Books[index] = e;
                //}

                // Update the displayed books
                var index = AllBooksDisplay.ToList().FindIndex(x => x.Index == e.Index);
                if (index != -1)
                {
                    AllBooksDisplay[index] = e;
                }
                UpdateDisplayedBooks();
            }
        }

        // Dùng để thay đổi lại vị trí và chiều cao của popup khi thay đổi kích thước cửa sổ
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StandardPopup.HorizontalOffset = this.ActualWidth - 730;
            BookPopupControl.RightDialogHeight = this.ActualHeight;
        }
        /// <summary>
        /// Hàm update sách hiện thị trên một trang và thực hiện paging
        /// </summary>
        private void UpdateDisplayedBooks()
        {
            var skip = (currentPage - 1) * ItemsPerPage;
            DisplayedBooks.Clear();
            int cnt = 0;
            foreach (var book in AllBooksDisplay)
            {
                book.Index = cnt;
                cnt++;
            }

            foreach (var book in AllBooksDisplay.Skip(skip).Take(ItemsPerPage))
            {
                DisplayedBooks.Add(book);
            }
            PageInfo.Text = $"Page {currentPage} of {totalPages}";
            PreviousButton.IsEnabled = currentPage > 1;
            NextButton.IsEnabled = currentPage < totalPages;
        }
        /// <summary>
        /// Hiện thị trang kế tiếpc của lisview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdateDisplayedBooks();
            }
        }
        /// <summary>
        /// Hiện thị lại trang trước đó của listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDisplayedBooks();
            }
        }

        /// <summary>
        /// Thêm sách mới khi nhấn button add new book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            // Open the popup to add a new book
            BookPopupControl.ViewModel.ClearData();
            BookPopupControl.ClearErrorMessage();
            BookPopupControl.SetPopupTitle("Add New Book");
            BookPopupControl.SetButton("Add");
            BookPopupControl.SetEditable(true);
            StandardPopup.IsOpen = true;
        }

        /// <summary>
        /// Xoá sách khi nhấn button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            //UpdateDisplayedBooks();

            // Chỉ giả lập xóa sách, chưa đụng vào database
            var button = sender as Button;
            var book = button?.Tag as Book;

            if (book != null)
            {
                AllBooksDisplay.Remove(book);
            }
            totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
            if (currentPage > totalPages) currentPage = totalPages; // Adjust page if last page is removed
            UpdateDisplayedBooks();
        }
        /// <summary>
        /// Chỉnh sửa thông tin danh sách khi chọn nút chỉnh sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            StandardPopup.IsOpen = true;

            var button = sender as Button;
            var book = button?.Tag as Book;

            if (book != null)
            {
                BookPopupControl.setData(book);
                BookPopupControl.ClearErrorMessage();
                BookPopupControl.SetPopupTitle("Edit Book");
                BookPopupControl.SetButton("Edit");
                BookPopupControl.SetEditable(true);
            }
        }

        /// <summary>
        /// Hàm truyền về tên và yêu cầu sắp xếp theo tăng hay giảm
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

                SortBooks(property, order);
            }
        }

        /// <summary>
        /// Sắp xếp sách theo thứ tự tăng hay giảm thông qua order và sắp xếp theo trường nào thông qua property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="order"></param>
        private void SortBooks(string property, string order)
        {
            if (order == "ASC")
            {
                var sortedBooks = AllBooksDisplay.OrderBy(b => b.GetType().GetProperty(property).GetValue(b)).ToList();
                AllBooksDisplay.Clear();
                int cnt = 0;
                foreach (var book in sortedBooks)
                {
                    book.Index = cnt;
                    AllBooksDisplay.Add(book);
                    cnt++;
                }
                UpdateDisplayedBooks();

            }
            else if (order == "DES")
            {
                var sortedBooks = AllBooksDisplay.OrderByDescending(b => b.GetType().GetProperty(property).GetValue(b)).ToList();
                AllBooksDisplay.Clear();
                int cnt = 0;
                foreach (var book in sortedBooks)
                {
                    book.Index = cnt;
                    AllBooksDisplay.Add(book);
                    cnt++;
                }
                UpdateDisplayedBooks();
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Lây ra số lượng sách hiển thị trên một trang
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
        /// AutotextSearch cho việc tìm kiếm theo tên sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredOrders = ViewModel.AllBooks.Where(order =>
                    order.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                DisplayedBooks.Clear();
                foreach (var order in filteredOrders)
                {
                    DisplayedBooks.Add(order);
                }
            }
            else
            {
                UpdateDisplayedBooks();
            }
        }
    }
    /// <summary>
    /// Lớp giúp thực hiện việc hiển thị bẳng theo 2 màu khác nhau xen kẽ
    /// </summary>
    public class AlternationIndexToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isEven)
            {
                // Replace the problematic line with the following
                Windows.UI.Color hexColor1 = ColorHelper.FromArgb(0xFF, 0x16, 0x70, 0xBA);
                Windows.UI.Color hexColor2 = ColorHelper.FromArgb(0xFF, 0x5B, 0x92, 0xD4);
                return !isEven ? new SolidColorBrush(hexColor1) : new SolidColorBrush(hexColor2);
            }
            return new SolidColorBrush(Colors.Transparent);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}