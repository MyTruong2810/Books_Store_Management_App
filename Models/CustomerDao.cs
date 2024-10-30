using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Books_Store_Management_App.Models
{
    public class CustomerDao : IDaos<Customer>
    {

        public List<Customer> Db = new List<Customer>();

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
                Db.Add(newCustomer);
            }
        }

        public Tuple<int, List<Customer>> GetAll(
            int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            IEnumerable<Customer> origin;

            switch (typerOfSearch)
            {
                case 2:
                    origin = Db.Where(e => e.Name.Contains(keyword));
                    break;
                case 3:
                    origin = Db.Where(e => e.Phone.Contains(keyword));
                    break;
                case 4:
                    origin = Db.Where(e => e.Gender.Contains(keyword));
                    break;
                case 5:
                    origin = Db.Where(e => e.DateofBirth.Contains(keyword));
                    break;
                case 6:
                    origin = Db.Where(e => e.Address.Contains(keyword));
                    break;
                default:
                    origin = Db.Where(e => e.ID.Contains(keyword));
                    break;
            }

            switch (typerOfSort)
            {
                case 2:
                    origin = origin.OrderBy(e => e.Name);
                    break;
                case 3:
                    origin = origin.OrderBy(e => e.Phone);
                    break;
                case 4:
                    origin = origin.OrderBy(e => e.Gender);
                    break;
                case 5:
                    origin = origin.OrderBy(e => e.DateofBirth);
                    break;
                case 6:
                    origin = origin.OrderBy(e => e.Address);
                    break;
                default:
                    origin = origin.OrderBy(e => e.ID);
                    break;
            }


            var totalItems = origin.Count();

            var result = origin // IEnumerable<Employee>
                .Skip((page - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();

            return new Tuple<int, List<Customer>>(totalItems, result);
        }

        //Change when connect with database
        public void Insert(Customer insertItem)
        {
            if (insertItem != null)
            {
                Db.Add(insertItem);
            }
        }

        //Change when connect with database
        public Customer LoadProfile(string id)
        {
            return Db.FirstOrDefault(e => e.ID == id);
        }

        //Change when connect with database
        public void Save(Customer profile)
        {
            var oldInfo = Db.FirstOrDefault(e => e.ID == profile.ID);
            if (oldInfo != null)
            {
                Db.Remove(oldInfo);
                Db.Add(profile);
            }
        }

        //Change when connect with database
        public void Delete(string id)
        {
            // Find the item with the specified ID
            var itemToDelete = Db.FirstOrDefault(e => e.ID == id);

            // Check if the item exists
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
