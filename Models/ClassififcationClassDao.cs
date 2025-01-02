using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Books_Store_Management_App.Models;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Provides operations for managing book classification categories.
    /// </summary>
    public class ClassificationClassDao : IDaos<ClassificationClass>
    {
        /// <summary>
        /// In-memory list simulating a database for classification objects.
        /// </summary>
        public ObservableCollection<ClassificationClass> Db = new ObservableCollection<ClassificationClass>();

        /// <summary>
        /// Loads sample data into the in-memory database.
        /// </summary>
        public void loadDatafromDbList()
        {
            PsqlDao psqlDao = new PsqlDao();
            foreach (var item in psqlDao.GetClassificationClasses())
            {
                Db.Add(item);
            }
        }

        /// <summary>
        /// Retrieves paginated and filtered classification items based on search and sort parameters.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="rowsPerPage">The number of rows per page.</param>
        /// <param name="keyword">The keyword for filtering items.</param>
        /// <param name="typerOfSearch">The search type (e.g., by ID or Tags).</param>
        /// <param name="typerOfSort">The sort type (e.g., by ID or Tags).</param>
        /// <returns>A tuple containing the total item count and a paginated list.</returns>
        public Tuple<int, ObservableCollection<ClassificationClass>> GetAll(
            int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            IEnumerable<ClassificationClass> origin;

            switch (typerOfSearch)
            {
                case 2:
                    origin = Db.Where(e => e.Tags.Contains(keyword));
                    break;
                default:
                    origin = Db.Where(e => e.ID.Contains(keyword));
                    break;
            }

            switch (typerOfSort)
            {
                case 2:
                    origin = origin.OrderBy(e => e.Tags);
                    break;
                default:
                    origin = origin.OrderBy(e => int.Parse(e.ID)); // Default sort by ID
                    break;
            }

            var totalItems = origin.Count();
            var result = origin.Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToList();
            ObservableCollection<ClassificationClass> observableResult = new ObservableCollection<ClassificationClass>(result);

            return new Tuple<int, ObservableCollection<ClassificationClass>>(totalItems, observableResult);
        }

        /// <summary>
        /// Inserts a new classification item into the in-memory database.
        /// </summary>
        /// <param name="insertItem">The classification item to insert.</param>
        public void Insert(ClassificationClass insertItem)
        {
            if (insertItem != null)
            {
                Db.Add(insertItem);
            }
        }

        /// <summary>
        /// Retrieves a classification item based on its ID.
        /// </summary>
        /// <param name="id">The ID of the classification item.</param>
        /// <returns>The classification item if found, otherwise null.</returns>
        public ClassificationClass LoadProfile(string id)
        {
            return Db.FirstOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// Updates an existing classification item.
        /// </summary>
        /// <param name="profile">The updated classification item.</param>
        /// <param name="temp">Temporary parameter (unused).</param>
        public void Save(ClassificationClass profile, string temp)
        {
            var oldInfo = Db.FirstOrDefault(e => e.ID == profile.ID);

            if (oldInfo != null)
            {
                Db.Remove(oldInfo);
                Db.Add(profile);
            }
        }

        /// <summary>
        /// Deletes a classification item by ID.
        /// </summary>
        /// <param name="id">The ID of the classification item to delete.</param>
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
