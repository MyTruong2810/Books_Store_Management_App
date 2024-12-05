using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App
{
    public class OrderPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Order> _orders;

        public PsqlDao PsqlDao { get; set; }
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public OrderPageViewModel()
        {
            //_orderDao = orderDao;
            LoadOrders();
        }

        public void LoadOrders()
        {
            var orders = new PsqlDao().GetAllOrders();
            Orders = new ObservableCollection<Order>(orders);
            PsqlDao = new PsqlDao();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
