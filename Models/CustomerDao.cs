using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Books_Store_Management_App.Models
{
    // Data Access Object (DAO) for managing Customer entities in an in-memory list
    public class CustomerDao : IDaos<Customer>
    {
        // In-memory database of customers
        public List<Customer> Db = new List<Customer>();

        // Loads sample data into the in-memory customer list
        public void loadDatafromDbList()
        {
            for (int i = 0; i < 15; i++)
            {
                var newCustomer = new Customer
                {
                    ID = $"#{i}",
                    Name = $"LTCuberik{i}",
                    Phone = "0123 354 268",
                    Avatar = new BitmapImage(new Uri("ms-appx:///Assets/avatar01.jpg")),
                    Gender = "Male",
                    DateofBirth = "01-Jan-2002",
                    Payment = "Card - 0923 2358 2657",
                    CVV = 448,
                    Address = "Dong Hoa, Di An, Binh Duong, Viet Nam"
                };
                Db.Add(newCustomer); // Add each new customer to the list
            }
        }

        // Retrieves paginated and filtered customers based on search and sort criteria
        public Tuple<int, List<Customer>> GetAll(
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
                    origin = Db.Where(e => e.DateofBirth.Contains(keyword)); // Search by Date of Birth
                    break;
                case 6:
                    origin = Db.Where(e => e.Address.Contains(keyword)); // Search by Address
                    break;
                default:
                    origin = Db.Where(e => e.ID.Contains(keyword)); // Default search by ID
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
                    origin = origin.OrderBy(e => e.DateofBirth); // Sort by Date of Birth
                    break;
                case 6:
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

            // Return the total item count and the paginated list as a tuple
            return new Tuple<int, List<Customer>>(totalItems, result);
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
            return Db.FirstOrDefault(e => e.ID == id);
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
            var itemToDelete = Db.FirstOrDefault(e => e.ID == id);

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
