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
using OxyPlot.Axes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// Represents the Customer Management page where customer data can be managed.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        public CustomerViewModel CustomerVM { get; set; }

        private Customer editingCustomer = null;

        private PsqlDao psqlDao = new PsqlDao();

        public CustomerPage()
        {
            this.InitializeComponent();
            // Initialize the ViewModel and load initial data
            CustomerVM = new CustomerViewModel();
            CustomerVM.Init(); // Load customer data
            UpdatePagingInfo_bootstrap(); // Set up pagination
            UpdateRowPerPageIfo_bootstrap(); // Set up rows per page
        }

        // Initializes the pagination info for the ComboBox
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

            pagesComboBox.ItemsSource = infoList; // Bind the ComboBox to the page list
            pagesComboBox.SelectedIndex = 0; // Set default selected index
        }

        // Initializes the rows per page info for the ComboBox
        void UpdateRowPerPageIfo_bootstrap()
        {
            var infoShow = new List<object>();
            for (int i = 1; i <= CustomerVM.TotalItems; i++)
            {
                infoShow.Add(new { item = i });
            }
            rowsPerPageComboBox.ItemsSource = infoShow; // Bind the ComboBox to the row options
            rowsPerPageComboBox.SelectedIndex = 9; // Set default selected index
        }

        // Event handler for adding a new customer
        private async void addButton_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            //// Show the dialog for adding the new customer
            var newCustomer = new Customer
            {
                ID = CustomerVM.MaxId + 1,
                Avatar = "ms-appx:///Assets/Avatar.jpg",
                Name = "",
                Address = "",
                CVV = 0,
                Gender = "",
                Payment = "",
                Phone = ""
            };

            if (newCustomer != null)
            {
                CustomerVM.SelectedCustomer = newCustomer; // Set selected customer
                                                           // Show the dialog for editing
                ContentDialogResult result = await AddCustomerDialog.ShowAsync();

                if (result == ContentDialogResult.Primary) // If confirmed
                {
                    CustomerVM.InsertCustomer(CustomerVM.SelectedCustomer); // Insert customer
                    CustomerVM.GetAllCustomers(); // Refresh the customer list
                    PsqlDao psqlDao = new PsqlDao();
                    psqlDao.InsertCustomer(CustomerVM.SelectedCustomer);
                    CustomerVM.MaxId++;
                    CustomerVM.Init();
                }
            }
        }

        // Event handler for updating an existing customer
        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var customerId = button?.Tag?.ToString(); // Get ID from button Tag

            if (customerId != null)
            {
                // Find the customer by ID
                var newCustomer = CustomerVM.Customers.FirstOrDefault(c => c.ID.ToString() == customerId);

                if (newCustomer != null)
                {
                    CustomerVM.SelectedCustomer = newCustomer; // Set selected customer
                    // Show the dialog for editing
                    ContentDialogResult result = await EditCustomerDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary) // If confirmed
                    {
                        CustomerVM.EditCustomer(CustomerVM.SelectedCustomer); // Update customer
                        CustomerVM.GetAllCustomers(); // Refresh the customer list
                        psqlDao.UpdateCustomer(CustomerVM.SelectedCustomer); // Save to database
                    }
                }
            }
        }

        // Event handler for changing the customer's avatar image
        private async void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            // Create and configure FileOpenPicker to select an image
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            // IMPORTANT: Associate the picker with the current application window
            // This allows the picker to open correctly as a modal dialog relative to the app window.
            var hwnd = WindowHelper.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            // Open the picker and wait for the user to select a file
            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null) // If a file was selected
            {
                // Get the file path of the selected image
                string filePath = file.Path;

                // Update the customer's avatar path in the ViewModel
                CustomerVM.SelectedCustomer.Avatar = filePath;
            }
        }

        // Event handler for deleting a customer
        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var customerId = button?.Tag?.ToString(); // Get ID from button Tag

            if (customerId != null)
            {
                // Find the customer by ID
                var delCustomer = CustomerVM.Customers.FirstOrDefault(c => c.ID.ToString() == customerId);

                if (delCustomer != null)
                {
                    CustomerVM.SelectedCustomer = delCustomer; // Set selected customer

                    // Show the dialog for confirmation
                    ContentDialogResult result = await DeleteCustomerDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary) // If confirmed
                    {
                        CustomerVM.DeleteCustomer(customerId); // Delete the customer
                        CustomerVM.GetAllCustomers(); // Refresh the customer list
                        psqlDao.DeleteCustomer(delCustomer); // Delete from database
                        if (CustomerVM.MaxId == int.Parse(customerId)){
                            CustomerVM.MaxId--;
                        }
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
                CustomerVM.LoadingPage(item.Page); // Load selected page
            }
        }

        // Event handler for sort selection change
        private void sortbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerVM.TypeOfSort = sortbyComboBox.SelectedIndex + 1; // Get selected sort type
            CustomerVM.LoadingPage(1); // Reload page with new sort
        }

        // Event handler for filter selection change
        private void filterbyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerVM.TypeOfSearch = filterSearchComboBox.SelectedIndex + 1; // Get selected search type
            CustomerVM.LoadingPage(1); // Reload page with new filter
        }

        // Event handler for rows per page selection change
        private void rowsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerVM.RowsPerPage = rowsPerPageComboBox.SelectedIndex + 1; // Get selected rows per page
            CustomerVM.LoadingPage(1); // Reload page
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
            CustomerVM.Keyword = keywordTextBox.Text; // Set keyword for searching
            CustomerVM.LoadingPage(1); // Reload page with search
            UpdatePagingInfo_bootstrap(); // Update pagination info
        }
    }

    public class AlternatingRowTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? EvenTemplate { get; set; }
        public DataTemplate? OddTemplate { get; set; }

        protected override DataTemplate? SelectTemplateCore(object item, DependencyObject container)
        {
            var itemsControl = ItemsControl.ItemsControlFromItemContainer(container);
            int index = itemsControl?.IndexFromContainer(container) ?? -1;

            return index % 2 == 0 ? EvenTemplate : OddTemplate;
        }
    }
}
