using Books_Store_Management_App.Models.Payment;
using Books_Store_Management_App.Models;
using Books_Store_Management_App.Services.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Services.Payment.Implementations
{
    /// <summary>
    /// Chiến lược thanh toán demo.
    /// </summary>
    public class DemoPaymentStrategy : IPaymentStrategy
    {
        private readonly PaymentRepository _paymentRepository;

        public DemoPaymentStrategy(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        /// <summary>
        /// Xử lý thanh toán dựa trên yêu cầu thanh toán được cung cấp.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaymentResult> ProcessPayment(PaymentRequest request)
        {
            var (success, message) = await _paymentRepository.ProcessDemoPayment(request);

            return new PaymentResult
            {
                Success = success,
                TransactionId = success ? message : null,
                Message = success ? "Payment processed successfully" : message
            };
        }

        /// <summary>
        /// Xử lý truy vấn thông tin đơn hàng dựa trên appTransId.
        /// </summary>
        /// <param name="appTransId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<PaymentResult> QueryOrder(string appTransId)
        {
            throw new NotImplementedException();
        }
    }
}
