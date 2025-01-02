using Books_Store_Management_App.Models.Payment.Enums;
using Books_Store_Management_App.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Services
{
    /// <summary>
    /// Dịch vụ thanh toán.
    /// </summary>
    public class PaymentService
    {
        private readonly PaymentStrategyFactory _strategyFactory;

        public PaymentService(PaymentStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        /// <summary>
        /// Xử lý thanh toán.
        /// </summary>
        /// <param name="method">Phương thức thanh toán.</param>
        /// <param name="request">Yêu cầu thanh toán.</param>
        /// <returns>Kết quả thanh toán.</returns>
        public async Task<PaymentResult> ProcessPayment(PaymentMethod method, PaymentRequest request)
        {
            var strategy = _strategyFactory.CreateStrategy(method);
            return await strategy.ProcessPayment(request);
        }

        /// <summary>
        /// Truy vấn đơn hàng.
        /// </summary>
        /// <param name="method">Phương thức thanh toán.</param>
        /// <param name="appTransId">Mã giao dịch ứng dụng.</param>
        /// <returns>Kết quả truy vấn đơn hàng.</returns>
        public async Task<PaymentResult> QueryOrder(PaymentMethod method, string appTransId)
        {
            var strategy = _strategyFactory.CreateStrategy(method);
            return await strategy.QueryOrder(appTransId);
        }
    }
}
