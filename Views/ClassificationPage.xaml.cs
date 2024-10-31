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
    /// Represents the Classification Management page where classification classes can be managed.
    /// </summary>
    public sealed partial class ClassificationPage : Page
    {
        public ClassificationClassViewModel ClassificationClassVM { get; set; }

        private ClassificationClass editingClassificationClass = null;

        public ClassificationPage()
        {
            this.InitializeComponent();
            // Initialize the ViewModel and load data
            ClassificationClassVM = new ClassificationClassViewModel();
            ClassificationClassVM.Init(); // Load initial data
            UpdatePagingInfo_bootstrap(); // Set up pagination
            UpdateRowPerPageIfo_bootstrap(); // Set up rows per page
        }

        // Initializes the pagination info for the ComboBox
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

            pagesComboBox.ItemsSource = infoList; // Bind the ComboBox to the page list
            pagesComboBox.SelectedIndex = 0; // Set default selected index
        }

        // Initializes the rows per page info for the ComboBox
        void UpdateRowPerPageIfo_bootstrap()
        {
            var infoShow = new List<object>();
            for (int i = 1; i <= ClassificationClassVM.TotalItems; i++)
            {
                infoShow.Add(new { item = i });
            }
            rowsPerPageComboBox.ItemsSource = infoShow; // Bind the ComboBox to the row options
            rowsPerPageComboBox.SelectedIndex = 9; // Set default selected index
        }

        // Event handler for adding a new classification class
        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var newClassificationClass = new ClassificationClass
            {
                ID = $"#{ClassificationClassVM.TotalItems + 1}"
            };
            // Set the new classification class as the selected one in ViewModel
            ClassificationClassVM.SelectedClassificationClass = newClassificationClass;

            // Show the dialog for adding the new classification class
            ContentDialogResult result = await AddClassificationClassDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) // If the user confirmed
            {
                ClassificationClassVM.InsertClassificationClass(ClassificationClassVM.SelectedClassificationClass); // Insert new class
                ClassificationClassVM.GetAllClassificationClasss(); // Refresh the list
            }
        }

        // Event handler for updating an existing classification class
        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var classificationClassId = button?.Tag?.ToString(); // Get ID from button Tag

            if (classificationClassId != null)
            {
                // Find the classification class by ID
                var newClassificationClass = ClassificationClassVM.ClassificationClasss.FirstOrDefault(c => c.ID == classificationClassId);

                if (newClassificationClass != null)
                {
                    ClassificationClassVM.SelectedClassificationClass = newClassificationClass; // Set selected class
                    // Show the dialog for editing
                    ContentDialogResult result = await EditClassificationClassDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary) // If confirmed
                    {
                        ClassificationClassVM.EditClassificationClass(ClassificationClassVM.SelectedClassificationClass); // Update class
                        ClassificationClassVM.GetAllClassificationClasss(); // Refresh the list
                    }
                }
            }
        }

        // Event handler for deleting a classification class
        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var classificationClassId = button?.Tag?.ToString(); // Get ID from button Tag

            if (classificationClassId != null)
            {
                // Find the classification class by ID
                var delClassificationClass = ClassificationClassVM.ClassificationClasss.FirstOrDefault(c => c.ID == classificationClassId);

                if (delClassificationClass != null)
                {
                    ClassificationClassVM.SelectedClassificationClass = delClassificationClass; // Set selected class

                    // Show the dialog for confirmation
                    ContentDialogResult result = await DeleteClassificationClassDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary) // If confirmed
                    {
                        ClassificationClassVM.DeleteClassificationClass(classificationClassId); // Delete the class
                        ClassificationClassVM.GetAllClassificationClasss(); // Refresh the list
                    }
                }
            }
        }

        // Event handler for page selection change
        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedItem;

            if (item != null)
            {
                ClassificationClassVM.LoadingPage(item.Page); // Load selected page
            }
        }

        // Event handler for sort selection change
        private void sortbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassificationClassVM.TypeOfSort = sortbyComboBox.SelectedIndex + 1; // Get selected sort type
            ClassificationClassVM.LoadingPage(1); // Reload page with new sort
        }

        // Event handler for filter selection change
        private void filterbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassificationClassVM.TypeOfSearch = filterSearchComboBox.SelectedIndex + 1; // Get selected search type
            ClassificationClassVM.LoadingPage(1); // Reload page with new filter
        }

        // Event handler for rows per page selection change
        private void rowsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassificationClassVM.RowsPerPage = rowsPerPageComboBox.SelectedIndex + 1; // Get selected rows per page
            ClassificationClassVM.LoadingPage(1); // Reload page
            UpdatePagingInfo_bootstrap(); // Update pagination info
        }

        // Event handler for next button click
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedIndex;
            if (item < pagesComboBox.Items.Count - 1) // Check if not the last page
            {
                pagesComboBox.SelectedIndex += 1; // Go to next page
            }
        }

        // Event handler for previous button click
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedIndex;
            if (item > 0) // Check if not the first page
            {
                pagesComboBox.SelectedIndex -= 1; // Go to previous page
            }
        }

        // Event handler for search button click
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            ClassificationClassVM.Keyword = keywordTextBox.Text; // Set keyword for searching
            ClassificationClassVM.LoadingPage(1); // Reload page with search
            UpdatePagingInfo_bootstrap(); // Update pagination info
        }
    }
}
