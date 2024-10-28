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
    public sealed partial class StockPage : Page
    {
        public int[] ShowEntities = { 5, 10, 15, 20 };

        public string[] generSearch = { "Drama", "Novel", "Science" };
        public string[] priceSearch = { "Greater than $100", "Smaller than $100" };
        public ObservableCollection<Book> AllBooksDisplay { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> DisplayedBooks { get; set; } = new ObservableCollection<Book>();
        private int ItemsPerPage = 10;
        private int currentPage = 1;
        private int totalPages;
        public class StockPageViewModel
        {
            public ObservableCollection<Book> AllBooks { get; set; }
            public void Init()
            {
                IDao dao = new PsqlDao();
                AllBooks = dao.GetAllBooks();
            }
        }
        public StockPageViewModel ViewModel { get; set; }

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
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdateDisplayedBooks();
            }
        }
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDisplayedBooks();
            }
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            //var newBook = new Book("ms-appx:///Assets/image1.jpg", "New Book", "New Publisher", "New Author", "1234567890123", 2024, 9.99, "New Genre", 100, 0);
            //AllBooksDisplay.Add(newBook);
            //totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
            //UpdateDisplayedBooks();

            // Open the popup to add a new book
            BookPopupControl.ViewModel.ClearData();
            BookPopupControl.ClearErrorMessage();
            BookPopupControl.SetPopupTitle("Add New Book");
            BookPopupControl.SetButton("Add");
            BookPopupControl.SetEditable(true);
            StandardPopup.IsOpen = true;
        }
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            //var book = (sender as Button).DataContext as Book;
            //AllBooksDisplay.Remove(book);
            //totalPages = (int)Math.Ceiling((double)AllBooksDisplay.Count / ItemsPerPage);
            //if (currentPage > totalPages) currentPage = totalPages; // Adjust page if last page is removed

            //UpdateDisplayedBooks();

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
        private void EditBook_Click(object sender, RoutedEventArgs e)
        {

            /* ==========================================================
             * You: Implement code to change the edit Page / edit Frame ||
             * ==========================================================
             */

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