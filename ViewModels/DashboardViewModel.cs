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
    /// <summary>
    /// Lớp ViewModel cho trang Dashboard để hiển thị thông tin tổng quan.
    /// </summary>
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public int totalBook { get; set; }
        public int totalCustomer { get; set; }
        public int totalOrder { get; set; }
        public double totalRevenue { get; set; }
        public double totalMonth { get; set; }
        public double totalDay { get; set; }

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

            Customers = dao.GetAllCustomers();
            totalBook = Books.Count;
            totalOrder = Orders.Count;
            totalCustomer = Customers.Count; 
            totalRevenue = 0;
            totalMonth = 0;
            totalDay = 0;

            DateTime now = DateTime.Now; 

            for (int i = 0; i < Orders.Count; i++)
            {
                totalRevenue += Orders[i].Price;
                if (Orders[i].Date.Month == now.Month && Orders[i].Date.Year == now.Year)
                {
                    totalMonth += Orders[i].Price;
                }
                if (Orders[i].Date.Date == now.Date)
                {
                    totalDay += Orders[i].Price;
                }
            }
            OutStock = new ObservableCollection<Book>(Books.Where(x => x.Quantity < 10));
            BestSeller = new ObservableCollection<Book>(Books.OrderByDescending(x => x.Quantity).Take(5)); 
        }
    }
}
