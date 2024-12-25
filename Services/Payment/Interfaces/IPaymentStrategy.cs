using Books_Store_Management_App.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Services.Payment.Interfaces
{
    /// <summary>
    /// Interface xác định các phương thức cần cung cấp cho một dịch vụ thanh toán.
    /// </summary>
    public interface IPaymentStrategy
    {
        Task<PaymentResult> ProcessPayment(PaymentRequest request);
        Task<PaymentResult> QueryOrder(string appTransId);
    }
}
