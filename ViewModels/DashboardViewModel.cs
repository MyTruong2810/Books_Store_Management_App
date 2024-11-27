using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public int totalBook { get; set; }
        public int totalCustomer { get; set; }
        public int totalOrder { get; set; }
        public double totalRevenue { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<Book> BestSeller { get; set; }

        public ObservableCollection<Book> OutStock { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Order> Orders { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Init()
        {
            IDao dao = new PsqlDao();
            Books = dao.GetAllBooks();
            Orders = dao.GetAllOrders();
            totalBook = Books.Count;
            totalOrder = Orders.Count;
            totalCustomer = 10;
            totalRevenue = 0;
            for (int i = 0; i < Orders.Count; i++)
            {
                totalRevenue += Orders[i].Price;
            }
            OutStock = new ObservableCollection<Book>(Books.Where(x => x.Quantity < 10));

            //Todo: Take from the user not from the quanlity, change later
            BestSeller = new ObservableCollection<Book>(Books.OrderBy(x => x.Quantity).Take(5));
        }
    }
}
