using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Books_Store_Management_App.Models
{
    public class ClassificationClassDao : IDaos<ClassificationClass>
    {

        public List<ClassificationClass> Db = new List<ClassificationClass>();

        public void loadDatafromDbList()
        {
            for (int i = 0; i < 15; i++)
            {
                var newClassificationClass = new ClassificationClass
                {
                    ID = $"#{i}",
                    Tags = $"LTCuberik{i}",
                };
                Db.Add(newClassificationClass);
            }
        }

        public Tuple<int, List<ClassificationClass>> GetAll(
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
                    origin = origin.OrderBy(e => e.ID);
                    break;
            }


            var totalItems = origin.Count();

            var result = origin // IEnumerable<Employee>
                .Skip((page - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();

            return new Tuple<int, List<ClassificationClass>>(totalItems, result);
        }

        //Change when connect with database
        public void Insert(ClassificationClass insertItem)
        {
            if (insertItem != null)
            {
                Db.Add(insertItem);
            }
        }

        //Change when connect with database
        public ClassificationClass LoadProfile(string id)
        {
            return Db.FirstOrDefault(e => e.ID == id);
        }

        //Change when connect with database
        public void Save(ClassificationClass profile)
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
