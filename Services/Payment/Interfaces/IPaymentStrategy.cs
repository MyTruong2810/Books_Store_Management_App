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
        /// <summary>
        /// Xử lý thanh toán dựa trên yêu cầu thanh toán được cung cấp.
        /// </summary>
        /// <param name="request">Yêu cầu thanh toán.</param>
        /// <returns>Kết quả thanh toán.</returns>
        Task<PaymentResult> ProcessPayment(PaymentRequest request);

        /// <summary>
        /// Truy vấn thông tin đơn hàng dựa trên appTransId.
        /// </summary>
        /// <param name="appTransId">Mã giao dịch của ứng dụng.</param>
        /// <returns>Kết quả truy vấn đơn hàng.</returns>
        Task<PaymentResult> QueryOrder(string appTransId);
    }
}
