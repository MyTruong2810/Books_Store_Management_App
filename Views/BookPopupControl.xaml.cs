using Books_Store_Management_App.Models;
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
    /// <summary>
    /// Một User Control dùng để hiển thị thông tin chi tiết của một cuốn sách
    /// Gồm các trường thông tin như tên sách, tác giả, năm xuất bản, thể loại, giá bán, giá mua, số lượng, mô tả
    /// </summary>
    public sealed partial class BookPopupControl : UserControl
    {
        // Danh sách thể loại sách
        private List<Genre> genreList;

        public BookPopupControlViewModel ViewModel { get; set; }
        public BookPopupControl()
        {
            this.InitializeComponent();

            // Lấy danh sách thể loại sách từ database
            IDao dao = new PsqlDao();
            genreList = dao.GetAllGenres().ToList();
            classifyComboBox.ItemsSource = genreList;

            ViewModel = new BookPopupControlViewModel();
        }

        public event EventHandler<dynamic> SaveButtonClicked;

        /// <summary>
        /// Đặt tiêu đề cho popup
        /// </summary>
        /// <param name="title"></param>
        public void SetPopupTitle(string title)
        {
            TitleTextBlock.Text = title;
        }

        /// <summary>
        /// Đặt chiều rộng cho popup
        /// </summary>
        public double RightDialogHeight
        {
            get => rightDialog.Height;
            set => rightDialog.Height = value;
        }

        /// <summary>
        /// Đặt trạng thái cho các trường nhập liệu
        /// Nếu isEditable = true thì các trường nhập liệu sẽ được phép chỉnh sửa
        /// Nếu isEditable = false thì các trường nhập liệu sẽ bị khóa
        /// </summary>
        /// <param name="isEditable"></param>
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

        /// <summary>
        /// Đặt nội dung cho nút (Save, Edit)
        /// </summary>
        /// <param name="text"></param>
        public void SetButton(string text)
        {
            SaveBtn.Content = text;
        }

        /// <summary>
        /// Lấy nội dung của nút (Save, Edit)
        /// </summary>
        /// <returns></returns>
        public string GetButton()
        {
            return SaveBtn.Content.ToString();
        }

        /// <summary>
        /// Xóa nội dung của các trường nhập liệu
        /// </summary>
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

        /// <summary>
        /// Xử lý xự kiện khi nút X được nhấn
        /// Đóng popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xBtn_Click(object sender, RoutedEventArgs e)
        {
            // Logic để đóng popup
            var parent = this.Parent as Popup;
            if (parent.IsOpen) { parent.IsOpen = false; }

        }

        /// <summary>
        /// Xử lý xự kiện khi nút Cancel được nhấn
        /// Đóng popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic để hủy
            var parent = this.Parent as Popup;
            if (parent.IsOpen) { parent.IsOpen = false; }
        }

        /// <summary>
        /// Xử lý xự kiện khi hover vào nút Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            // Logic khi rê chuột vào nút
            // Làm mờ nút
            xBtn.Background.Opacity = 0.5;
        }

        /// <summary>
        /// Xử lý xự kiện khi rời chuột khỏi nút Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            // Làm nút trở lại bình thường
            xBtn.Background.Opacity = 1;
        }

        /// <summary>
        /// Xử lý xự kiện khi nút Save được nhấn
        /// Lưu thông tin sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate dữ liệu
            ViewModel.ValidateAll();

            // Nếu có lỗi thì không thực hiện lưu
            if (ViewModel.HasErrors)
            {
                return;
            }

            // Đưa dữ liệu từ comboxbox vào viewmodel
            ViewModel.Genre = classifyComboBox.SelectedItem.ToString();

            // Lưu thông tin từ các trường nhập liệu vào ViewModel
            ViewModel.MergeToBook();

            var parent = this.Parent as Popup;
            if (parent.IsOpen) { parent.IsOpen = false; }
            //SaveButtonClicked.Invoke(this, ViewModel);
            var persist = new
            {
                Book = ViewModel.Book,
                ImageInfo = ViewModel.ImageInfo,
            };

            SaveButtonClicked.Invoke(this, persist);
        }

        /// <summary>
        /// Hiển thị dialog để chọn ảnh
        /// Note Chọn Ảnh trong Assets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChangeIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}Assets"; // Thay đổi đường dẫn này thành thư mục bạn muốn
            openFileDialog.Filter = "All files (*.*)|*.*"; // Bạn có thể thay đổi bộ lọc file nếu cần
            openFileDialog.Title = "Chọn một file";


            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                string safeFileName = openFileDialog.SafeFileName;
                // get image type like image/jpeg ...
                string fileType = "image/" + System.IO.Path.GetExtension(fileName).Split(".")[1];

                string selectedFilePath = "http://localhost:9000/bookstore/"+safeFileName;

                Console.WriteLine("File đã chọn: " + selectedFilePath);

                // Hiển thị ảnh đã chọn lên giao diện
                ViewModel.ImageSource = fileName;

                // ViewModel.ImageInfo is dynamic type
                ViewModel.ImageInfo = new
                {
                    ObjectName = safeFileName,
                    FileName = fileName,
                    ContentType = fileType
                };


                // Lưu đường dẫn ảnh vào ViewModel
            }
        }

        /// <summary>
        /// Gán dữ liệu sách vào ViewModel
        /// </summary>
        /// <param name="book"></param>
        public void setData(Book book)
        {
            ViewModel.SetData(book);

            // Chuyển Genre từ string sang Genre và gán vào combobox
            classifyComboBox.SelectedItem = genreList.Find(x => x.Name == book.Genre);
        }

        /// <summary>
        /// Xóa dữ liệu sách    
        /// </summary>
        public void clearData()
        {
            ViewModel.ClearData();
            classifyComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Xóa thông báo lỗi
        /// </summary>
        public void ClearErrorMessage()
        {
            ViewModel.ClearErrorMessage();
        }
    }
}
