﻿using Books_Store_Management_App.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using Books_Store_Management_App;
using Books_Store_Management_App.Models;

namespace Books_Store_Management_App.ViewModels
{
    /// <summary>
    /// Lớp ViewModel cho trang ClassificationClass để hiển thị danh sách ClassificationClass.
    /// </summary>
    public class ClassificationClassViewModel : INotifyPropertyChanged
    {
        // Properties for search, pagination, and sorting
        public string Keyword { get; set; } = ""; // Search keyword
        public int CurrentPage { get; set; } = 1; // Current page number in pagination
        public int RowsPerPage { get; set; } = 10; // Rows per page
        public int TypeOfSearch { get; set; } = 1; // Type of search criteria
        public int TypeOfSort { get; set; } = 1; // Sorting criteria
        public int TotalPages { get; set; } = 0; // Total number of pages
        public int TotalItems { get; set; } = 0; // Total items found

        public int MaxId { get; set; } = 0; // Maximum ID value for new items

        // DAO interface to handle data operations
        private IDaos<ClassificationClass> _dao = null;

        // Event to notify UI of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Property for the currently selected ClassificationClass
        private ClassificationClass _selectedClassificationClass;

        // Collection to hold ClassificationClass items for binding
        public FullObservableCollection<ClassificationClass> ClassificationClasss { get; set; }

        // Initializes the ViewModel, loads data from the database
        public void Init()
        {
            _dao = new ClassificationClassDao(); // Initialize DAO with a concrete implementation
            _dao.loadDatafromDbList(); // Load data into DAO's list
            GetAllClassificationClasss(); // Fetch initial data for display
        }

        // Fetches all ClassificationClass items based on current search, pagination, and sorting settings
        public void GetAllClassificationClasss()
        {
            var (totalItems, classificationClasss) = _dao.GetAll(
                CurrentPage, RowsPerPage, Keyword, TypeOfSearch, TypeOfSort);

            // Populate the observable collection with fetched items
            ClassificationClasss = new FullObservableCollection<ClassificationClass>(
                classificationClasss
            );

            // Update total items and calculate total pages
            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage)
                         + ((TotalItems % RowsPerPage == 0) ? 0 : 1);
            int cnt = 0;
            foreach (var item in ClassificationClasss)
            {
                cnt++;
                item.Index = cnt;
                MaxId = MaxId < Convert.ToInt32(item.ID) ? Convert.ToInt32(item.ID) : MaxId;
            }

        }

        // Inserts a new ClassificationClass entry
        public void InsertClassificationClass(ClassificationClass classificationClass)
        {
            _dao.Insert(classificationClass);
        }

        // Deletes an existing ClassificationClass by its ID
        public void DeleteClassificationClass(string id)
        {
            _dao.Delete(id);
        }

        // Updates an existing ClassificationClass with new data
        public void EditClassificationClass(ClassificationClass newClassificationClass)
        {
            _dao.Save(newClassificationClass);
        }

        // Updates the current page and refreshes the ClassificationClass list for the new page
        public void LoadingPage(int page)
        {
            CurrentPage = page;
            GetAllClassificationClasss();
        }

        // Raises a PropertyChanged event to notify the UI of a property update
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Property for the selected ClassificationClass, notifying UI when changed
        public ClassificationClass SelectedClassificationClass
        {
            get => _selectedClassificationClass;
            set
            {
                _selectedClassificationClass = value;
                OnPropertyChanged(nameof(SelectedClassificationClass));
            }
        }
    }
}