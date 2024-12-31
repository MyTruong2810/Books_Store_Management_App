using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models.Payment
{
    /// <summary>
    /// Clas này chứa thông tin của một yêu cầu thanh toán.
    /// </summary>
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public string orderUrl { get; set; }
        public string appTransId { get; set; }
        public string qrCode { get; set; }

        // TODO: Thêm các thông tin khác cần thiết cho kết quả thanh toán.
    }
}
