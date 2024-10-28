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
    public interface IDaos<T>
    {
        // Load the data of a specific profile (could be by ID, username, etc.)
        T LoadProfile(string id);
        Tuple<int, List<T>> GetAll(
            int page,
            int rowsPerPage,
            string keyword,
            int typerOfSearch,
            int typerOfSort
        );

        void loadDatafromDbList();

        void Insert(T insertItem);

        // Save or update the profile data
        void Save(T profile);

        // Optionally delete profile if needed
        void Delete(string id);
    }
}


