using Books_Store_Management_App.Models;
using Books_Store_Management_App.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace Book_Store_Management
{
    // ViewModel for managing Customer entities in the Book Store Management app
    public class CustomerViewModel : INotifyPropertyChanged
    {
        // Properties for search, pagination, and sorting
        public string Keyword { get; set; } = ""; // Search keyword for filtering customers
        public int CurrentPage { get; set; } = 1; // Current page in pagination
        public int RowsPerPage { get; set; } = 10; // Number of rows per page
        public int TypeOfSearch { get; set; } = 1; // Type of search filter
        public int TypeOfSort { get; set; } = 1; // Sort order for results
        public int TotalPages { get; set; } = 0; // Total number of pages for pagination
        public int TotalItems { get; set; } = 0; // Total items in the current search

        // DAO for interacting with the Customer data source
        private IDaos<Customer> _dao = null;

        // Event to notify UI about property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Currently selected Customer for displaying details or editing
        private Customer _selectedCustomer;

        // Observable collection of Customer entities for UI binding
        public FullObservableCollection<Customer> Customers { get; set; }

        // Initialization method for setting up the DAO and loading initial data
        public void Init()
        {
            _dao = new CustomerDao(); // Initialize DAO with a specific CustomerDao instance
            _dao.loadDatafromDbList(); // Load data from the database
            GetAllCustomers(); // Retrieve initial list of customers
        }

        // Fetches customers from the DAO based on pagination, search, and sort settings
        public void GetAllCustomers()
        {
            var (totalItems, customers) = _dao.GetAll(
                CurrentPage, RowsPerPage, Keyword, TypeOfSearch, TypeOfSort);

            // Update the observable collection with fetched customers
            Customers = new FullObservableCollection<Customer>(customers);

            // Update total items and calculate total pages for pagination
            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage) + ((TotalItems % RowsPerPage == 0) ? 0 : 1);
        }

        // Inserts a new customer record into the database
        public void InsertCustomer(Customer customer)
        {
            _dao.Insert(customer);
        }

        // Deletes a customer record based on the provided ID
        public void DeleteCustomer(string id)
        {
            _dao.Delete(id);
        }

        // Updates an existing customer record with new data
        public void EditCustomer(Customer newCustomer)
        {
            _dao.Save(newCustomer);
        }

        // Loads a specific page of customers and updates the current page
        public void LoadingPage(int page)
        {
            CurrentPage = page;
            GetAllCustomers();
        }

        // Raises the PropertyChanged event to notify UI of property updates
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Property for the selected customer, notifying UI when the selected item changes
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
    }
}
