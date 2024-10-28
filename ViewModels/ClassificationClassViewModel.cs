using Books_Store_Management_App.Helpers;
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
    public class ClassificationClassViewModel : INotifyPropertyChanged
    {
        public string Keyword { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int RowsPerPage { get; set; } = 10;
        public int TypeOfSearch { get; set; } = 1;
        public int TypeOfSort { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;

        private IDaos<ClassificationClass> _dao = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private ClassificationClass _selectedClassificationClass;

        public FullObservableCollection<ClassificationClass> ClassificationClasss { get; set; }

        public void Init()
        {
            _dao = new ClassificationClassDao();
            _dao.loadDatafromDbList();
            GetAllClassificationClasss();
        }

        public void GetAllClassificationClasss()
        {
            var (totalItems, classificationClasss) = _dao.GetAll(
                CurrentPage, RowsPerPage, Keyword, TypeOfSearch, TypeOfSort);
            ClassificationClasss = new FullObservableCollection<ClassificationClass>(
                classificationClasss
            );

            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage)
                         + ((TotalItems % RowsPerPage == 0)
                             ? 0 : 1);
        }

        public void InsertClassificationClass(ClassificationClass classificationClass)
        {
            _dao.Insert(classificationClass);
        }

        public void DeleteClassificationClass(string id)
        {
            _dao.Delete(id);
        }

        public void EditClassificationClass(ClassificationClass newClassificationClass)
        {
            _dao.Save(newClassificationClass);
        }

        public void LoadingPage(int page)
        {
            CurrentPage = page;
            GetAllClassificationClasss();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
