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
using Books_Store_Management_App.ViewModels;
using System.Drawing;
using Books_Store_Management_App.Models;

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Lớp StockPage hiển thị danh sách sách trong kho.
    /// </summary>
    public sealed partial class StockPage : Page
    {
        public int[] ShowEntities = { 5, 10, 15, 20 }; // Số lượng sách hiển thị trên mỗi trang
        public ObservableCollection<Genre> genres = new ObservableCollection<Genre>(); // Danh sách thể loại sách
        public string[] priceSearch = {"Greater than $10.00", "Smaller than $10.00" };
        public ObservableCollection<Book> AllBooksDisplay { get; set; } = new ObservableCollection<Book>(); // Danh sách sách hiển thị
        public ObservableCollection<Book> DisplayedBooks { get; set; } = new ObservableCollection<Book>();  // Danh sách sách được hiển thị trên mỗi trang
        private int ItemsPerPage = 10; // Số lượng sách hiển thị trên mỗi trang
        private int currentPage = 1; // Trang hiện tại
        private int totalPages; // Tổng số trang
        public StockPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Khởi tạo trang StockPage, gọi ViewModel và danh sách sách.
        /// </summary>
        public StockPage()
        {
            this.InitializeComponent();
            ViewModel = new StockPageViewModel();
            ViewModel.Init();
            AllBooksDisplay = ViewModel.AllBooks;
            genres = ViewModel.AllGenres;
            totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
            UpdateDisplayedBooks();

            // Khởi tạo thông số và vị trí hiển của popup
            StandardPopup.HorizontalOffset = this.ActualWidth - 730;
            BookPopupControl.RightDialogHeight = this.ActualHeight;
            BookPopupControl.SaveButtonClicked += BookPopupControl_SaveButtonClicked;
        }

        /// <summary>
        /// Lấy dữ liệu từ trang Order Detail khi từ trang Order Detail chuyển qua.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BookPopupControl_SaveButtonClicked(object sender, Book e)
        {
            if (BookPopupControl.GetButton() == "Add")
            {
                // id is max id + 1
                var id = AllBooksDisplay.Max(x => x.Index) + 1;
                e.Index = id;

                try
                {
                    bool success = await new PsqlDao().SaveBookAsync(e);

                    if (!success)
                    {
                        return;
                    }

                    AllBooksDisplay.Add(e);
                    totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
                    UpdateDisplayedBooks();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (BookPopupControl.GetButton() == "Edit")
            {
                try
                {
                    bool success = await new PsqlDao().UpdateBookAsync(e);

                    if (!success)
                    {
                        return;
                    }


                    var index = AllBooksDisplay.ToList().FindIndex(x => x.Index == e.Index);
                    if (index != -1)
                    {
                        AllBooksDisplay[index] = e;
                    }
                    UpdateDisplayedBooks();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Dùng để thay đổi lại vị trí và chiều cao của popup khi thay đổi kích thước cửa sổ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StandardPopup.HorizontalOffset = this.ActualWidth - 730;
            BookPopupControl.RightDialogHeight = this.ActualHeight;
        }
        private void UpdateDisplayedBooks()
        {
            var skip = (currentPage - 1) * ItemsPerPage;
            DisplayedBooks.Clear();
            int cnt = 0;
            foreach (var book in AllBooksDisplay)
            {
                book.CurrentRow= cnt;
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
        /// Hiện thị trang tiếp theo của listview.
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
        /// Hiện thị trang trước của listview.
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
        /// Thêm sách mới vào trong kho hoặc danh sách.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo popup để thêm sách mới
            BookPopupControl.ViewModel.ClearData();
            BookPopupControl.ClearErrorMessage();
            BookPopupControl.SetPopupTitle("Add New Book");
            BookPopupControl.SetButton("Add");
            BookPopupControl.SetEditable(true);
            StandardPopup.IsOpen = true;
        }

        /// <summary>
        /// Xóa sách khỏi kho hoặc danh sách.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            // Chỉ giả lập xóa sách, chưa đụng vào database
            var button = sender as Button;
            var book = button?.Tag as Book;

            if (book != null)
            {
                // Set the ISBN in the dialog dynamically
                TextBlock isbnTextBlock = new TextBlock
                {
                    Text = book.ISBN
                };

                // Create StackPanel and add ISBN TextBlock and confirmation TextBlock
                StackPanel stackPanel = new StackPanel
                {
                    Width = 500,
                    Margin = new Thickness(0)
                };

                stackPanel.Children.Add(isbnTextBlock);
                stackPanel.Children.Add(new TextBlock { Text = "Do you want to delete this book?" });

                // Set the content of the dialog
                DeleteBookDialog.Content = stackPanel;

                // Show the dialog
                ContentDialogResult result = await DeleteBookDialog.ShowAsync();

                if (result == ContentDialogResult.Primary) // If confirmed
                {
                    // Call the delete method on the ViewModel
                    bool success = await new PsqlDao().DeleteBookAsync(book.Index);
                    AllBooksDisplay.Remove(book);

                    // Adjust the paging after deletion
                    totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
                    if (currentPage > totalPages) currentPage = totalPages; // Adjust page if last page is removed
                    UpdateDisplayedBooks();
                }
            }
        }


        /// <summary>
        /// Chỉnh sửa thông tin sách.
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
        /// Lấy thông tin trường yêu cầu sắp xếp và tyêu cầu sắp xếp theo tăng hay giảm dần.
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
        /// Thực hiện sắp xếp sách theo trường yêu cầu.
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
                    book.CurrentRow = cnt;
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
                    book.CurrentRow = cnt;
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
        /// Chọn số lượng sách hiển thị trên mỗi trang.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Combo3_Selected(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            string submittedText = comboBox?.SelectedItem?.ToString() ?? string.Empty;

            if (int.TryParse(submittedText, out int itemsPerPage) && itemsPerPage > 0)
            {
                ItemsPerPage = itemsPerPage;
                totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
                currentPage = 1;
                UpdateDisplayedBooks();
            }
        }

        /// <summary>
        /// AutoTextSearch cho ô tìm kiếm sách.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyFilters()
        {
            var searchText = SearchTextBox?.Text.Trim() ?? string.Empty;
            var selectedGenre = SearchByGenres?.SelectedItem?.ToString() ?? string.Empty;
            var selectedPriceRange = SearchByPrices?.SelectedItem?.ToString() ?? string.Empty;

            // First filter books by title (searchText must be found in the title)
            var filteredBooks = ViewModel.AllBooks.Where(book =>
                string.IsNullOrWhiteSpace(searchText) ||
                book.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);  // Fix: check if searchText is in book.Title

            // Then apply genre and price filters (if any)
            filteredBooks = filteredBooks.Where(book =>
                (string.IsNullOrWhiteSpace(selectedGenre) ||
                 book.Genre.Equals(selectedGenre, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(selectedPriceRange) ||
                 IsPriceInRange(book.Price, selectedPriceRange))
            );

            // Update displayed books
            DisplayedBooks.Clear();
            foreach (var book in filteredBooks)
            {
                DisplayedBooks.Add(book);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void SearchByGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void SearchByPrices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private bool IsPriceInRange(double price, string priceRange)
        {
            // Example implementation for price range filtering
            if (priceRange.Equals("Greater than $10.00", StringComparison.OrdinalIgnoreCase))
                return price > 10;
            if (priceRange.Equals("Smaller than $10.00", StringComparison.OrdinalIgnoreCase))
                return price <= 10;

            return true; // Default to including all prices if no range is matched
        }
    }
    /// <summary>
    /// Lớp giúp hiển thị màu sách bảng xen kẽ màu, ứng dụng tính chẵn lẽ của index.
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