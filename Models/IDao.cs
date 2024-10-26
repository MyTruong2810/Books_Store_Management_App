using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    public interface IDao
    {
        ObservableCollection<Book> GetAllBooks();
        ObservableCollection<Order> GetAllOrders();
    }
}


