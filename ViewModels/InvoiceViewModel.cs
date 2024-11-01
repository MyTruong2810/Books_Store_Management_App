using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.ViewModels
{
    /// <summary>
    /// Địa chỉ của một đơn vị
    /// Hiện tại dùng để giả lập địa chỉ của người bán và người mua
    /// </summary>
    public class Address
    {
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    /// <summary>
    /// ViewModel cho trang InvoicePage
    /// Dùng để lưu trữ thông tin của một hóa đơn
    /// Và xứ lý các logic liên quan đến hóa đơn
    /// </summary>
    public class InvoiceViewModel
    {
        public Order Order { get; set; }
        public Address SellerAddress { get; set; }
        public Address CustomerAddress { get; set; }
        public string InvoiceDate { get; set; }
        public InvoiceViewModel()
        {
            Order = new Order();

            var conveter = new DateTimeToStringConverter();
            InvoiceDate = (string)conveter.Convert(DateTime.Now, typeof(string), null, null);
        }

        // Tính tổng tiền của các mặt hàng trong đơn hàng
        public double SubTotal
        {
            get
            {
                double total = 0;
                foreach (OrderItem item in Order.OrderItems)
                {
                    total += item.SubTotal;
                }

                return total;
            }
        }

        // Tính tổng số tiền được giảm giá
        public double TotalDiscount
        {
            get
            {
                double totalDiscount = SubTotal * Order.Discount;

                return totalDiscount;
            }
        }
    }
}
