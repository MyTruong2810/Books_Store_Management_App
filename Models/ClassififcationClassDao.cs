using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Books_Store_Management_App.Models
{
    // Data Access Object (DAO) for managing ClassificationClass entities in memory
    public class ClassificationClassDao : IDaos<ClassificationClass>
    {
        // In-memory list representing the database for ClassificationClass objects
        public List<ClassificationClass> Db = new List<ClassificationClass>();

        // Loads sample data into the in-memory database
        public void loadDatafromDbList()
        {
            for (int i = 0; i < 15; i++)
            {
                var newClassificationClass = new ClassificationClass
                {
                    ID = $"#{i}",
                    Tags = $"LTCuberik{i}",
                };
                Db.Add(newClassificationClass); // Add each new ClassificationClass to Db list
            }
        }

        // Retrieves paginated and filtered ClassificationClass items based on search and sort parameters
        public Tuple<int, List<ClassificationClass>> GetAll(
            int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            IEnumerable<ClassificationClass> origin;

            // Apply search filter based on type of search
            switch (typerOfSearch)
            {
                case 2:
                    origin = Db.Where(e => e.Tags.Contains(keyword)); // Search by Tags
                    break;
                default:
                    origin = Db.Where(e => e.ID.Contains(keyword)); // Default search by ID
                    break;
            }

            // Apply sorting based on type of sort
            switch (typerOfSort)
            {
                case 2:
                    origin = origin.OrderBy(e => e.Tags); // Sort by Tags
                    break;
                default:
                    origin = origin.OrderBy(e => e.ID); // Default sort by ID
                    break;
            }

            // Count total items after filtering
            var totalItems = origin.Count();

            // Apply pagination and convert to list
            var result = origin
                .Skip((page - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();

            // Return the total item count and the paginated list as a tuple
            return new Tuple<int, List<ClassificationClass>>(totalItems, result);
        }

        // Inserts a new ClassificationClass item into the in-memory database
        public void Insert(ClassificationClass insertItem)
        {
            if (insertItem != null)
            {
                Db.Add(insertItem);
            }
        }

        // Loads a ClassificationClass profile based on ID (simulates a database retrieval)
        public ClassificationClass LoadProfile(string id)
        {
            return Db.FirstOrDefault(e => e.ID == id);
        }

        // Updates an existing ClassificationClass profile (simulates a database save operation)
        public void Save(ClassificationClass profile)
        {
            var oldInfo = Db.FirstOrDefault(e => e.ID == profile.ID);
            if (oldInfo != null)
            {
                Db.Remove(oldInfo); // Remove old record
                Db.Add(profile); // Add updated profile
            }
        }

        // Deletes a ClassificationClass item by ID, throws exception if ID not found
        public void Delete(string id)
        {
            var itemToDelete = Db.FirstOrDefault(e => e.ID == id);

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
