using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Lớp đại diện việc thực hiện các thao tác với khách hàng.
    /// </summary>
    public class CustomerDao : IDaos<Customer>
    {
        // In-memory database of customers
        public ObservableCollection<Customer> Db { get; set; }
        public PsqlDao PsqlDao { get; set; }

        // Loads sample data into the in-memory customer list
        public void loadDatafromDbList()
        {
            PsqlDao = new PsqlDao();
            Db = PsqlDao.GetAllCustomers();
        }

        // Retrieves paginated and filtered customers based on search and sort criteria
        public Tuple<int,ObservableCollection<Customer>> GetAll(
            int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            IEnumerable<Customer> origin;

            // Apply search filter based on type of search
            switch (typerOfSearch)
            {
                case 2:
                    origin = Db.Where(e => e.Name.Contains(keyword)); // Search by Name
                    break;
                case 3:
                    origin = Db.Where(e => e.Phone.Contains(keyword)); // Search by Phone
                    break;
                case 4:
                    origin = Db.Where(e => e.Gender.Contains(keyword)); // Search by Gender
                    break;
                case 5:
                    origin = Db.Where(e => e.Address.Contains(keyword)); // Search by Address
                    break;
                default:
                    origin = Db.Where(e => e.ID.ToString().Contains(keyword)); // Default search by ID
                    break;
            }

            // Apply sorting based on type of sort
            switch (typerOfSort)
            {
                case 2:
                    origin = origin.OrderBy(e => e.Name); // Sort by Name
                    break;
                case 3:
                    origin = origin.OrderBy(e => e.Phone); // Sort by Phone
                    break;
                case 4:
                    origin = origin.OrderBy(e => e.Gender); // Sort by Gender
                    break;
                case 5:
                    origin = origin.OrderBy(e => e.Address); // Sort by Address
                    break;
                default:
                    origin = origin.OrderBy(e => e.ID); // Default sort by ID
                    break;
            }

            // Calculate total items after filtering
            var totalItems = origin.Count();

            // Apply pagination to filtered results
            var result = origin
                .Skip((page - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();


            ObservableCollection<Customer> observableResult = new ObservableCollection<Customer>(result);
            // Return the total item count and the paginated list as a tuple
            return new Tuple<int, ObservableCollection<Customer>>(totalItems, observableResult);
        }

        // Inserts a new customer record into the in-memory database
        public void Insert(Customer insertItem)
        {
            if (insertItem != null)
            {
                Db.Add(insertItem);
            }
        }

        // Retrieves a customer profile based on ID (simulates database retrieval)
        public Customer LoadProfile(string id)
        {
            return Db.FirstOrDefault(e => e.ID.ToString() == id);
        }

        // Updates an existing customer profile (simulates database update operation)
        public void Save(Customer profile)
        {
            var oldInfo = Db.FirstOrDefault(e => e.ID == profile.ID);
            if (oldInfo != null)
            {
                Db.Remove(oldInfo); // Remove old record
                Db.Add(profile); // Add updated profile
            }
        }

        // Deletes a customer record by ID, throws exception if ID is not found
        public void Delete(string id)
        {
            var itemToDelete = Db.FirstOrDefault(e => e.ID.ToString() == id);

            // Check if the item exists before deletion
            if (itemToDelete != null)
            {
                Db.Remove(itemToDelete);
            }
            else
            {
                throw new ArgumentException($"Item with ID '{id}' not found.");
            }
        }
    }
}
