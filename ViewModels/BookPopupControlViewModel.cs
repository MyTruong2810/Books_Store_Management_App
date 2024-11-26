using Books_Store_Management_App.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.ViewModels
{
    /// <summary>
    /// View model cho BookPopupControl
    /// Dùng để xử lý dữ liệu và validate dữ liệu nhập vào
    /// </summary>
    public class BookPopupControlViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Book _book;
        public Book Book
        {
            get => _book;
            set
            {
                if (_book != value)
                {
                    _book = value;
                    OnPropertyChanged(nameof(Book));
                }
            }
        }

        public dynamic ImageInfo { get; set; }

        // Các trường lấy thông tin để Validate xong mới đưa vào Book
        // Note: Sở dĩ không đưa trực tiếp vào Book vì khi nhập liệu có thể có lỗi,
        // nếu đưa vào Book ngay thì dữ liệu trong Book sẽ bị thay đổi ngay lập tức
        private string _title;
        private string _author;
        private string _publisher;
        private string _year;
        private string _isbn;
        private string _genre;
        private string _sellingPrices;
        private string _purchasePrice;
        private string _quantity;
        private string _description;
        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }
        //

        public string TitleErrorMessage { get; set; } = "";

        [Required(ErrorMessage = "Title is required.")]
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string AuthorErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "Author is required.")]
        public string Author
        {
            get => _author;
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }

        public string PublisherErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "Publisher is required.")]
        public string Publisher
        {
            get => _publisher;
            set
            {
                if (_publisher != value)
                {
                    _publisher = value;
                    OnPropertyChanged(nameof(Publisher));
                }
            }
        }

        public string YearErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Year must be 4 digits long.")]
        public string Year
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(nameof(Year));
                }
            }
        }

        public string ISBNErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression(@"^\d*$", ErrorMessage = "ISBN must be numeric.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must be exactly 13 characters long.")]
        public string ISBN
        {
            get => _isbn;
            set
            {
                if (_isbn != value)
                {
                    _isbn = value;
                    OnPropertyChanged(nameof(ISBN));
                }
            }
        }
        public string Genre
        {
            get => _genre;
            set
            {
                if (_genre != value)
                {
                    _genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        public string SellingPricesErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "Selling price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Selling price must be greater than 0.")]
        public string SellingPrices
        {
            get => _sellingPrices;
            set
            {
                if (_sellingPrices != value)
                {
                    _sellingPrices = value;
                    OnPropertyChanged(nameof(SellingPrices));
                }
            }
        }

        public string PurchasePriceErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "Purchase price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Purchase price must be greater than 0.")]
        public string PurchasePrice
        {
            get => _purchasePrice;
            set
            {
                if (_purchasePrice != value)
                {
                    _purchasePrice = value;
                    OnPropertyChanged(nameof(PurchasePrice));
                }
            }
        }

        public string QuantityErrorMessage { get; set; } = "";
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public string Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        /// <summary>
        /// Set dữ liệu cho các trường nhập liệu
        /// </summary>
        /// <param name="book"></param>
        public void SetData(Book book)
        {
            Book = book;
            Title = book.Title;
            Author = book.Author;
            Publisher = book.Publisher;
            Year = book.Year.ToString();
            ISBN = book.ISBN;
            Genre = book.Genre;
            SellingPrices = book.Price.ToString();
            PurchasePrice = book.PurchasePrice.ToString();
            Quantity = book.Quantity.ToString();
            Description = book.Description;
            ImageSource = book.ImageSource;
        }

        /// <summary>
        /// Xóa dữ liệu của các trường nhập liệu
        /// </summary>
        public void ClearData()
        {
            Book = null;
            Title = "";
            Author = "";
            Publisher = "";
            Year = "";
            ISBN = "";
            Genre = "";
            SellingPrices = "";
            PurchasePrice = "";
            Quantity = "";
            Description = "";
            ImageSource = "ms-appx:///Assets/default_image.jpg";
        }

        /// <summary>
        /// Xóa thông báo lỗi
        /// </summary>
        public void ClearErrorMessage()
        {
            TitleErrorMessage = "";
            AuthorErrorMessage = "";
            PublisherErrorMessage = "";
            YearErrorMessage = "";
            ISBNErrorMessage = "";
            SellingPricesErrorMessage = "";
            PurchasePriceErrorMessage = "";
            QuantityErrorMessage = "";
        }

        /// <summary>
        /// Đưa dữ liệu từ các trường nhập liệu vào Book
        /// </summary>
        public void MergeToBook()
        {
            if (Book == null)
            {
                Book = new Book();
            }

            Book.Title = Title;
            Book.Author = Author;
            Book.Publisher = Publisher;
            Book.Year = int.Parse(Year);
            Book.ISBN = ISBN;
            Book.Genre = Genre;
            Book.Price = double.Parse(SellingPrices);
            Book.PurchasePrice = double.Parse(PurchasePrice);
            Book.Quantity = int.Parse(Quantity);
            Book.Description = Description;
            //Book.ImageSource = ImageSource;
        }

        // Triển khai INotifyDataErrorInfo
        // Sẽ tách ra thành một class riêng nếu cần
        public void ValidateAll()
        {
            ValidateProperty(Title, nameof(Title));
            TitleErrorMessage = GetErrorMessage(nameof(Title));
            ValidateProperty(ISBN, nameof(ISBN));
            ISBNErrorMessage = GetErrorMessage(nameof(ISBN));
            ValidateProperty(Year, nameof(Year));
            YearErrorMessage = GetErrorMessage(nameof(Year));
            ValidateProperty(Author, nameof(Author));
            AuthorErrorMessage = GetErrorMessage(nameof(Author));
            ValidateProperty(Publisher, nameof(Publisher));
            PublisherErrorMessage = GetErrorMessage(nameof(Publisher));
            ValidateProperty(SellingPrices, nameof(SellingPrices));
            SellingPricesErrorMessage = GetErrorMessage(nameof(SellingPrices));
            ValidateProperty(PurchasePrice, nameof(PurchasePrice));
            PurchasePriceErrorMessage = GetErrorMessage(nameof(PurchasePrice));
            ValidateProperty(Quantity, nameof(Quantity));
            QuantityErrorMessage = GetErrorMessage(nameof(Quantity));

            // Kiểm tra thêm các thuộc tính khác nếu cần
        }

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        private void ValidateProperty(object value, string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(this) { MemberName = propertyName };
            bool isValid = Validator.TryValidateProperty(value, context, results);

            if (!isValid)
            {
                _errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public string GetErrorMessage(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName].First() : string.Empty;
        }
        // Kết thúc triển khai INotifyDataErrorInfo

        // Triển khai INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Kết thúc triển khai INotifyPropertyChanged
    }

}
