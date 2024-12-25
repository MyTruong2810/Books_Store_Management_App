using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Clas này chứa thông tin của một yêu cầu thanh toán.
    /// </summary>
    public class PaymentRequest
    {
        public string AppId { get; set; }
        public string AppUser { get; set; }
        public string AppTime { get; set; }
        public string Amount { get; set; }
        public string AppTransId { get; set; }
        public string EmbedData { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Description { get; set; }
        public string BankCode { get; set; }
        public string Mac { get; set; }
        public int orderId { get; set; }
        public int? MemberPaymentId { get; set; }
    }
}
