using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Lớp đại diện cho một mã giảm giá.
    /// </summary>
    public class Coupon : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Discount { get; set; }
        public DateTime ExpiryDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coupon coupon)
            {
                return Id == coupon.Id;
            }

            return false;
        }
    }
}
