using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.ViewModels
{
    public class DashboardViewModel
    {
        public ObservableCollection<Book> Books { get; set; }

        public void Init()
        {
            IDao dao = new PsqlDao();
            Books = dao.GetAllBooks();
        }
    }
}
