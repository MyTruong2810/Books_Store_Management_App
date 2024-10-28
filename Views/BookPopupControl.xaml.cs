using Books_Store_Management_App.Models;
using Books_Store_Management_App.ViewModels;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App
{
    public sealed partial class BookPopupControl : UserControl
    {
        private List<Genre> genreList;

        public BookPopupControlViewModel ViewModel { get; set; }
        public BookPopupControl()
        {
            this.InitializeComponent();

            IDao dao = new PsqlDao();
            genreList = dao.GetAllGenres().ToList();
            classifyComboBox.ItemsSource = genreList;

            ViewModel = new BookPopupControlViewModel();
        }

        public event EventHandler<Book> SaveButtonClicked;

        //public Book ViewModel
        //{
        //    get { return (Book)GetValue(ViewModelProperty); }
        //    set { SetValue(ViewModelProperty, value); }
        //}

        //public static readonly DependencyProperty ViewModelProperty =
        //    DependencyProperty.Register("ViewModel", typeof(Book), typeof(BookPopupControl), new PropertyMetadata(null));
        public void SetPopupTitle(string title)
        {
            TitleTextBlock.Text = title;
        }

        public double RightDialogHeight
        {
            get => rightDialog.Height;
            set => rightDialog.Height = value;
        }
        public void SetEditable(bool isEditable)
        {
            //Console.WriteLine(ViewModel);

            bookNameTextBox.IsEnabled = isEditable;
            authorTextBox.IsEnabled = isEditable;
            yearTextBox.IsEnabled = isEditable;
            classifyComboBox.IsEnabled = isEditable;
            sellingPricesTextBox.IsEnabled = isEditable;
            purchasePriceTextBox.IsEnabled = isEditable;
            quantityTextBox.IsEnabled = isEditable;
            desciptionTextBox.IsEnabled = isEditable;
            // Các textbox khác cũng có thể được thiết lập tương tự
        }

        public void SetButton(string text)
        {
            SaveBtn.Content = text;
        }
        public string GetButton()
        {
            return SaveBtn.Content.ToString();
        }

        public void ClearFields()
        {
            // Đặt giá trị các trường về rỗng
            bookNameTextBox.Text = string.Empty;
            authorTextBox.Text = string.Empty;
            yearTextBox.Text = string.Empty;
            sellingPricesTextBox.Text = string.Empty;
            purchasePriceTextBox.Text = string.Empty;
            quantityTextBox.Text = string.Empty;
            desciptionTextBox.Text = string.Empty;
            // Reset các trường khác nếu cần
        }
        private void xBtn_Click(object sender, RoutedEventArgs e)
        {
            // Logic để đóng popup
            var parent = this.Parent as Popup;
            if (parent.IsOpen) { parent.IsOpen = false; }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic để hủy
            var parent = this.Parent as Popup;
            if (parent.IsOpen) { parent.IsOpen = false; }
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            xBtn.Background.Opacity = 0.5;
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            xBtn.Background.Opacity = 1;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ValidateAll();

            if (ViewModel.HasErrors)
            {
                return;
            }

            ViewModel.Genre = classifyComboBox.SelectedItem.ToString();

            ViewModel.MergeToBook();

            var parent = this.Parent as Popup;
            if (parent.IsOpen) { parent.IsOpen = false; }
            //SaveButtonClicked.Invoke(this, ViewModel);
            SaveButtonClicked.Invoke(this, ViewModel.Book);
        }
        private void ChangeIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}Assets"; // Thay đổi đường dẫn này thành thư mục bạn muốn
            openFileDialog.Filter = "All files (*.*)|*.*"; // Bạn có thể thay đổi bộ lọc file nếu cần
            openFileDialog.Title = "Chọn một file";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                Console.WriteLine("File đã chọn: " + selectedFilePath);

                ViewModel.ImageSource = selectedFilePath;
            }
        }

        public void setData(Book book)
        {
            ViewModel.SetData(book);

            classifyComboBox.SelectedItem = genreList.Find(x => x.Name == book.Genre);

            //bookNameTextBox.Text = book.Title;
            //authorTextBox.Text = book.Author;
            //yearTextBox.Text = book.Year.ToString();
            //classifyComboBox.SelectedItem = genreList.Find(x => x.Name == book.Genre);
            //sellingPricesTextBox.Text = book.Price.ToString();
            //purchasePriceTextBox.Text = book.PurchasePrice.ToString();
            //quantityTextBox.Text = book.Quantity.ToString();
            //desciptionTextBox.Text = book.Description;
        }

        public void clearData()
        {
            ViewModel.ClearData();
            classifyComboBox.SelectedIndex = 0;
            //bookNameTextBox.Text = "";
            //authorTextBox.Text = "";
            //yearTextBox.Text = "";
            //classifyComboBox.SelectedIndex = 0;
            //sellingPricesTextBox.Text = "";
            //purchasePriceTextBox.Text = "";
            //quantityTextBox.Text = "";
            //desciptionTextBox.Text = "";
        }

        public void ClearErrorMessage()
        {
            ViewModel.ClearErrorMessage();
        }
    }
}
