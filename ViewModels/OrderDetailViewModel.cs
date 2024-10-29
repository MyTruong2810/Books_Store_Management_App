using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;
using System.Security.Policy;

namespace Books_Store_Management_App.ViewModels
{
    public class OrderDetailViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        //private readonly IDao<Book> _bookDao;
        public List<Book> Books { get; set; }
        public List<Coupon> Coupons { get; set; }

        public string CustomerNameError { get; set; } = "";
        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Boolean IsDelivered { get; set; }

        private FullObservableCollection<Coupon> _selectedCoupons;
        public FullObservableCollection<Coupon> SelectedCoupons
        {
            get => _selectedCoupons;
            set
            {
                _selectedCoupons = value;
                OnPropertyChanged(nameof(SelectedCoupons));
                OnPropertyChanged(nameof(ActualTotal));
                OnPropertyChanged(nameof(HasCoupon));

                if (_selectedCoupons != null)
                    _selectedCoupons.CollectionChanged -= SelectedCoupons_CollectionChanged;

                if (_selectedCoupons != null)
                    _selectedCoupons.CollectionChanged += SelectedCoupons_CollectionChanged;
            }
        }

        private void SelectedCoupons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasCoupon));
            OnPropertyChanged(nameof(ActualTotal));
        }
        public bool HasCoupon => (SelectedCoupons.Count > 0);

        private ObservableCollection<OrderItem> _selectedBooks;
        public string SelectedBooksError { get; set; } = "";
        [Required(ErrorMessage = "Please select at least one book.")]
        [MinLength(1, ErrorMessage = "Please select at least one book.")]
        public ObservableCollection<OrderItem> SelectedBooks
        {
            get => _selectedBooks;
            set
            {
                _selectedBooks = value;
                OnPropertyChanged(nameof(SelectedBooks));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(ActualTotal));
            }
        }
        public Order Order { get; set; }
        public double Total
        {
            get
            {
                double total = 0;
                foreach (OrderItem item in SelectedBooks)
                {
                    total += item.SubTotal;
                }

                return total;
            }
        }
        public double ActualTotal
        {
            get
            {
                double total = Total;
                double totalDiscount = 0;
                foreach (Coupon coupon in SelectedCoupons)
                {
                    totalDiscount += coupon.Discount;
                }

                total -= total * totalDiscount;

                return total;
            }
        }

        // TODO
        public Dictionary<string, string> PaymentMethods { get; set; } = new Dictionary<string, string>
        {
            { "Cash", "/Assets/cash.jpg" },
            { "Momo", "/Assets/momo_qr.png" },
            { "VNPay", "/Assets/vnpay_qr.jpg" }
        };
        public string PaymentMethodQRCode { get; set; } = "/Assets/cash.jpg";
        // 

        public bool IsQrCodeVisible { get; set; } = false;
        public bool IsBooksListViewVisible { get; set; } = true;

        public ICommand ChangeToQRCodeCommand { get; set; }
        public ICommand BookSelectionChangedCommand { get; set; }
        public ICommand BookSeletionDeleteCommand { get; set; }
        public ICommand HandleCreateNewOrderCommand { get; set; }
        public ICommand DateSelectedCommand { get; set; }
        public ICommand TimeSelectedCommand { get; set; }

        public OrderDetailViewModel()
        {
            SelectedBooks = new ObservableCollection<OrderItem>();
            SelectedCoupons = new FullObservableCollection<Coupon>();
            PurchaseDate = DateTime.Now;
            LoadBooks();
            LoadGenre();

            // Initialize the command
            BookSelectionChangedCommand = new RelayCommand(OnBookSelectionChanged);
            BookSeletionDeleteCommand = new RelayCommand(OnBookSelectionDelete);
            HandleCreateNewOrderCommand = new RelayCommand(HandleCreateNewOrder);
            DateSelectedCommand = new RelayCommand(HandleDateSelected);
            TimeSelectedCommand = new RelayCommand(HandleTimeSelected);
            ChangeToQRCodeCommand = new RelayCommand(ChangeToQRCode);

            SelectedCoupons.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(ActualTotal)); // Cập nhật ActualTotal khi SelectedCoupons thay đổi
            };

            SelectedBooks.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (OrderItem newItem in e.NewItems)
                    {
                        newItem.PropertyChanged += OnOrderItemPropertyChanged;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (OrderItem oldItem in e.OldItems)
                    {
                        oldItem.PropertyChanged -= OnOrderItemPropertyChanged;
                    }
                }

                OnPropertyChanged(nameof(Total)); // Cập nhật Total khi danh sách SelectedBooks thay đổi
                OnPropertyChanged(nameof(ActualTotal)); // Cập nhật ActualTotal khi danh sách SelectedBooks thay đổi
            };

        }

        private void ChangeToQRCode(object obj)
        {
            IsQrCodeVisible = !IsQrCodeVisible;
            IsBooksListViewVisible = !IsBooksListViewVisible;

            OnPropertyChanged(nameof(IsQrCodeVisible));
            OnPropertyChanged(nameof(IsBooksListViewVisible));
        }

        private void HandleTimeSelected(object parameter)
        {
            var selectedTime = parameter as TimeSpan?;
            if (selectedTime != null)
            {
                PurchaseDate = PurchaseDate.Date + selectedTime.Value;
                OnPropertyChanged(nameof(PurchaseDate));
            }
        }

        private void HandleDateSelected(object parameter)
        {
            if (parameter != null)
            {
                Console.WriteLine("Parameter Type: " + parameter.GetType());

                if (parameter is DateTime selectedDate)
                {
                    if (PurchaseDate.GetType() == selectedDate.GetType())
                    {

                        PurchaseDate = selectedDate;
                    }
                    OnPropertyChanged(nameof(PurchaseDate));
                }
                else
                {
                    // Nếu parameter không phải DateTime, xử lý phù hợp
                    Console.WriteLine("Incorrect parameter type.");
                }
            }
        }

        private void HandleCreateNewOrder(object obj)
        {

        }

        private void OnOrderItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Cập nhật Total khi Quantity hoặc SubTotal của OrderItem thay đổi
            if (e.PropertyName == nameof(OrderItem.Quantity) || e.PropertyName == nameof(OrderItem.SubTotal))
            {
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(ActualTotal));
            }
        }

        private void LoadBooks()
        {
            //var books = await _bookDao.GetAllAsync();
            var Dao = new PsqlDao();
            Books = Dao.GetAllBooks().ToList();
        }

        private void LoadGenre()
        {
            var coupons = new List<Coupon>
               {
                   new Coupon { Id = 1, Name = "10% OFF", Discount = 0.1, ExpiryDate = new DateTime(2021, 12, 31) },
                   new Coupon { Id = 2, Name = "20% OFF", Discount = 0.2, ExpiryDate = new DateTime(2021, 12, 31) },
                   new Coupon { Id = 3, Name = "30% OFF", Discount = 0.3, ExpiryDate = new DateTime(2021, 12, 31) }
               };

            Coupons = coupons;
        }

        public void AddSelectedCoupons(FullObservableCollection<Coupon> coupons)
        {
            if (coupons != null)
            {
                foreach (var coupon in coupons)
                {
                    if (!SelectedCoupons.Contains(coupon) && Coupons.Contains(coupon))
                    {
                        SelectedCoupons.Add(coupon);
                    }
                }
            }
        }

        public void AddSelectedBooks(List<OrderItem> orderItems)
        {
            if (orderItems != null)
            {
                foreach (var orderItem in orderItems)
                {
                    if (!SelectedBooks.Contains(orderItem) && Books.Contains(orderItem.Book))
                    {
                        SelectedBooks.Add(orderItem);
                    }
                }
            }
        }
        private void OnBookSelectionChanged(object parameter)
        {
            var e = parameter as Syncfusion.UI.Xaml.Editors.ComboBoxSelectionChangedEventArgs;

            // Thêm sách được chọn vào ListView (nếu nó chưa có)
            foreach (var addedItem in e.AddedItems)
            {
                if (addedItem is Book book && !SelectedBooks.Any(item => item.Book.Index == book.Index))
                {
                    SelectedBooks.Add(new OrderItem
                    {
                        Book = book,
                        Quantity = 1
                    });
                }
            }

            // Xóa sách đã bỏ chọn khỏi ListView
            foreach (var removedItem in e.RemovedItems)
            {
                //OrderItem orderItem = SelectedBooks.FirstOrDefault(b => b.Book == removedItem);
                var bookToRemove = removedItem as Book;
                if (bookToRemove != null)
                {
                    var itemToRemove = SelectedBooks.FirstOrDefault(item => item.Book.Index == bookToRemove.Index);
                    if (itemToRemove != null)
                    {
                        SelectedBooks.Remove(itemToRemove);
                    }
                    else
                    {
                        // TO DO: Xử lý trường hợp removedItem không phải Book
                    }
                }
                else
                {
                    // TO DO: Xử lý trường hợp removedItem không phải Book
                }
            }
        }
        private void OnBookSelectionDelete(object obj)
        {
            OrderItem orderItem = obj as OrderItem;
            if (orderItem != null)
            {
                SelectedBooks.Remove(orderItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Triển khai INotifyDataErrorInfo

        public void ValidateAll()
        {
            ValidateProperty(CustomerName, nameof(CustomerName));
            CustomerNameError = GetErrorMessage(nameof(CustomerName));
            ValidateProperty(SelectedBooks, nameof(SelectedBooks));
            SelectedBooksError = GetErrorMessage(nameof(SelectedBooks));

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
    }

}
