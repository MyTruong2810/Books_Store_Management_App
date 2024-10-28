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

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassificationPage : Page
    {
        public ClassificationClassViewModel ClassificationClassVM { get; set; }

        private ClassificationClass editingClassificationClass = null;
        public ClassificationPage()
        {
            this.InitializeComponent();
            ClassificationClassVM = new ClassificationClassViewModel();
            ClassificationClassVM.Init();
            UpdatePagingInfo_bootstrap();
            UpdateRowPerPageIfo_bootstrap();
        }

        void UpdatePagingInfo_bootstrap()
        {
            var infoList = new List<object>();
            for (int i = 1; i <= ClassificationClassVM.TotalPages; i++)
            {
                infoList.Add(new
                {
                    Page = i,
                    Total = ClassificationClassVM.TotalPages

                });
            }

            pagesComboBox.ItemsSource = infoList;
            pagesComboBox.SelectedIndex = 0;
        }

        void UpdateRowPerPageIfo_bootstrap()
        {

            var infoShow = new List<object>();
            for (int i = 1; i <= ClassificationClassVM.TotalItems; i++)
            {
                infoShow.Add(new { item = i });
            }
            rowsPerPageComboBox.ItemsSource = infoShow;
            rowsPerPageComboBox.SelectedIndex = 9;
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var newClassificationClass = new ClassificationClass();
            newClassificationClass.ID = $"#{ClassificationClassVM.TotalItems + 1}";
            // Lưu thông tin khách hàng vào ClassificationClassVM.SelectedClassificationClass
            ClassificationClassVM.SelectedClassificationClass = newClassificationClass;

            // Hiển thị hộp thoại xác nhận
            ContentDialogResult result = await AddClassificationClassDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                ClassificationClassVM.InsertClassificationClass(ClassificationClassVM.SelectedClassificationClass);
                ClassificationClassVM.GetAllClassificationClasss();
            }
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ID của khách hàng từ nút được nhấn
            var button = sender as Button;
            var ClassificationClassId = button?.Tag?.ToString();

            if (ClassificationClassId != null)
            {
                // Tìm khách hàng dựa trên ID
                var newClassificationClass = ClassificationClassVM.ClassificationClasss.FirstOrDefault(c => c.ID == ClassificationClassId);

                if (newClassificationClass != null)
                {
                    // Lưu thông tin khách hàng vào ClassificationClassVM.SelectedClassificationClass
                    ClassificationClassVM.SelectedClassificationClass = newClassificationClass;
                    // Hiển thị hộp thoại xác nhận
                    ContentDialogResult result = await EditClassificationClassDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        ClassificationClassVM.EditClassificationClass(ClassificationClassVM.SelectedClassificationClass);
                        ClassificationClassVM.GetAllClassificationClasss();
                    }
                }
            }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ID của khách hàng từ nút được nhấn
            var button = sender as Button;
            var ClassificationClassId = button?.Tag?.ToString();

            if (ClassificationClassId != null)
            {
                // Tìm khách hàng dựa trên ID
                var delClassificationClass = ClassificationClassVM.ClassificationClasss.FirstOrDefault(c => c.ID == ClassificationClassId);

                if (delClassificationClass != null)
                {
                    // Lưu thông tin khách hàng vào ClassificationClassVM.SelectedClassificationClass
                    ClassificationClassVM.SelectedClassificationClass = delClassificationClass;

                    // Hiển thị hộp thoại xác nhận
                    ContentDialogResult result = await DeleteClassificationClassDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        // Xóa khách hàng nếu người dùng chọn "Xóa"
                        ClassificationClassVM.DeleteClassificationClass(ClassificationClassId);
                        ClassificationClassVM.GetAllClassificationClasss();
                    }
                }
            }
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedItem;

            if (item != null)
            {
                ClassificationClassVM.LoadingPage(item.Page);
            }
        }

        private void sortbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassificationClassVM.TypeOfSort = sortbyComboBox.SelectedIndex + 1;
            ClassificationClassVM.LoadingPage(1);
        }
        private void filterbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassificationClassVM.TypeOfSearch = filterSearchComboBox.SelectedIndex + 1;
            ClassificationClassVM.LoadingPage(1);
        }
        private void rowsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassificationClassVM.RowsPerPage = rowsPerPageComboBox.SelectedIndex + 1;
            ClassificationClassVM.LoadingPage(1);
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
            ClassificationClassVM.Keyword = keywordTextBox.Text;
            ClassificationClassVM.LoadingPage(1);
            UpdatePagingInfo_bootstrap();
        }
    }
}
