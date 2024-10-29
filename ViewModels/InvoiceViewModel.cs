using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.ViewModels
{
    public class Address
    {
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
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
