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
    /// <summary>
    /// View model cho trang chi tiết đơn hàng
    /// Dùng để lưu trữ thông tin của một đơn hàng
    /// Và xử lý các logic liên quan đến đơn hàng
    /// </summary>
    public class OrderDetailViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        // Biến lưu trữ danh sách các sách hiện có trong cửa hàng
        public List<Book> Books { get; set; }
        // Biến lưu trữ danh sách các mã giảm giá hiện có
        public List<Coupon> Coupons { get; set; }

        // Chứa mã lỗi của thuộc tính CustomerName
        public string CustomerNameError { get; set; } = "";
        // Điều kiện validate cho thuộc tính CustomerName
        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; }

        // Chứa mã lỗi của thuộc tính PurchaseDate
        public DateTime PurchaseDate { get; set; }

        // Chứa mã lỗi của thuộc tính IsDelivered
        public Boolean IsDelivered { get; set; }

        // Chứa danh sách các mã giảm giá được chọn

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

        // Xử lý sự kiện khi danh sách SelectedCoupons thay đổi
        // Cập nhật lại ActualTotal và HasCoupon
        private void SelectedCoupons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasCoupon));
            OnPropertyChanged(nameof(ActualTotal));
        }

        // Kiểm tra xem có mã giảm giá nào được chọn không
        // Dùng để hiển thị thông báo trên giao diện
        public bool HasCoupon => (SelectedCoupons.Count > 0);

        // Chứa danh sách các sách được chọn
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

        // Tính tổng số tiền của các sách được chọn
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

        // Tính tổng số tiền thực tế sau khi áp dụng mã giảm giá
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
            // Khởi tạo các biến
            SelectedBooks = new ObservableCollection<OrderItem>();
            SelectedCoupons = new FullObservableCollection<Coupon>();
            PurchaseDate = DateTime.Now;
            LoadBooks();
            LoadCoupon();

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

            // Xử lý sự kiện khi danh sách SelectedBooks thay đổi
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

        // Xử lý sự kiện khi nhấn nút chuyển đổi giữa QR Code và ListView
        private void ChangeToQRCode(object obj)
        {
            IsQrCodeVisible = !IsQrCodeVisible;
            IsBooksListViewVisible = !IsBooksListViewVisible;

            OnPropertyChanged(nameof(IsQrCodeVisible));
            OnPropertyChanged(nameof(IsBooksListViewVisible));
        }

        // Xử lý sự kiện khi chọn thời gian
        // Thêm thời gian vào ngày mua
        private void HandleTimeSelected(object parameter)
        {
            var selectedTime = parameter as TimeSpan?;
            if (selectedTime != null)
            {
                PurchaseDate = PurchaseDate.Date + selectedTime.Value;
                OnPropertyChanged(nameof(PurchaseDate));
            }
        }

        // Xử lý sự kiện khi chọn ngày
        // Thêm ngày vào ngày mua
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

        // ToDo
        private void HandleCreateNewOrder(object obj)
        {

        }
        //

        // Xử lý sự kiện khi thuộc tính của OrderItem thay đổi
        // Cụ thể là Quantity và SubTotal
        private void OnOrderItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Cập nhật Total khi Quantity hoặc SubTotal của OrderItem thay đổi
            if (e.PropertyName == nameof(OrderItem.Quantity) || e.PropertyName == nameof(OrderItem.SubTotal))
            {
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(ActualTotal));
            }
        }

        // Load dữ liệu sách từ cơ sở dữ liệu
        private void LoadBooks()
        {
            //var books = await _bookDao.GetAllAsync();
            var Dao = new PsqlDao();
            Books = Dao.GetAllBooks().ToList();
        }

        // Load dữ liệu mã giảm giá
        // Hiện tại dùng dữ liệu cứng
        // Có thể thay thế bằng việc load từ cơ sở dữ liệu
        private void LoadCoupon()
        {
            var coupons = new List<Coupon>
               {
                   new Coupon { Id = 1, Name = "10% OFF", Discount = 0.1, ExpiryDate = new DateTime(2021, 12, 31) },
                   new Coupon { Id = 2, Name = "20% OFF", Discount = 0.2, ExpiryDate = new DateTime(2021, 12, 31) },
                   new Coupon { Id = 3, Name = "30% OFF", Discount = 0.3, ExpiryDate = new DateTime(2021, 12, 31) }
               };

            Coupons = coupons;
        }

        // Thêm mã giảm được chọn vào danh sách SelectedCoupons
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

        // Thêm sách được chọn vào danh sách SelectedBooks
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

        // Xử lý sự kiện khi sách được chọn hoặc bỏ chọn
        // Từ ComboBox chọn sách
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

            // Xóa sách đã bỏ chọn khỏi dang sách SelectedBooks
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

        // Xử lý sự kiện khi sách được xóa khỏi danh sách SelectedBooks
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
        // Sẽ tách phần này ra một class khác để quản lý lỗi
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
