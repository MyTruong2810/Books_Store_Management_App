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
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public string Keyword { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int RowsPerPage { get; set; } = 10;
        public int TypeOfSearch { get; set; } = 1;
        public int TypeOfSort { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;

        private IDaos<Customer> _dao = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private Customer _selectedCustomer;

        public FullObservableCollection<Customer> Customers { get; set; }

        public void Init()
        {
            _dao = new CustomerDao();
            _dao.loadDatafromDbList();
            GetAllCustomers();
        }

        public void GetAllCustomers()
        {
            var (totalItems, customers) = _dao.GetAll(
                CurrentPage, RowsPerPage, Keyword, TypeOfSearch, TypeOfSort);
            Customers = new FullObservableCollection<Customer>(
                customers
            );

            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage)
                         + ((TotalItems % RowsPerPage == 0)
                             ? 0 : 1);
        }

        public void InsertCustomer(Customer customer)
        {
            _dao.Insert(customer);
        }

        public void DeleteCustomer(string id)
        {
            _dao.Delete(id);
        }

        public void EditCustomer(Customer newCustomer)
        {
            _dao.Save(newCustomer);
        }

        public void LoadingPage(int page)
        {
            CurrentPage = page;
            GetAllCustomers();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
