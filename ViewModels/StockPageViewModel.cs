using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.ViewModels
{
    /// <summary>
    /// Lớp ViewModel cho trang StockPage để hiển thị danh sách sách trong kho, nhưng chưa thực hiện logic theo mô hình
    /// MVVM hoàn chỉnh
    /// </summary>
    public class StockPageViewModel
    {
        public ObservableCollection<Book> AllBooks { get; set; }
        public void Init()
        {
            IDao dao = new PsqlDao();
            AllBooks = dao.GetAllBooks();
        }
    }
}
