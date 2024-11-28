using System;
using System.Collections.Generic;
using System.ComponentModel;
using Books_Store_Management_App.Helpers;

namespace Books_Store_Management_App.Models
{
    public class OrderItem : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity
        {
            get; set;
        }

        public double SubTotal
        {
            get
            {
                return Book.Price * Quantity;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return new OrderItem
            {
                Id = this.Id,
                Book = this.Book,
                Quantity = this.Quantity
            };
        }
    }

    public class Order : INotifyPropertyChanged, ICloneable
    {
        public string ID { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }

        public FullObservableCollection<Coupon> Coupons { get; set; }
        public double Discount
        {
            get
            {
                double totalDiscount = 0;
                foreach (Coupon coupon in Coupons)
                {
                    totalDiscount += coupon.Discount;
                }


                return totalDiscount;
            }
        }

        public Boolean IsDelivered { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public int Amount
        {
            get
            {
                int totalQuantity = 0;
                foreach (OrderItem item in OrderItems)
                {
                    totalQuantity += item.Quantity;
                }
                return totalQuantity;
            }
        }
        public double Price
        {
            get
            {
                double total = 0;
                foreach (OrderItem item in OrderItems)
                {
                    total += item.SubTotal;
                }
                return Math.Ceiling(total - total * Discount);
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                    OnPropertyChanged(nameof(IsEven)); // Notify when IsEven changes
                }
            }
        }

        public bool IsEven => Index % 2 == 0;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            var orderItemsClone = new List<OrderItem>();
            foreach (var item in OrderItems)
            {
                orderItemsClone.Add((OrderItem)item.Clone());
            }

            return new Order
            {
                ID = this.ID,
                Customer = this.Customer,
                Date = this.Date,
                Coupons = this.Coupons,
                IsDelivered = this.IsDelivered,
                OrderItems = orderItemsClone,
                Index = this.Index
            };
        }
    }

}
