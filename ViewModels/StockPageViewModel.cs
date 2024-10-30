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
    public class StockPageViewModel : INotifyPropertyChanged
    {
        //Todo: Implement the StockPageViewModel later
            public ObservableCollection<Book> AllBooks { get; set; }
            public void Init()
            {
                IDao dao = new PsqlDao();
                AllBooks = dao.GetAllBooks();
            }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
