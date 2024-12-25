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
    public class DemoPaymentStrategy : IPaymentStrategy
    {
        private readonly PaymentRepository _paymentRepository;

        public DemoPaymentStrategy(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

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

        public Task<PaymentResult> QueryOrder(string appTransId)
        {
            throw new NotImplementedException();
        }
    }
}
