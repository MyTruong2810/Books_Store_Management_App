using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Class giả lập tài khoản thanh toán.
    /// Giả lập khi tạo Customer tạo ra một tài khoản thanh toán.
    /// Có 1 tài khoản admin để nhận tiền.
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public Decimal Balance { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
