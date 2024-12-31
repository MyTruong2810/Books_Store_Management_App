using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Class đại diện cho một giao dịch.
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public string? FromUserId { get; set; }
        public string ToUserId { get; set; }
        public DateTime TimeCreate { get; set; }
        public string Content { get; set; }
        public decimal Amount { get; set; }
        public int orderId { get; set; }
    }
}
